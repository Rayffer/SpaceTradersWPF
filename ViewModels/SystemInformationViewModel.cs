﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

using ApiSystem = SpaceTradersWPF.ApiModels.System;

namespace SpaceTradersWPF.ViewModels;

internal class SystemInformationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;
    private IEnumerable<ApiSystem> systems;
    private ApiSystem selectedSystem;
    private DelegateCommand loadInformationCommand;
    private DelegateCommand<Waypoint> openWaypointInformationCommand;
    private Waypoint[] systemWaypoints;
    private int pageNumber;
    private int lastPageNumberLoaded;
    private int maxPage;

    public int PageNumber
    {
        get => this.pageNumber;
        set => this.SetProperty(ref this.pageNumber, value);
    }

    public int MaxPage
    {
        get => this.maxPage;
        set => this.SetProperty(ref this.maxPage, value);
    }

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
            if (value is not null)
            {
                this.eventAggregator.GetEvent<SystemWaypointInformationEvent>().Publish(new SystemWaypointInformationEventArgs
                {
                    SystemSymbol = value.Symbol
                });
            }
        }
    }

    public Waypoint[] SystemWaypoints
    {
        get => this.systemWaypoints;
        set => this.SetProperty(ref this.systemWaypoints, value);
    }

    public ICommand LoadInformationCommand => this.loadInformationCommand ??= new DelegateCommand(async () => await this.LoadInformation());
    public ICommand OpenWaypointInformationCommand => this.openWaypointInformationCommand ??= new DelegateCommand<Waypoint>(this.OpenWaypointInformationDetail);

    public SystemInformationViewModel(
        IEventAggregator eventAggregator,
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.eventAggregator = eventAggregator;
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
        this.eventAggregator.GetEvent<SystemInformationEvent>().Subscribe(async (arguments) => await this.GetSystemInformation(arguments));
        this.eventAggregator.GetEvent<SystemWaypointInformationEvent>().Subscribe(async (arguments) => await this.GetSystemWaypointsInformation(arguments));

        this.PageNumber = 1;
        this.MaxPage = 8999 / 20;
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
        if (this.PageNumber == this.lastPageNumberLoaded)
        {
            return;
        }
        this.Systems = await this.spaceTradersApi.GetSystems(this.PageNumber, 20);
        this.SystemWaypoints = Enumerable.Empty<Waypoint>().ToArray();
        this.SelectedSystem = this.Systems.First();

        this.lastPageNumberLoaded = this.PageNumber;
    }

    private void OpenWaypointInformationDetail(Waypoint waypoint)
    {
        if (this.regionManager.Regions[RegionNames.DetailViewAreaRegion].Views.OfType<WaypointInformationView>().Any())
        {
            return;
        }

        this.regionManager.Regions[RegionNames.DetailViewAreaRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.DetailViewAreaRegion, typeof(WaypointInformationView));
        this.eventAggregator.GetEvent<WaypointInformationEvent>().Publish(new WaypointInformationEventArguments
        {
            Waypoint = waypoint
        });
    }
}
