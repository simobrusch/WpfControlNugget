using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    class LogModelRepository : RepositoryBase<LogModel>
    {
        public List<LogModel> Logs { get; set; }
        public LogModel _Logs { get; set; }
        

        public LogModelRepository(string connectionString) : base(connectionString)
        {
        }

        public override LogModel GetSingle<P>(P pkValue)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT id, pod, location, hostname, severity, timestamp, message FROM " + TableName + "WHERE id =" + pkValue, conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            _Logs = (new LogModel(

                                reader.GetInt32("id"),
                                reader.GetValue(reader.GetOrdinal("pod")) as string,
                                reader.GetValue(reader.GetOrdinal("location")) as string,
                                reader.GetValue(reader.GetOrdinal("hostname")) as string,
                                reader.GetString("severity"),
                                reader.GetDateTime("timestamp"),
                                reader.GetValue(reader.GetOrdinal("message")) as string
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return _Logs;
        }


        public override void Add(LogModel entity)
        {
            throw new System.NotSupportedException();
        }

        public override void Delete(LogModel entity)
        {
            throw new System.NotSupportedException();
        }

        public override void Update(LogModel entity)
        {
            throw new System.NotSupportedException();
        }

        public override List<LogModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT id, pod, location, hostname, severity, timestamp, message FROM " + TableName + "WHERE " + whereCon, conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogModel(

                                reader.GetInt32("id"),
                                reader.GetValue(reader.GetOrdinal("pod")) as string,
                                reader.GetValue(reader.GetOrdinal("location")) as string,
                                reader.GetValue(reader.GetOrdinal("hostname")) as string,
                                reader.GetString("severity"),
                                reader.GetDateTime("timestamp"),
                                reader.GetValue(reader.GetOrdinal("message")) as string
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return Logs;
        }

        public override List<LogModel> GetAll()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries", conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogModel(

                                reader.GetInt32("id"),
                                reader.GetValue(reader.GetOrdinal("pod")) as string,
                                reader.GetValue(reader.GetOrdinal("location")) as string,
                                reader.GetValue(reader.GetOrdinal("hostname")) as string,
                                reader.GetString("severity"),
                                reader.GetDateTime("timestamp"),
                                reader.GetValue(reader.GetOrdinal("message")) as string
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return Logs;
        }
        public override string TableName => "v_logentries";

        public void BtnLogClear_Click(LogModel logModelEntry)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "LogClear";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_logentries_id", logModelEntry.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void BtnAdd_Click(LogModel newLogModelEntry)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("LogMessageAdd", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@i_pod", MySqlDbType.String).Value = newLogModelEntry.Pod;
                        cmd.Parameters.Add("@i_hostname", MySqlDbType.String).Value = newLogModelEntry.Hostname;
                        cmd.Parameters.Add("@i_severity", MySqlDbType.Int32).Value = newLogModelEntry.Severity;
                        cmd.Parameters.Add("@i_message", MySqlDbType.String).Value = newLogModelEntry.Message;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
    }
}
