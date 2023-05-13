using System;

using Prism.Events;
using Prism.Mvvm;

using SpaceTradersWPF.Events;

namespace SpaceTradersWPF.ViewModels;

internal class SystemInformationViewModel : BindableBase
{
    private readonly IEventAggregator eventAggregator;

    public SystemInformationViewModel(IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
        this.eventAggregator.GetEvent<SystemInformationEvent>().Subscribe(GetSystemInformationInformation);
    }

    private void GetSystemInformationInformation(SystemInformation eventInformation)
    {
        throw new NotImplementedException();
    }
}