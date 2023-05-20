using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class ShipNavigationViewModel : BindableBase
{
    private bool closeRequested;
    private readonly ISpaceTradersApi spaceTradersApi;
    private readonly IRegionManager regionManager;

    public bool CloseRequested
    {
        get => this.closeRequested;
        set => SetProperty(ref this.closeRequested, value);
    }

    public ShipNavigationViewModel(ISpaceTradersApi spaceTradersApi,
        IRegionManager regionManager)
    {
        this.spaceTradersApi = spaceTradersApi;
        this.regionManager = regionManager;
    }

    internal void RemoveElementAnimationCompleted(FlyoutNotificationView view)
    {
        if (!this.regionManager.Regions[RegionNames.DialogAreaRegion].Views.Contains(view))
        {
            return;
        }

        this.regionManager.Regions[RegionNames.DialogAreaRegion].Remove(view);
    }
}
