﻿using System.Threading.Tasks;

using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Events.Models;

namespace SpaceTradersWPF.ViewModels;

internal class WaypointInformationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    private readonly ISpaceTradersApi spaceTradersApi;
    private Waypoint waypointInformation;

    public Waypoint WaypointInformation
    {
        get => waypointInformation;
        set => SetProperty(ref waypointInformation, value);
    }

    public WaypointInformationViewModel(
        IEventAggregator eventAggregator,
        ISpaceTradersApi spaceTradersApi)
    {
        this.eventAggregator = eventAggregator;
        this.spaceTradersApi = spaceTradersApi;

        this.eventAggregator.GetEvent<WaypointInformationEvent>().Subscribe(async waypointInformation => await GetWaypointInformation(waypointInformation));
    }

    ~WaypointInformationViewModel()
    {
        this.eventAggregator.GetEvent<WaypointInformationEvent>().Unsubscribe(async waypointInformation => await GetWaypointInformation(waypointInformation));
    }

    private async Task GetWaypointInformation(WaypointInformationEventArguments eventInformation)
    {
        var waypointInformation = await this.spaceTradersApi.GetWaypoint(eventInformation.WaypointSymbol);
        this.WaypointInformation = waypointInformation;
    }
}
