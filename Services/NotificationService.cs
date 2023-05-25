using System;

using Prism.Events;
using Prism.Regions;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Types;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.Services;

internal class NotificationService : INotificationService
{
    private readonly IRegionManager regionManager;
    private readonly IEventAggregator eventAggregator;

    public NotificationService(
        IRegionManager regionManager,
        IEventAggregator eventAggregator)
    {
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
    }

    public void ShowFlyoutNotification(string headerMessage, string bodyMessage, NotificationTypes notificationType, bool clearOtherNotifications = false)
    {
        throw new NotImplementedException();
    }

    public void ShowToastNotification(string headerMessage, string bodyMessage, NotificationTypes notificationType, bool clearOtherNotifications = false)
    {
        if (clearOtherNotifications)
        {
            this.regionManager.Regions[RegionNames.ToastNotificationArea].RemoveAll();
        }

        this.regionManager.RegisterViewWithRegion(RegionNames.ToastNotificationArea, typeof(ToastNotificationView));
        this.eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArguments
        {
            ToastNotificationHeader = headerMessage,
            ToastNotificationMessage = bodyMessage,
            ToastNotificationTypes = notificationType
        });
    }
}
