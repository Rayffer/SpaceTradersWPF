using Prism.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace SpaceTradersWPF.ViewModels;

internal class ShipCargoTransferViewModel
{
    private DelegateCommand cancelNavigationCommand;
    private DelegateCommand performCargoTransferCommand;

    public ICommand CancelNavigationCommand => cancelNavigationCommand ??= new DelegateCommand(CancelNavigation);
    public ICommand PerformCargoTransferCommand => performCargoTransferCommand ??= new DelegateCommand<object[]>(PerformCargoTransfer);

    private void CancelNavigation()
    {
    }

    private void PerformCargoTransfer(object[] arguments)
    {
    }
}
