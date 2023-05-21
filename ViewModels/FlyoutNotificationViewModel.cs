using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Types;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class FlyoutNotificationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionManager regionManager;
    private NotificationTypes toastNotificationType;
    private string header;
    private string message;

    public NotificationTypes NotificationType
    {
        get => this.toastNotificationType;
        set => this.SetProperty(ref this.toastNotificationType, value);
    }

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

    public FlyoutNotificationViewModel(IEventAggregator eventAggregator,
        IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<NotificationEvent>().Subscribe(this.SetInformation);
    }

    ~FlyoutNotificationViewModel()
    {
        this.eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(this.SetInformation);
    }

    private void SetInformation(NotificationEventArguments eventArguments)
    {
        this.eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(this.SetInformation);
        this.NotificationType = eventArguments.ToastNotificationTypes;
        this.Header = eventArguments.ToastNotificationHeader;
        this.Message = eventArguments.ToastNotificationMessage;
    }

    internal void AnimationCompleted(FlyoutNotificationView view)
    {
        if (!this.regionManager.Regions[RegionNames.FlyoutNotificationArea].Views.Contains(view))
        {
            return;
        }

        this.regionManager.Regions[RegionNames.FlyoutNotificationArea].Remove(view);
    }
}
