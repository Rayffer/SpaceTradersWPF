using System;
using System.Threading;
using System.Windows;

using Prism.Ioc;
using Prism.Unity;

using RestSharp;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Events;
using SpaceTradersWPF.Mappers;
using SpaceTradersWPF.Repositories;
using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

using Unity;

namespace SpaceTradersWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return this.Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        RegisterDialogs(containerRegistry);
        RegisterEvents(containerRegistry);
        RegisterMappers(containerRegistry);
        RegisterServices(containerRegistry);
        RegisterRepositories(containerRegistry);
    }

    private static void RegisterRepositories(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IInformationRepository<Survey>, JsonFileRepository<Survey>>();
    }

    private static void RegisterDialogs(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<YesNoDialogView>();
    }

    private static void RegisterMappers(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ISpaceTradersApiMapper, SpaceTradersApiMapper>();
    }

    private static void RegisterServices(IContainerRegistry containerRegistry)
    {
        var client = new RestClient("https://api.spacetraders.io/v2/");
        containerRegistry.RegisterInstance(typeof(IRestClient), client);
        containerRegistry.Register<ISpaceTradersApi, SpaceTradersApi>();
        containerRegistry.Register<INotificationService, NotificationService>();
        containerRegistry.RegisterSingleton<IWaypointSurveyService, WaypointSurveyService>();
    }

    private static void RegisterEvents(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<WaypointInformationEvent>();
        containerRegistry.RegisterSingleton<SystemInformationEvent>();
        containerRegistry.RegisterSingleton<NotificationEvent>();
        containerRegistry.RegisterSingleton<ShipInformationEvent>();
    }
}
