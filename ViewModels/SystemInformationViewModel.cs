using System;

using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.Events;
using SpaceTradersWPF.Events.Models;

namespace SpaceTradersWPF.ViewModels;

internal class SystemInformationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;

    public SystemInformationViewModel(IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<SystemInformationEvent>().Subscribe(this.GetSystemInformationInformation);
    }

    ~SystemInformationViewModel()
    {
        this.eventAggregator.GetEvent<SystemInformationEvent>().Unsubscribe(this.GetSystemInformationInformation);
    }

    private void GetSystemInformationInformation(SystemInformationEventArguments eventInformation)
    {
        throw new NotImplementedException();
    }
}
