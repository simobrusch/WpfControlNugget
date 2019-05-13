using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfControlNugget.ViewModel.Commands
{
    class ButtonCommands :ICommand
    {
        public LogViewModel LogViewModel;
        public ButtonCommands(LogViewModel logVieWModel)
        {
            this.LogViewModel = logVieWModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //this.LogViewModel.BtnLoadData_Click();
        }

        public event EventHandler CanExecuteChanged;
    }
}
