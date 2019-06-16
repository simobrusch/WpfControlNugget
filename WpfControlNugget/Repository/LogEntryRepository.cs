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
    class LogEntryRepository : RepositoryBase<LogEntryModel>
    {
        public List<LogEntryModel> Logs { get; set; }
        public LogEntryModel _Logs { get; set; }

        public LogEntryRepository(string connectionString) : base(connectionString)
        {
            Logs = new List<LogEntryModel>();
        }

        public override LogEntryModel GetSingle<P>(P pkValue)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {TableName} WHERE id = {pkValue}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            _Logs = (new LogEntryModel(

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
        public override void Add(LogEntryModel newLogModelEntry)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "LogMessageAdd";
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
        public override void Delete(LogEntryModel entity)
        {
            throw new System.NotSupportedException();
        }
        public override void Update(LogEntryModel entity)
        {
            throw new System.NotSupportedException();
        }
        public override List<LogEntryModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
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
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {TableName} WHERE {whereCon}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogEntryModel(

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
        public override List<LogEntryModel> GetAll()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {TableName}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Logs.Add(new LogEntryModel(

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
        public override void CallStoredProcedure(LogEntryModel logModelEntry)
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
        public override string TableName => "v_logentries";
    }
}
