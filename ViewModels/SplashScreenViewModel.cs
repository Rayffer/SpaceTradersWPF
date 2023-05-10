using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;

using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class SplashScreenViewModel
{
    private DelegateCommand<object> proceedIntoApplicationCommand = null;
    private readonly IContainerExtension containerExtension;
    private readonly IRegionManager regionManager;

    public SplashScreenViewModel(
        IContainerExtension containerExtension,
        IRegionManager regionManager)
    {
        this.containerExtension = containerExtension;
        this.regionManager = regionManager;
    }

    public DelegateCommand<object> ProceedIntoApplicationCommand => proceedIntoApplicationCommand ??= new DelegateCommand<object>(ProceedIntoApplication);

    private void ProceedIntoApplication(object view)
    {
        regionManager.Regions["SplashScreenRegion"].Remove(view);
        this.regionManager.RegisterViewWithRegion("SplashScreenRegion", typeof(AgentSelectionView));
    }
}