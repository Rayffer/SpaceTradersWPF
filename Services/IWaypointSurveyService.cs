using SpaceTradersWPF.ApiModels;

namespace SpaceTradersWPF.Services;

internal interface IWaypointSurveyService
{
    Survey GetSurvey(string waypointSymbol);

    void SaveSurveyDetails(params Survey[] surveyInformation);
}
