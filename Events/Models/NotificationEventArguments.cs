using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Events.Models;

internal class NotificationEventArguments
{
    public NotificationTypes ToastNotificationTypes { get; internal set; }
    public string ToastNotificationHeader { get; internal set; }
    public string ToastNotificationMessage { get; internal set; }
}