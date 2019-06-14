using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DuplicateCheckerLib;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;
using WpfControlNugget.Repository;
using WpfControlNugget.ViewModel.Commands;

namespace WpfControlNugget.ViewModel
{
    public class LogEntryViewModel : INotifyPropertyChanged
    {
        private string _txtConnectionString;
        private readonly DuplicateChecker _dupChecker;

        private ICommand _btnLoadDataClick;
        private ICommand _btnConfirmdataClick;
        private ICommand _btnAdddataClick;
        private ICommand _btnFindDuplicatesClick;

        public List<LogEntryModel> Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                OnPropertyChanged("Logs");
            }
        }
        private List<LogEntryModel> _logs;
        public LogEntryModel NewLogModelEntry { get; set; }
        public ObservableCollection<SeverityComboBoxItem> SeverityComboBox { get; set; }

        public LogEntryViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";

            Logs = new List<LogEntryModel>();
            NewLogModelEntry = new LogEntryModel();
            SeverityComboBox = new ObservableCollection<SeverityComboBoxItem>(){
                new SeverityComboBoxItem(){Id=1, Severity= 1},
                new SeverityComboBoxItem(){Id=2, Severity= 2},
                new SeverityComboBoxItem(){Id=3, Severity= 3}
            };
            _dupChecker = new DuplicateChecker();
        }
        public LogEntryModel MySelectedItem { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public SeverityComboBoxItem SetSeverity { get; set; }

        public string TxtConnectionString
        {
            get => _txtConnectionString;
            set
            {
                _txtConnectionString = value;
                OnPropertyChanged(nameof(TxtConnectionString));
            }
        }
        public ICommand BtnFindDuplicatesClick
        {
            get
            {
                return _btnFindDuplicatesClick ?? (_btnFindDuplicatesClick = new RelayCommand(
                           x =>
                           {
                               BtnFindDuplicates_Click();
                           }));
            }
        }
        public ICommand BtnLoadDataClick
        {
            get
            {
                return _btnLoadDataClick ?? (_btnLoadDataClick = new RelayCommand(
                           x =>
                           {
                               BtnLoadData_Click();
                           }));
            }
        }
        public ICommand BtnAddDataClick
        {
            get
            {
                return _btnAdddataClick ?? (_btnAdddataClick = new RelayCommand(
                           x =>
                           {
                               BtnAdd_Click();
                           }));
            }
        }
        public ICommand BtnConfirmDataClick
        {
            get
            {
                return _btnConfirmdataClick ?? (_btnConfirmdataClick = new RelayCommand(
                           x =>
                           {
                               BtnLogClear_Click();
                           }));
            }
        }
        public List<LogEntryModel> BtnFindDuplicates_Click()
        {
            var logModelRepository = new LogModelRepository(TxtConnectionString);
            this.Logs = logModelRepository.GetAll();
            var dupList = _dupChecker.FindDuplicates(Logs);
            Logs = new List<LogEntryModel>(dupList.Cast<LogEntryModel>());

            return Logs;
        }
        public void BtnLoadData_Click()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void LoadData()
        {
            try
            {
                var logModelRepository = new LogModelRepository(TxtConnectionString);
                this.Logs = logModelRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }

        }
        private void BtnLogClear_Click()
        {
            if (MySelectedItem == null) return;
            try
            {
                var logModelRepository = new LogModelRepository(TxtConnectionString);
                logModelRepository.CallStoredProcedure(MySelectedItem);
                this.Logs = logModelRepository.GetAll();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BtnAdd_Click()
        {
            try
            {
                var logModelRepository = new LogModelRepository(TxtConnectionString);
                logModelRepository.Add(this.NewLogModelEntry);
                this.Logs = logModelRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
