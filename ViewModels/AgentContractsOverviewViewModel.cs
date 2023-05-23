using System;
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

internal class AgentContractsOverviewViewModel : BindableBase
{
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

    public bool IsSelectedContractFulfilled
    {
        get => this.SelectedContract != null &&
            this.SelectedContract.Terms.Deliver.All(delivery => delivery.UnitsRequired <= delivery.UnitsFulfilled);
    }

    public bool SelectedShipCanDeliverSelectedContract
    {
        get => this.SelectedContract != null &&
            this.SelectedShip != null &&
            this.SelectedContract.Terms.Deliver.Any(delivery => delivery.DestinationSymbol == this.SelectedShip.NavigationInformation.WaypointSymbol &&
                                                                   this.SelectedShip.Cargo.Inventory.Any(inventory => inventory.Symbol == delivery.TradeSymbol));
    }

    public bool CanAcceptContract
    {
        get => this.SelectedContract != null &&
            !this.SelectedContract.Accepted;
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
            this.RaisePropertyChanged(nameof(this.IsSelectedContractFulfilled));
            this.RaisePropertyChanged(nameof(this.SelectedShipCanDeliverSelectedContract));
            this.RaisePropertyChanged(nameof(this.CanAcceptContract));
        }
    }

    public Ship SelectedShip
    {
        get => this.selectedShip;
        set
        {
            this.SetProperty(ref this.selectedShip, value);
            this.RaisePropertyChanged(nameof(this.IsSelectedContractFulfilled));
            this.RaisePropertyChanged(nameof(this.SelectedShipCanDeliverSelectedContract));
            this.RaisePropertyChanged(nameof(this.CanAcceptContract));
        }
    }

    public ICommand AcceptContractCommand => this.acceptContractCommand ??= new DelegateCommand<Contract>(async contract => await this.AcceptContract(contract));

    public ICommand DeliverContractCommand => this.deliverContractCommand ??= new DelegateCommand<Contract>(async contract => await this.DeliverContract(contract));

    public ICommand FulfillContractCommand => this.fulfillContractCommand ??= new DelegateCommand<Contract>(async contract => await this.FulfillContract(contract));

    public ICommand LoadInformationCommand => this.loadContractsCommand ??= new DelegateCommand(async () => await this.LoadInformation());

    public AgentContractsOverviewViewModel(
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
        var acceptContractResult = await this.spaceTradersApi.PostAcceptContract(contract.Id);

        this.notificationService.ShowToastNotification(
            $"Accepted contract with id {acceptContractResult.Contract.Id}",
            $"Received forward payment of {acceptContractResult.Contract.Terms.Payment.OnAccepted} units, " +
            $"with dealine of {acceptContractResult.Contract.DeadlineToAccept}",
            NotificationTypes.PositiveFeedback,
            true);
    }

    private async Task DeliverContract(Contract contract)
    {
        throw new NotImplementedException();
    }

    private async Task FulfillContract(Contract contract)
    {
        throw new NotImplementedException();
    }

    private async Task RefreshContracts(Contract contract)
    {
        this.Contracts = await this.spaceTradersApi.GetContracts();
        if (contract != null)
        {
            this.SelectedContract = this.Contracts.First(x => x.Id == contract.Id);
        }
    }

    private async Task RefreshShips(Ship ship)
    {
        this.Ships = await this.spaceTradersApi.GetShips(1, 20);
        if (ship != null)
        {
            this.SelectedShip = this.Ships.First(x => x.Symbol == ship.Symbol);
        }
    }
}
