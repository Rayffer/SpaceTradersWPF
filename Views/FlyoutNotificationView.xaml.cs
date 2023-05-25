using System;
using System.Windows.Controls;

using SpaceTradersWPF.ViewModels;

namespace SpaceTradersWPF.Views
{
    /// <summary>
    /// Interaction logic for FlyoutNotificationView.xaml
    /// </summary>
    public partial class FlyoutNotificationView : UserControl
    {
        public FlyoutNotificationView()
        {
            this.InitializeComponent();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            (this.DataContext as FlyoutNotificationViewModel).AnimationCompleted(this);
        }
    }
}
