using SpaceTradersWPF.Models;

namespace SpaceTradersWPF.Services;

internal interface ISpaceTradersApi
{
    AgentResponse RegisterAgent(string symbol, string faction);

    void SetAccessTokenHeader(string token);
}