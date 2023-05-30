using System.Windows;
using System.Windows.Controls;

using SpaceTradersWPF.ApiModels;

using SpaceTradersWPF.ViewModels;

namespace SpaceTradersWPF.Views;

/// <summary>
/// Interaction logic for SystemInformationView.xaml
/// </summary>
public partial class SystemInformationView : UserControl
{
    public SystemInformationView()
    {
        this.InitializeComponent();
    }

    private void Border_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement frameworkElement &&
            frameworkElement.DataContext is Waypoint waypoint)
        {
            (this.DataContext as SystemInformationViewModel).OpenWaypointInformationCommand.Execute(waypoint);
        }
    }
}
