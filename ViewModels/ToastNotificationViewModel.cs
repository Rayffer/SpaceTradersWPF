using Prism.Commands;
using System;

using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;
using SpaceTradersWPF.Types;
using System.Windows.Input;
using Prism.Regions;
using System.Linq;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF.ViewModels;

internal class ToastNotificationViewModel : BindableBase
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

    public ToastNotificationViewModel(IEventAggregator eventAggregator,
        IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<NotificationEvent>().Subscribe(this.SetInformation);
    }

    ~ToastNotificationViewModel()
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

    internal void AnimationCompleted(ToastNotificationView view)
    {
        this.regionManager.Regions[RegionNames.ToastNotificationArea].Remove(view);
    }
}
