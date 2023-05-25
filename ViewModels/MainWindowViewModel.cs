using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class MainWindowViewModel : BindableBase
{
    private readonly IRegionManager regionManager;

    public MainWindowViewModel(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(SplashScreenView));
    }
}
