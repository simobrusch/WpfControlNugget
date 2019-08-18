using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LinqToDB.Data;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class LogEntryRepository : RepositoryBase<LogEntryModel>
    {
        public LogEntryRepository() : base()
        {

        }
        /// <summary>
        /// Ermöglicht das hinzufügen von neuen LogEntries, indem die Stored Procedure "LogMessageAdd" ausgeführt wird.
        /// Die Stored Procedure hat 4 Parameter.
        /// </summary>
        /// <param name="newLogModelEntry"></param>
        public void ExecuteLogMessageAdd(LogEntryModel newLogModelEntry)
        {
            using (var dataConn = new DataConnection(ProviderName))
            {
                try
                {
                    var dataParams = new DataParameter[4];
                    dataParams[0] = new DataParameter("@i_pod", newLogModelEntry.Pod);
                    dataParams[1] = new DataParameter("@i_hostname", newLogModelEntry.Hostname);
                    dataParams[2] = new DataParameter("@i_severity", newLogModelEntry.Severity);
                    dataParams[3] = new DataParameter("@i_message", newLogModelEntry.Message);
                    dataConn.QueryProc<LogEntryModel>("LogMessageAdd", dataParams);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Ermöglicht das quittieren eines LogEntrys indem die Stored Procedure "LogClear" aufgerufen wird. 
        /// </summary>
        /// <param name="logModelEntry"></param>
        public void ExecuteLogClear(LogEntryModel logModelEntry)
        {
            using (var dataConn = new DataConnection(ProviderName))
            {
                try
                {
                    dataConn.QueryProc<LogEntryModel>("LogClear", new DataParameter("@_logentries_id", logModelEntry.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public override void Delete(LogEntryModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.logs.Find(entity.Id);
                    dataCtx.logs.Remove(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        public override void Update(LogEntryModel entity)
        {
            using (var dataCtx = new InventarisierungsloesungEntitiesNew())
            {
                try
                {
                    var pkValue = dataCtx.logs.Find(entity.Id);
                    dataCtx.logs.Attach(pkValue);
                    dataCtx.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
    }
}
