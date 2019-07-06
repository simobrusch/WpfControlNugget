using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfControlNugget.ViewModel.Commands;

namespace WpfControlNugget.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        public ICommand LogsCommand { get; set; }
        public ICommand LocationsCommand { get; set; }
        public ICommand CustomersCommand { get; set; }

        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public NavigationViewModel()
        {
            LogsCommand = new BaseCommand(OpenLogs);
            LocationsCommand = new BaseCommand(OpenLocations);
            CustomersCommand = new BaseCommand(OpenCustomers);
        }

        private void OpenLogs(object obj)
        {
            SelectedViewModel = new LogEntryViewModel();
        }

        private void OpenLocations(object obj)
        {
            SelectedViewModel = new LocationViewModel();
        }
        private void OpenCustomers(object obj)
        {
            SelectedViewModel = new CustomerViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
