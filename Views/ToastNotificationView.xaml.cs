using System;
using System.Windows.Controls;

using SpaceTradersWPF.ViewModels;

namespace SpaceTradersWPF.Views
{
    /// <summary>
    /// Interaction logic for ToastNotificationView.xaml
    /// </summary>
    public partial class ToastNotificationView : UserControl
    {
        public ToastNotificationView()
        {
            this.InitializeComponent();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            (this.DataContext as ToastNotificationViewModel).AnimationCompleted(this);
        }
    }
}
