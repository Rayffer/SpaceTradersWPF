﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.ViewModels;

internal class AgentContractsViewModel : BindableBase
{
    private bool isProcessingCommand;
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly INotificationService notificationService;
    private IEnumerable<Contract> contracts;
    private IEnumerable<Ship> ships;
    private Contract selectedContract;
    private Ship selectedShip;
    private DelegateCommand loadContractsCommand;
    private DelegateCommand<Contract> acceptContractCommand;
    private DelegateCommand<Contract> fulfillContractCommand;
    private DelegateCommand<Contract> deliverContractCommand;
    private DelegateCommand<Ship> negotiateContractCommand;

    public bool IsProcessingCommand 
    { 
        get => this.isProcessingCommand; 
        set => this.SetProperty(ref this.isProcessingCommand, value); 
    }

    public bool CanFulfillContract
    {
        get => this.SelectedContract is not null
               && !this.SelectedContract.Fulfilled
               && this.SelectedContract.Terms.Deliver.All(delivery => delivery.UnitsRequired <= delivery.UnitsFulfilled);
    }

    public bool CanShipDeliverContract
    {
        get => this.SelectedContract is not null
               && this.SelectedShip is not null
               && (this.SelectedShip.NavigationInformation.Status == "DOCKED" ||
                   this.SelectedShip.NavigationInformation.Status == "IN_ORBIT")
               && this.SelectedContract.Terms.Deliver
                   .Any(delivery => delivery.UnitsRequired > delivery.UnitsFulfilled
                                    && delivery.DestinationSymbol == this.SelectedShip.NavigationInformation.WaypointSymbol
                                    && this.SelectedShip.Cargo.Inventory.Any(inventory => inventory.Symbol == delivery.TradeSymbol));
    }

    public bool CanAcceptContract
    {
        get => this.SelectedContract is not null &&
            !this.SelectedContract.Accepted;
    }

    public bool CanShipNegotiateContract
    {
        get => this.Contracts?.All(x => x.Accepted && x.Fulfilled) ?? false &&
               this.SelectedShip.NavigationInformation.Status == "DOCKED";
    }

    public IEnumerable<Contract> Contracts
    {
        get => this.contracts;
        set
        {
            this.SetProperty(ref this.contracts, value);
        }
    }

    public IEnumerable<Ship> Ships
    {
        get => this.ships;
        set => this.SetProperty(ref this.ships, value);
    }

    public Contract SelectedContract
    {
        get => this.selectedContract;
        set
        {
            this.SetProperty(ref this.selectedContract, value);
            this.RaisePropertyChanged(nameof(this.CanFulfillContract));
            this.RaisePropertyChanged(nameof(this.CanShipDeliverContract));
            this.RaisePropertyChanged(nameof(this.CanAcceptContract));
            this.RaisePropertyChanged(nameof(this.CanShipNegotiateContract));
        }
    }

    public Ship SelectedShip
    {
        get => this.selectedShip;
        set
        {
            this.SetProperty(ref this.selectedShip, value);
            this.RaisePropertyChanged(nameof(this.CanFulfillContract));
            this.RaisePropertyChanged(nameof(this.CanShipDeliverContract));
            this.RaisePropertyChanged(nameof(this.CanAcceptContract));
            this.RaisePropertyChanged(nameof(this.CanShipNegotiateContract));
        }
    }

    public ICommand AcceptContractCommand => this.acceptContractCommand ??= new DelegateCommand<Contract>(async contract => await this.AcceptContract(contract));
    public ICommand DeliverContractCommand => this.deliverContractCommand ??= new DelegateCommand<Contract>(async contract => await this.DeliverContract(contract));
    public ICommand FulfillContractCommand => this.fulfillContractCommand ??= new DelegateCommand<Contract>(async contract => await this.FulfillContract(contract));
    public ICommand LoadInformationCommand => this.loadContractsCommand ??= new DelegateCommand(async () => await this.LoadInformation());
    public ICommand NegotiateContractCommand => this.negotiateContractCommand ??= new DelegateCommand<Ship>(async ship => await this.NegotiateContract(ship));

