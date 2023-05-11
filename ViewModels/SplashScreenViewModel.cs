using System.IO;
using System.Windows;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class SplashScreenViewModel : BindableBase, IRegionMemberLifetime
{
    private DelegateCommand<object> proceedIntoApplicationCommand = null;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;

    public SplashScreenViewModel(
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
        this.KeepAlive = true;
    }

    public DelegateCommand<object> ProceedIntoApplicationCommand => proceedIntoApplicationCommand ??= new DelegateCommand<object>(ProceedIntoApplication);

    public bool KeepAlive { get; }

    private void ProceedIntoApplication(object view)
    {
        regionManager.Regions[RegionNames.SplashScreenRegion].Remove(view);
        if (Directory.Exists("Data") &&
            File.Exists("Data/AccessToken.Token") &&
            MessageBox.Show("Do you want to proceed with the current agent?", "Continue previous session", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            this.spaceTradersApi.SetAccessTokenHeader(File.ReadAllText("Data/AccessToken.Token"));
            this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentGreetingView));

            return;
        }
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentSelectionView));
    }
}