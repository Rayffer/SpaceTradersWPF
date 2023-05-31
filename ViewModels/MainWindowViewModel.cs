using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Views;

using System.Windows.Input;

namespace SpaceTradersWPF.ViewModels;

internal class MainWindowViewModel : BindableBase
{
    private readonly IRegionManager regionManager;

    public MainWindowViewModel(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        this.regionManager.RegisterViewWithRegion(RegionNames.SplashScreenRegion, typeof(SplashScreenView));
    }

    private DelegateCommand closeDetailViewCommand;
    public ICommand CloseDetailViewCommand => this.closeDetailViewCommand ??= new DelegateCommand(this.CloseDetailView);

    private void CloseDetailView()
    {
        this.regionManager.Regions[RegionNames.DetailViewAreaRegion].RemoveAll();
    }
}
