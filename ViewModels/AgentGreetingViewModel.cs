using System;
using System.Windows.Threading;

using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Models;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class AgentGreetingViewModel : BindableBase
{
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;
    private Agent currentAgent = null;

    public Agent CurrentAgent
    {
        get => currentAgent;
        set => SetProperty(ref currentAgent, value);
    }

    public AgentGreetingViewModel(
        ISpaceTradersApi spaceTradersApi,
        IRegionManager regionManager)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.regionManager = regionManager;

        DispatcherTimer dispatcherTimer = new()
        {
            Interval = TimeSpan.FromSeconds(0.5)
        };
        dispatcherTimer.Tick += Timer_Tick;
        dispatcherTimer.Start();
    }

    private async void Timer_Tick(object sender, EventArgs e)
    {
        if (sender is not DispatcherTimer dispatcherTimer)
        {
            return;
        }
        if (dispatcherTimer.Interval == TimeSpan.FromSeconds(0.5))
        {
            dispatcherTimer.Stop();
            CurrentAgent = await this.spaceTradersApi.GetAgent();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            dispatcherTimer.Start();
            return;
        }
        dispatcherTimer.Stop();
        dispatcherTimer.Tick -= Timer_Tick;
        this.regionManager.Regions[RegionNames.SplashScreenRegion].RemoveAll();
        this.regionManager.RegisterViewWithRegion(RegionNames.MainMenuRegion, typeof(MainMenuView));
    }
}