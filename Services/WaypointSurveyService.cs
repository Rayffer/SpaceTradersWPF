using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Repositories;

namespace SpaceTradersWPF.Services;

internal class WaypointSurveyService : IWaypointSurveyService
{
    private readonly IInformationRepository<Survey> informationRepository;
    private readonly CancellationTokenSource cancellationTokenSource;
    private bool finishTask;

    public WaypointSurveyService(
        IInformationRepository<Survey> informationRepository)
    {
        this.informationRepository = informationRepository;
        this.cancellationTokenSource = new CancellationTokenSource();
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            while (true)
            {
                if (this.finishTask)
                {
                    break;
                }
                this.DeleteExpiredSurveys();
                await Task.Delay(TimeSpan.FromMinutes(3), this.cancellationTokenSource.Token);
            }
        });
    }

    ~WaypointSurveyService()
    {
        this.finishTask = true;
        this.cancellationTokenSource.Cancel();
    }

    public Survey GetSurvey(string waypointSymbol)
    {
        return this.informationRepository.Store.Where(x => x.Symbol == waypointSymbol).LastOrDefault();
    }

    public void SaveSurveyDetails(params Survey[] surveyInformations)
    {
        this.informationRepository.SaveInformation(surveyInformations);
    }

    private void DeleteExpiredSurveys()
    {
        var nonExpiredSurveys = this.informationRepository.Store.Where(x => x.Expiration > DateTimeOffset.UtcNow).ToList();
        this.informationRepository.Store.Clear();
        this.informationRepository.SaveInformation(nonExpiredSurveys.ToArray());
    }
}
