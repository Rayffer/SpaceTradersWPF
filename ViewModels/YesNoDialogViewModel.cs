using System;
using System.Linq;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class YesNoDialogViewModel : BindableBase, INavigationAware
{
    private string header;
    private string message;
    private DelegateCommand confirmDialogCommand;
    private DelegateCommand rejectDialogCommand;
    private Action<IRegionManager, ISpaceTradersApi> confirmationAction;
    private Action<IRegionManager, ISpaceTradersApi> rejectionAction;
    private readonly IRegionManager regionManager;
    private readonly ISpaceTradersApi spaceTradersApi;

    public string Header
    {
        get => header;
        set => SetProperty(ref header, value);
    }

    public string Message
    {
        get => message;
        set => SetProperty(ref message, value);
    }

    public DelegateCommand RejectDialogCommand => rejectDialogCommand ??= new DelegateCommand(RejectDialog);
    public DelegateCommand ConfirmDialogCommand => confirmDialogCommand ??= new DelegateCommand(ConfirmDialog);

    public YesNoDialogViewModel(
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
    }

    private void ConfirmDialog()
    {
        regionManager.Regions[RegionNames.SplashScreenRegion].Remove(regionManager.Regions[RegionNames.SplashScreenRegion].Views.OfType<YesNoDialogView>().Single());
        confirmationAction(regionManager, spaceTradersApi);
    }

    private void RejectDialog()
    {
        regionManager.Regions[RegionNames.SplashScreenRegion].Remove(regionManager.Regions[RegionNames.SplashScreenRegion].Views.OfType<YesNoDialogView>().Single());
        rejectionAction(regionManager, spaceTradersApi);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.TryGetValue<string>("Message", out var messageString))
        {
            Message = messageString;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "Message");
        }
        if (navigationContext.Parameters.TryGetValue<string>("Header", out var headerString))
        {
            Header = headerString;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "Header");
        }
        if (navigationContext.Parameters.TryGetValue<Action<IRegionManager, ISpaceTradersApi>>("ConfirmationAction", out var navigationConfirmationAction))
        {
            confirmationAction = navigationConfirmationAction;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "ConfirmationAction");
        }
        if (navigationContext.Parameters.TryGetValue<Action<IRegionManager, ISpaceTradersApi>>("RejectionAction", out var navigationRejectionAction))
        {
            rejectionAction = navigationRejectionAction;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "RejectionAction");
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}