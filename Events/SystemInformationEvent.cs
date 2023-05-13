using Prism.Events;

namespace SpaceTradersWPF.Events;

internal class SystemInformation
{
    public string SystemSymbol { get; internal set; }
}

internal class SystemInformationEvent : PubSubEvent<SystemInformation>
{
}