﻿using System;
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

        private object selectedViewModel;

        public object SelectedViewModel

        {

            get { return selectedViewModel; }

            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }

        }

        public NavigationViewModel()
        {
            LogsCommand = new BaseCommand(OpenLogs);
            LocationsCommand = new BaseCommand(OpenLocations);
        }

        private void OpenLogs(object obj)

        {

            SelectedViewModel = new LogEntryViewModel();

        }

        private void OpenLocations(object obj)

        {

            SelectedViewModel = new LocationViewModel();

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