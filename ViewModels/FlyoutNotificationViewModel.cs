using System;

using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Events;
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

    public NotificationTypes ToastNotificationType
    {
        get => toastNotificationType;
        set => SetProperty(ref toastNotificationType, value);
    }

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

    public FlyoutNotificationViewModel(IEventAggregator eventAggregator,
        IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<NotificationEvent>().Subscribe(SetInformation);
    }

    ~FlyoutNotificationViewModel()
    {
        this.eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(SetInformation);
    }

    private void SetInformation(NotificationEventArguments eventArguments)
    {
        this.eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(SetInformation);
        this.ToastNotificationType = eventArguments.ToastNotificationTypes;
        this.Header = eventArguments.ToastNotificationHeader;
        this.Message = eventArguments.ToastNotificationMessage;
    }

    internal void AnimationCompleted(FlyoutNotificationView view)
    {
        if (this.regionManager.Regions[RegionNames.FlyoutNotificationArea].Views.Contains(view))
        {
            this.regionManager.Regions[RegionNames.FlyoutNotificationArea].Remove(view);
        }
    }
}
