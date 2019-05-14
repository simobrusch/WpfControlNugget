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

        public LogViewModel()
        {
            TxtConnectionString = "Server=localhost;Database=;Uid=root;Pwd=;";
        }

        public ObservableCollection<Model.LogModel> Logs { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand _btnLoadDataClick;
        public ICommand _btnConfirmdataClick { set; get; }
        public ICommand _btnAdddataClick { set; get; }

        public string TxtConnectionString
        {
            get
            {
                return _txtConnectionString;
            }
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

        public void BtnLoadData_Click()
        {
            try
            {
                using (var conn = new MySqlConnection(this.TxtConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT idLogging, location, Hostname, severity, zeit, message FROM v_logentries ORDER BY zeit", conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogModel(
                                                    reader.GetInt32("idLogging"),
                                                    reader.GetString("pod"),
                                                    reader.GetString("location"),
                                                    reader.GetString("hostname"),
                                                    reader.GetString("severity"),
                                                    reader.GetDateTime("zeit"),
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
                    cmd.CommandText = "SELECT idLogging, location, Hostname, severity, zeit, message FROM v_logentries  WHERE `Quitiert`=false ORDER BY zeit";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Logs.Add(new LogModel(
                            reader.GetInt32("idLogging"),
                            reader.GetString("pod"),
                            reader.GetString("location"),
                            reader.GetString("hostname"),
                            reader.GetString("severity"),
                            reader.GetDateTime("zeit"),
                            reader.GetString("message")
                            ));
                    }
                }
            }
        }

        ////Method not working System.InvalidCastException
        //private void btnLogClear_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (var conn = new MySqlConnection(this.txtConnectionString.Text))
        //        {
        //            conn.Open();
        //            foreach (DataGridRow row in dataGridCustomers.SelectedItems)
        //            {
        //                using (var cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = "LogClear";
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        this.LoadData();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        ////Method not working MySQLexception
        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        using (var conn = new MySqlConnection(this.txtConnectionString.Text))
        //        {
        //            using (MySqlCommand cmd = new MySqlCommand("LogMessageAdd", conn))
        //            {
        //                conn.Open();
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
        //                {
        //                    DataSet ds = new DataSet();
        //                    dataGridCustomers.DataContext = ds;
        //                    cmd.Parameters.Add(new MySqlParameter("iHostname", conn));
        //                    cmd.Parameters.Add(new MySqlParameter("iSeverity", conn));
        //                    cmd.Parameters.Add(new MySqlParameter("iMessage", conn));
        //                    cmd.Parameters.Add(new MySqlParameter("iTimeStamp", conn));
        //                    adp.Fill(ds, "LoadDataBinding");
        //                }
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
