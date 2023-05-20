using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceTradersWPF.Views
{
    /// <summary>
    /// Interaction logic for ShipNavigationView.xaml
    /// </summary>
    public partial class ShipNavigationView : UserControl
    {
        public ShipNavigationView()
        {
            InitializeComponent();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
        }
    }
}
