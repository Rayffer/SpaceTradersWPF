using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal interface INotificationService
{
    void ShowToastNotification(string headerMessage, string bodyMessage, NotificationTypes notificationType, bool clearOtherNotifications = true);

    void ShowFlyoutNotification(string headerMessage, string bodyMessage, NotificationTypes notificationType, bool clearOtherNotifications = false);
}
