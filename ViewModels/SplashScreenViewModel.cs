using System;
using System.IO;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class SplashScreenViewModel : BindableBase
{
    private DelegateCommand<object> proceedIntoApplicationCommand = null;
    private readonly IRegionManager regionManager;

    public SplashScreenViewModel(
        IRegionManager regionManager)
    {
        this.regionManager = regionManager;
    }

    public DelegateCommand<object> ProceedIntoApplicationCommand => this.proceedIntoApplicationCommand ??= new DelegateCommand<object>(this.ProceedIntoApplication);

    private void ProceedIntoApplication(object view)
    {
        this.regionManager.Regions[RegionNames.SplashScreenRegion].Remove(view);
        if (Directory.Exists("Data") &&
            File.Exists("Data/AccessToken.Token"))
        {
            var navigationParameters = new NavigationParameters
            {
                { "Message", "Do you want to proceed with the current agent?" },
                { "Header", "Continue previous session" },
                { "ConfirmationAction", new Action<IRegionManager, ISpaceTradersApi>((actionRegionManager, actionSpaceTradersApi) =>
                    {
                        actionSpaceTradersApi.SetAccessTokenHeader(File.ReadAllText("Data/AccessToken.Token"));
                        actionRegionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentGreetingView));
                    })
                },
                { "RejectionAction", new Action<IRegionManager, ISpaceTradersApi>((actionRegionManager, actionSpaceTradersApi) =>
                    {
                        actionRegionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentSelectionView));
                    })
                },
            };
            this.regionManager.RequestNavigate(RegionNames.SplashScreenRegion, "YesNoDialogView", navigationParameters);

            return;
        }
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(AgentSelectionView));
    }
}
