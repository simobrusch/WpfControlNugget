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
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;
using WpfControlNugget.ViewModel.Commands;

namespace WpfControlNugget.ViewModel
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private string _txtConnectionString;
        private string _enterPod;
        private string _enterHostname;
        private int _enterSeverity;
        private string _enterMessage;

        private ICommand _btnLoadDataClick;
        private ICommand _btnConfirmdataClick;
        private ICommand _btnAdddataClick;
        private ICommand _btnFindDuplicatesClick;

        public ObservableCollection<LogModel> Logs { get; set; }
        public ObservableCollection<SeverityComboBoxItem> SeverityComboBox { get; set; }

        public LogViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";
            _enterSeverity = 1;

            Logs = new ObservableCollection<LogModel>();
            SeverityComboBox = new ObservableCollection<SeverityComboBoxItem>(){
                new SeverityComboBoxItem(){Id=1, Severity= 1},
                new SeverityComboBoxItem(){Id=2, Severity= 2},
                new SeverityComboBoxItem(){Id=3, Severity= 3}
            };
        }
        public LogModel MySelectedItem { get; set; }
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
        public string EnterPod
        {
            get => _enterPod;
            set
            {
                _enterPod = value;
                OnPropertyChanged(nameof(EnterPod));
            }
        }

        public string EnterHostname
        {
            get => _enterHostname;
            set
            {
                _enterHostname = value;
                OnPropertyChanged(nameof(EnterHostname));
            }
        }
        public int EnterSeverity
        {
            get => _enterSeverity;
            set
            {
                _enterSeverity = value;
                OnPropertyChanged(nameof(EnterSeverity));
            }
        }
        public string EnterMessage
        {
            get => _enterMessage;
            set
            {
                _enterMessage = value;
                OnPropertyChanged(nameof(EnterMessage));
            }
        }
        public ICommand BtnFindDuplicatesClick
        {
            get
            {
                return _btnFindDuplicatesClick ?? (_btnFindDuplicatesClick = new RelayCommand(
                           x =>
                           {
                               BtnAdd_Click();
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
                Logs.Clear();
                using (var conn = new MySqlConnection(TxtConnectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries ORDER BY timestamp", conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogModel(
                                reader.GetInt32("id"),
                                reader.GetString("pod"),
                                reader.GetString("location"),
                                reader.GetString("hostname"),
                                reader.GetString("severity"),
                                reader.GetDateTime("timestamp"),
                                reader.GetString("message")
                            ));
                        }
                    }
                }
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
                using (var conn = new MySqlConnection(TxtConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "LogClear";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_logentries_id", MySelectedItem.id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
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
                using (var conn = new MySqlConnection(TxtConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("LogMessageAdd", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@i_pod", MySqlDbType.String).Value = EnterPod;
                        cmd.Parameters.Add("@i_hostname", MySqlDbType.String).Value = EnterHostname;
                        cmd.Parameters.Add("@i_severity", MySqlDbType.Int32).Value = EnterSeverity;
                        cmd.Parameters.Add("@i_message", MySqlDbType.String).Value = EnterMessage;

                        cmd.ExecuteNonQuery();
                    }
                    LoadData();
                }
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
