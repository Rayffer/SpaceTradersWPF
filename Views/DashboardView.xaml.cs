using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ViewModels;

namespace SpaceTradersWPF.Views;

/// <summary>
/// Interaction logic for DashboardView.xaml
/// </summary>
public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        this.InitializeComponent();
    }

    private void Border_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement frameworkElement &&
            frameworkElement.DataContext is Ship ship)
        {
            (this.DataContext as DashboardViewModel).OpenShipInformation.Execute(ship);
        }
    }
}