    public AgentContractsViewModel(
        ISpaceTradersApi spaceTradersApi,
        INotificationService notificationService)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.notificationService = notificationService;
    }

    private async Task LoadInformation()
    {
        await this.RefreshContracts(this.SelectedContract);
        await this.RefreshShips(this.SelectedShip);
    }

    private async Task AcceptContract(Contract contract)
    {
        this.IsProcessingCommand = true;

        var acceptContractResult = await this.spaceTradersApi.PostAcceptContract(contract.Id);

        this.notificationService.ShowToastNotification(
            $"Accepted contract with id {acceptContractResult.Contract.Id}",
            $"Received forward payment of {acceptContractResult.Contract.Terms.Payment.OnAccepted} units, " +
            $"with dealine of {acceptContractResult.Contract.DeadlineToAccept}",
            NotificationTypes.PositiveFeedback,
            true);

        await this.RefreshContracts(this.SelectedContract);

        this.IsProcessingCommand = false;
    }

    private async Task DeliverContract(Contract contract)
    {
        this.IsProcessingCommand = true;

        if (this.SelectedShip.NavigationInformation.Status == "IN_ORBIT")
        {
            await this.spaceTradersApi.PostShipDock(this.SelectedShip.Symbol);
            await Task.Delay(TimeSpan.FromSeconds(1));
            this.SelectedShip.NavigationInformation.Status = "DOCKED";
        }
        var contractCargoToDeliver = this.SelectedContract.Terms.Deliver.FirstOrDefault(delivery =>
            delivery.DestinationSymbol == this.SelectedShip.NavigationInformation.WaypointSymbol &&
            this.SelectedShip.Cargo.Inventory.Any(inventory => inventory.Symbol == delivery.TradeSymbol));

        var cargoToDeliver = this.SelectedShip.Cargo.Inventory.FirstOrDefault(inventory => inventory.Symbol == contractCargoToDeliver.TradeSymbol);

        var deliver = await this.spaceTradersApi.PostDeliverContract(contract.Id, new ApiModels.Requests.ContractDeliverRequest
        {
            ShipSymbol = this.SelectedShip.Symbol,
            TradeSymbol = cargoToDeliver.Symbol,
            Units = Math.Min(contractCargoToDeliver.UnitsRequired - contractCargoToDeliver.UnitsFulfilled, cargoToDeliver.Units)
        });
        await Task.Delay(TimeSpan.FromSeconds(1));

        this.notificationService.ShowToastNotification(
            $"Delivered cargo for contract {contract.Id}",
            string.Empty,
            NotificationTypes.PositiveFeedback);

        if (this.SelectedShip.NavigationInformation.Status == "DOCKED")
        {
            await this.spaceTradersApi.PostShipOrbit(this.SelectedShip.Symbol);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        await this.RefreshContracts(contract);
        await this.RefreshShips(this.SelectedShip);

        this.IsProcessingCommand = false;
    }

    private async Task FulfillContract(Contract contract)
    {
        this.IsProcessingCommand = true;

        _ = await this.spaceTradersApi.PostFulfillContract(contract.Id);

        this.notificationService.ShowToastNotification(
            $"Fulfilled contract {contract.Id}",
            $"received {contract.Terms.Payment.OnFulfilled} credits",
            NotificationTypes.PositiveFeedback);

        await this.RefreshContracts(this.SelectedContract);

        this.IsProcessingCommand = false;
    }

    private async Task RefreshContracts(Contract contract)
    {
        this.Contracts = await this.spaceTradersApi.GetContracts();
        if (contract is not null)
        {
            this.SelectedContract = this.Contracts.First(x => x.Id == contract.Id);
        }
    }

    private async Task RefreshShips(Ship ship)
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
        if (ship is not null)
        {
            this.SelectedShip = this.Ships.First(x => x.Symbol == ship.Symbol);
        }
    }

    private async Task NegotiateContract(Ship ship)
    {
        this.IsProcessingCommand = true;

        var contract = await this.spaceTradersApi.PostShipNegotiateContract(ship.Symbol);

        if (contract is not null)
        {
            this.notificationService.ShowToastNotification(
                "New contract negotiated",
                $"Contract {contract.Id} received, check details",
                NotificationTypes.PositiveFeedback);
        }

        this.IsProcessingCommand = false;
    }
}
