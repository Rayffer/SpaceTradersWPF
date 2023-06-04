﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ClassExtensions;
using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Types;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentShipsViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IEventAggregator eventAggregator;
    private readonly INotificationService notificationService;
    private readonly IRegionManager regionManager;
    private readonly IWaypointSurveyService waypointSurveyService;
    private IEnumerable<Ship> ships;
    private Ship selectedShip;
    private Inventory selectedInventory;
    private DelegateCommand loadShipsCommand;
    private DelegateCommand<Ship> performExtractionCommand;
    private DelegateCommand<Ship> performSurveyCommand;
    private DelegateCommand<Ship> performWarpCommand;
    private DelegateCommand<Ship> performOrbitCommand;
    private DelegateCommand<Ship> performDockCommand;
    private DelegateCommand<Ship> performNavigateCommand;
    private DelegateCommand<Ship> performRefuelCommand;
    private DelegateCommand<object[]> performInventorySellCommand;
    private DelegateCommand<Ship> automateMiningCommand;
    private DelegateCommand<Ship> cancelShipAutomationCommand;

    private static readonly Dictionary<string, CancellationTokenSource> shipCancellationTokens = new Dictionary<string, CancellationTokenSource>();

    private bool alreadyLoaded;
    private string cargoToSell;

    public string CargoToSell
    {
        get => this.cargoToSell;
        set => this.SetProperty(ref this.cargoToSell, value);
    }

    public Ship SelectedShip
    {
        get => this.selectedShip;
        set
        {
            this.SetProperty(ref this.selectedShip, value);
        }
    }

    public IEnumerable<Ship> Ships
    {
        get => this.ships;
        set => this.SetProperty(ref this.ships, value);
    }

    public Inventory SelectedInventory
    {
        get => this.selectedInventory;
        set
        {
            this.SetProperty(ref this.selectedInventory, value);
            if (value is not null && this.SelectedShip.NavigationInformation.Status == "DOCKED")
            {
                this.CargoToSell = value.Units.ToString();
                this.RaisePropertyChanged(nameof(this.CargoToSell));
            }
            this.performInventorySellCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand LoadShipsCommand => this.loadShipsCommand ??= new DelegateCommand(async () => await this.LoadShips());
    public ICommand PerformExtractionCommand => this.performExtractionCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformExtraction(ship), this.CanExtract);
    public ICommand PerformSurveyCommand => this.performSurveyCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformSurvey(ship), this.CanSurvey);
    public ICommand PerformWarpCommand => this.performWarpCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformWarp(ship), this.CanWarp);
    public ICommand PerformOrbitCommand => this.performOrbitCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformOrbit(ship), this.CanOrbit);
    public ICommand PerformDockCommand => this.performDockCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformDock(ship), this.CanDock);
    public ICommand PerformNavigateCommand => this.performNavigateCommand ??= new DelegateCommand<Ship>(ship => this.PerformNavigate(ship), this.CanNavigate);
    public ICommand PerformRefuelCommand => this.performRefuelCommand ??= new DelegateCommand<Ship>(async ship => await this.PerformRefuel(ship), this.CanRefuel);
    public ICommand PerformInventorySellCommand => this.performInventorySellCommand ??= new DelegateCommand<object[]>(async parameters => await this.PerformInventorySell(parameters), this.CanSell);
    public ICommand AutomateMiningCommand => this.automateMiningCommand ??= new DelegateCommand<Ship>(this.AutomateMining, this.CanStartAutomation);
    public ICommand CancelShipAutomationCommand => this.cancelShipAutomationCommand ??= new DelegateCommand<Ship>(this.CancelShipAutomation, this.CanCancelAutomation);

    public AgentShipsViewModel(
        ISpaceTradersApi spaceTradersApi,
        IEventAggregator eventAggregator,
        INotificationService notificationService,
        IRegionManager regionManager,
        IWaypointSurveyService waypointSurveyService)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.notificationService = notificationService;
        this.regionManager = regionManager;
        this.waypointSurveyService = waypointSurveyService;
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<ShipInformationEvent>().Subscribe(async (eventInformation) => await this.LoadSelectedShipInformation(eventInformation));
    }

    ~AgentShipsViewModel()
    {
        this.eventAggregator.GetEvent<ShipInformationEvent>().Unsubscribe(async (eventInformation) => await this.LoadSelectedShipInformation(eventInformation));
    }

    private async Task LoadSelectedShipInformation(ShipInformationEventArguments arguments)
    {
        this.alreadyLoaded = true;
        await this.RefreshShips(arguments.Ship);
    }

    private async Task LoadShips()
    {
        if (this.alreadyLoaded)
        {
            this.alreadyLoaded = false;
            return;
        }

        await this.RefreshShips(this.SelectedShip);
    }

    private async Task PerformExtraction(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
        var result = await this.spaceTradersApi.PostShipExtractResources(ship.Symbol, ship.NavigationInformation.WaypointSymbol);

        this.notificationService.ShowToastNotification(
            $"Ship extracted {result.Extraction.Yield.Units} unit{(result.Extraction.Yield.Units > 1 ? "s" : string.Empty)} of {result.Extraction.Yield.Symbol}",
            null,
            NotificationTypes.PositiveFeedback,
            true);
        await this.RefreshShips(ship);
    }

    private async Task PerformSurvey(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
        var surveyInformation = await this.spaceTradersApi.PostShipCreateSurvey(ship.Symbol);
        this.waypointSurveyService.SaveSurveyDetails(surveyInformation.Surveys);

        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} surveyed waypoint {ship.NavigationInformation.WaypointSymbol}",
            $"Found fields: {string.Join(", ", surveyInformation.Surveys.SelectMany(survey => survey.Deposits.Select(deposit => deposit.Symbol)))}",
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task PerformWarp(Ship ship)
    {
        var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
        if (cooldown != null)
        {
            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} is cooling down",
                $"Remaining cooldown: {cooldown.RemainingSeconds} seconds",
                NotificationTypes.WarningFeedback,
                true);
            return;
        }
    }

    private async Task PerformOrbit(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipOrbit(ship.Symbol);
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} entered orbit succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task PerformDock(Ship ship)
    {
        _ = await this.spaceTradersApi.PostShipDock(ship.Symbol);
        if (this.SelectedInventory is null)
        {
            this.CargoToSell = string.Empty;
        }
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} docked succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private void PerformNavigate(Ship ship)
    {
        this.regionManager.RegisterViewWithRegion(RegionNames.DialogAreaRegion, typeof(ShipNavigationView));
        this.eventAggregator
            .GetEvent<ShipNavigationRequestEvent>()
            .Publish(new ShipNavigationRequestEventArguments
            {
                Ship = ship
            });
    }

    private async Task PerformRefuel(Ship ship)
    {
        var refuelResponse = await this.spaceTradersApi.PostShipRefuel(ship.Symbol);
        await this.RefreshShips(ship);
        this.notificationService.ShowToastNotification(
            $"Ship {ship.Symbol} refueled succesfully",
            null,
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task RefreshShips(Ship ship)
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
        if (ship != null)
        {
            this.SelectedShip = this.Ships.First(x => x.Symbol == ship.Symbol);
        }
    }

    private async Task PerformInventorySell(object[] commandParameters)
    {
        if (commandParameters.Length == 2 &&
            commandParameters[0] is Ship ship)
        {
            var sellCargoResponse = await this.spaceTradersApi.PostShipSellCargo(ship.Symbol, new ShipSellCargoRequest
            {
                Symbol = this.SelectedInventory.Symbol,
                Units = int.Parse(this.CargoToSell)
            });

            this.notificationService.ShowToastNotification(
                $"Ship {ship.Symbol} sold cargo",
                $"Sold {sellCargoResponse.Transaction.Units} units of {sellCargoResponse.Transaction.TradeSymbol} for {sellCargoResponse.Transaction.TotalPrice} credits, current credits: {sellCargoResponse.Agent.Credits}",
                NotificationTypes.PositiveFeedback);

            await this.RefreshShips(this.SelectedShip);
        }
    }

    private void AutomateMining(Ship shipToAutomate)
    {
        var clonedShip = shipToAutomate.Clone();
        var shipCancellationTokenSource = new CancellationTokenSource();
        shipCancellationTokens.Add(clonedShip.Symbol, shipCancellationTokenSource);
        this.UpdateActionButtons();

        var automationAction = new Action<Ship, CancellationTokenSource>(async (ship, cancellationToken) =>
        {
            var shipCargo = ship.Cargo.Units;
            var cooldown = await this.spaceTradersApi.GetShipCooldown(ship.Symbol);
            if (cooldown is not null)
            {
                await Task.Delay(TimeSpan.FromSeconds(cooldown.RemainingSeconds));
                cooldown = default;
            }
            while (!cancellationToken.IsCancellationRequested)
            {
                if (shipCargo >= ship.Cargo.Capacity * 0.75)
                {
                    ship = await this.spaceTradersApi.GetShip(ship.Symbol);
                    if (ship.Cargo.Inventory.Where(cargo => cargo.Symbol == "ANTIMATTER" /*|| cargo.Symbol == "ALUMINUM_ORE"*/).Sum(cargo => cargo.Units) >= ship.Cargo.Capacity * 0.75)
                    {
                        cancellationToken.Cancel(false);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.notificationService.ShowFlyoutNotification(
                            $"{ship.Symbol} cannot continue mining",
                            "Cargo is full of excluded goods",
                            NotificationTypes.WarningFeedback);
                        });
                        continue;
                    }
                    if (ship.NavigationInformation.Status != "DOCKED")
                    {
                        await this.spaceTradersApi.PostShipDock(ship.Symbol);
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    var creditsAfterTransaction = 0;
                    var cargoToSell = ship.Cargo.Inventory.Where(cargo => cargo.Symbol != "ANTIMATTER" /*&& cargo.Symbol != "ALUMINUM_ORE"*/);
                    foreach (var cargo in cargoToSell)
                    {
                        var transaction = await this.spaceTradersApi.PostShipSellCargo(ship.Symbol, new ShipSellCargoRequest { Symbol = cargo.Symbol, Units = cargo.Units });
                        if (transaction is null)
                        {
                            return;
                        }
                        creditsAfterTransaction += transaction.Transaction.TotalPrice;
                        shipCargo -= transaction.Transaction.Units;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.notificationService.ShowFlyoutNotification(
                        $"{ship.Symbol} sold goods",
                        $"Total sale price {creditsAfterTransaction}",
                        NotificationTypes.PositiveFeedback);
                    });
                    var orbitResponse = await this.spaceTradersApi.PostShipOrbit(ship.Symbol);
                    ship.NavigationInformation.Status = "IN_ORBIT";
                }

                if (ship.NavigationInformation.Status != "IN_ORBIT")
                {
                    await this.spaceTradersApi.PostShipOrbit(ship.Symbol);
                }

                if (cooldown is not null)
                {
                    await Task.Delay(TimeSpan.FromSeconds((cooldown.Expiration - DateTime.UtcNow).TotalSeconds + 1));
                }
                //if (this.SelectedShip.Mounts.Any(mount => mount.Symbol.StartsWith("MOUNT_SURVEYOR_")) &&
                //    this.waypointSurveyService.GetSurvey(ship.NavigationInformation.WaypointSymbol) is null)
                //{
                //    var survey = await this.spaceTradersApi.PostShipCreateSurvey(ship.Symbol);
                //    this.waypointSurveyService.SaveSurveyDetails(survey.Surveys);
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        this.notificationService.ShowFlyoutNotification(
                //            $"Ship {ship.Symbol} surveyed waypoint {ship.NavigationInformation.WaypointSymbol}",
                //            $"Found fields: {string.Join(", ", survey.Surveys.SelectMany(survey => survey.Deposits.Select(deposit => deposit.Symbol)))}",
                //            NotificationTypes.PositiveFeedback,
                //            true);
                //    });
                //    await Task.Delay(TimeSpan.FromSeconds(survey.Cooldown.RemainingSeconds));
                //}
                var extractionResponse = await this.spaceTradersApi.PostShipExtractResources(ship.Symbol, ship.NavigationInformation.WaypointSymbol);
                if (extractionResponse is not null)
                {
                    shipCargo += extractionResponse.Extraction.Yield.Units;
                    cooldown = extractionResponse.Cooldown;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.notificationService.ShowFlyoutNotification(
                            $"{ship.Symbol} extracted from {ship.NavigationInformation.WaypointSymbol}",
                            $"Yield: {extractionResponse.Extraction.Yield.Units} units of {extractionResponse.Extraction.Yield.Symbol}, remaining cargo capacity: {ship.Cargo.Capacity - shipCargo}",
                            NotificationTypes.PositiveFeedback);
                    });
                }
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                shipCancellationTokens.Remove(ship.Symbol);
                this.UpdateActionButtons();
                this.notificationService.ShowFlyoutNotification(
                    $"Ship {ship.Symbol} finished its orders",
                    "Ship is now available for manual control",
                    NotificationTypes.PositiveFeedback);
            });
        });
        Task.Run(() => automationAction(clonedShip, shipCancellationTokenSource));
    }

    private void UpdateActionButtons()
    {
        this.automateMiningCommand.RaiseCanExecuteChanged();
        this.cancelShipAutomationCommand.RaiseCanExecuteChanged();
        this.performDockCommand.RaiseCanExecuteChanged();
        this.performOrbitCommand.RaiseCanExecuteChanged();
        this.performNavigateCommand.RaiseCanExecuteChanged();
        this.performRefuelCommand.RaiseCanExecuteChanged();
        this.performWarpCommand.RaiseCanExecuteChanged();
        this.performExtractionCommand.RaiseCanExecuteChanged();
        this.performInventorySellCommand.RaiseCanExecuteChanged();
        this.performSurveyCommand.RaiseCanExecuteChanged();
    }

    private void CancelShipAutomation(Ship ship)
    {
        if (shipCancellationTokens.TryGetValue(ship.Symbol, out var cancelToken))
        {
            cancelToken.Cancel(false);
            this.notificationService.ShowFlyoutNotification(
                $"Requested ship {ship.Symbol} to stop its orders",
                "Once current orders finish it will be available",
                NotificationTypes.WarningFeedback);
        }
    }

    private bool CanExtract(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "IN_ORBIT"
               && ship.NavigationInformation.Route.Destination.Type == "ASTEROID_FIELD";
    }

    private bool CanSurvey(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "IN_ORBIT"
               && ship.NavigationInformation.Route.Destination.Type == "ASTEROID_FIELD";
    }

    private bool CanWarp(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "IN_ORBIT";
    }

    private bool CanOrbit(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "DOCKED";
    }

    private bool CanDock(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "IN_ORBIT";
    }

    private bool CanNavigate(Ship ship)
    {
        return ship != null
               && !shipCancellationTokens.ContainsKey(ship.Symbol)
               && ship.NavigationInformation.Status == "IN_ORBIT";
    }

    private bool CanRefuel(Ship ship)
    {
        return ship != null
               && ship.NavigationInformation.Status == "DOCKED"
               && !shipCancellationTokens.ContainsKey(ship.Symbol);
    }

    private bool CanSell(object[] commandParameters)
    {
        return commandParameters != null
            && commandParameters.Length == 2
            && commandParameters[0] is Ship ship
            && ship.NavigationInformation.Status == "DOCKED"
            && commandParameters[1] is Inventory inventory
            && !shipCancellationTokens.ContainsKey(ship.Symbol)
            && inventory is not null;
    }

    private bool CanStartAutomation(Ship ship)
    {
        return ship != null
            && ship.NavigationInformation.Route.Destination.Type == "ASTEROID_FIELD"
            && ship.NavigationInformation.Status == "IN_ORBIT"
            && ship.Mounts.Any(mount => mount.Symbol == "MOUNT_MINING_LASER_II" || mount.Symbol == "MOUNT_MINING_LASER_I" || mount.Symbol == "MOUNT_MINING_LASER_III")
            && ship.Modules.Any(module => module.Symbol == "MODULE_MINERAL_PROCESSOR_I")
            && !shipCancellationTokens.ContainsKey(ship.Symbol);
    }

    private bool CanCancelAutomation(Ship ship)
    {
        return ship != null
            && shipCancellationTokens.ContainsKey(ship.Symbol)
            && !shipCancellationTokens[ship.Symbol].IsCancellationRequested;
    }
}
