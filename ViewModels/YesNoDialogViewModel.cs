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
        get => this.header;
        set => this.SetProperty(ref this.header, value);
    }

    public string Message
    {
        get => this.message;
        set => this.SetProperty(ref this.message, value);
    }

    public DelegateCommand RejectDialogCommand => this.rejectDialogCommand ??= new DelegateCommand(this.RejectDialog);
    public DelegateCommand ConfirmDialogCommand => this.confirmDialogCommand ??= new DelegateCommand(this.ConfirmDialog);

    public YesNoDialogViewModel(
        IRegionManager regionManager,
        ISpaceTradersApi spaceTradersApi)
    {
        this.regionManager = regionManager;
        this.spaceTradersApi = spaceTradersApi;
    }

    private void ConfirmDialog()
    {
        this.regionManager.Regions[RegionNames.SplashScreenRegion].Remove(this.regionManager.Regions[RegionNames.SplashScreenRegion].Views.OfType<YesNoDialogView>().Single());
        this.confirmationAction(this.regionManager, this.spaceTradersApi);
    }

    private void RejectDialog()
    {
        this.regionManager.Regions[RegionNames.SplashScreenRegion].Remove(this.regionManager.Regions[RegionNames.SplashScreenRegion].Views.OfType<YesNoDialogView>().Single());
        this.rejectionAction(this.regionManager, this.spaceTradersApi);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.TryGetValue<string>("Message", out var messageString))
        {
            this.Message = messageString;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "Message");
        }
        if (navigationContext.Parameters.TryGetValue<string>("Header", out var headerString))
        {
            this.Header = headerString;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "Header");
        }
        if (navigationContext.Parameters.TryGetValue<Action<IRegionManager, ISpaceTradersApi>>("ConfirmationAction", out var navigationConfirmationAction))
        {
            this.confirmationAction = navigationConfirmationAction;
        }
        else
        {
            throw new ArgumentException("Missing Parameter", "ConfirmationAction");
        }
        if (navigationContext.Parameters.TryGetValue<Action<IRegionManager, ISpaceTradersApi>>("RejectionAction", out var navigationRejectionAction))
        {
            this.rejectionAction = navigationRejectionAction;
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
