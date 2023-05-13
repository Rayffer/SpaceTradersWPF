using System.Threading.Tasks;

using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;

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

    private async Task GetWaypointInformation(WaypointInformation eventInformation)
    {
        var waypointInformation = await this.spaceTradersApi.GetWaypoint(eventInformation.WaypointSymbol);
        this.WaypointInformation = waypointInformation;
    }
}