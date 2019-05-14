using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
        private ICommand _btnLoadDataClick;
        private ICommand _btnConfirmdataClick;
        private ICommand _btnAdddataClick;

        public LogViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";
            Logs = new ObservableCollection<LogModel>();
        }

        public ObservableCollection<Model.LogModel> Logs { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public string TxtConnectionString
        {
            get => _txtConnectionString;
            set
            {
                _txtConnectionString = value;
                OnPropertyChanged(nameof(TxtConnectionString));
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
                               btnAdd_Click();
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
                this.Logs.Clear();
                using (var conn = new MySqlConnection(this.TxtConnectionString))
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
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadData()
        {
            this.Logs.Clear();
            using (var conn = new MySqlConnection(this.TxtConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries  WHERE `Quitiert`=false ORDER BY timestamp";
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

        ////Method not working System.InvalidCastException
        private void BtnLogClear_Click()
        {
            try
            {
                using (var conn = new MySqlConnection(this.TxtConnectionString))
                {
                    //conn.Open();
                    //foreach (DataGridRow row in dataGridCustomers.SelectedItems)
                    //{
                    //    using (var cmd = conn.CreateCommand())
                    //    {
                    //        cmd.CommandText = "LogClear";
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}
                }
                this.LoadData();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ////Method not working MySQLexception
        private void btnAdd_Click()
        {
            try
            {
                using (var conn = new MySqlConnection(this.TxtConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("LogMessageAdd", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            //dataGridCustomers.DataContext = ds;
                            cmd.Parameters.Add(new MySqlParameter("iHostname", conn));
                            cmd.Parameters.Add(new MySqlParameter("iSeverity", conn));
                            cmd.Parameters.Add(new MySqlParameter("iMessage", conn));
                            cmd.Parameters.Add(new MySqlParameter("iTimeStamp", conn));
                            adp.Fill(ds, "LoadDataBinding");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
