using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Services;

using ApiSystem = SpaceTradersWPF.ApiModels.System;

namespace SpaceTradersWPF.ViewModels;

internal class SystemInformationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    private readonly ISpaceTradersApi spaceTradersApi;
    private IEnumerable<ApiSystem> systems;
    private ApiSystem selectedSystem;
    private DelegateCommand loadInformationCommand;

    public IEnumerable<ApiSystem> Systems
    {
        get => this.systems;
        set => this.SetProperty(ref this.systems, value);
    }

    public ApiSystem SelectedSystem
    {
        get => this.selectedSystem;
        set
        {
            this.SetProperty(ref this.selectedSystem, value);
            this.eventAggregator.GetEvent<SystemWaypointInformationEvent>().Publish(new SystemWaypointInformationEventArgs
            {
                SystemSymbol = value.Symbol
            });
        }
    }

    public ICommand LoadInformationCommand => this.loadInformationCommand ??= new DelegateCommand(async () => await this.LoadInformation());

    public Waypoint[] SystemWaypoints { get; private set; }

    public SystemInformationViewModel(IEventAggregator eventAggregator,
        ISpaceTradersApi spaceTradersApi)
    {
        this.eventAggregator = eventAggregator;
        this.spaceTradersApi = spaceTradersApi;
        this.eventAggregator.GetEvent<SystemInformationEvent>().Subscribe(async (arguments) => await this.GetSystemInformation(arguments));
        this.eventAggregator.GetEvent<SystemWaypointInformationEvent>().Subscribe(async (arguments) => await this.GetSystemWaypointsInformation(arguments));
    }

    ~SystemInformationViewModel()
    {
        this.eventAggregator.GetEvent<SystemInformationEvent>().Unsubscribe(async (arguments) => await this.GetSystemInformation(arguments));
        this.eventAggregator.GetEvent<SystemWaypointInformationEvent>().Unsubscribe(async (arguments) => await this.GetSystemWaypointsInformation(arguments));
    }

    private async Task GetSystemWaypointsInformation(SystemWaypointInformationEventArgs eventInformation)
    {
        this.SystemWaypoints = await this.spaceTradersApi.GetWaypoints(eventInformation.SystemSymbol, 1, 20);
    }

    private async Task GetSystemInformation(SystemInformationEventArguments eventInformation)
    {
        this.eventAggregator.GetEvent<SystemInformationEvent>().Unsubscribe(async (arguments) => await this.GetSystemInformation(arguments));
    }

    private async Task LoadInformation()
    {
        this.Systems = await this.spaceTradersApi.GetSystems(1, 20);
    }
}
