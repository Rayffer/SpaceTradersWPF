using System.Windows;

using Prism.Ioc;
using Prism.Unity;

using RestSharp;

using SpaceTradersWPF.Services;
using SpaceTradersWPF.Views;

namespace SpaceTradersWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        var client = new RestClient("https://api.spacetraders.io/v2/");
        containerRegistry.RegisterInstance(typeof(IRestClient), client);
        containerRegistry.Register<ISpaceTradersApi, SpaceTradersApi>();
    }
}