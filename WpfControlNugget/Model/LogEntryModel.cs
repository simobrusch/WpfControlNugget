using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateCheckerLib;
using LinqToDB.Mapping;

namespace WpfControlNugget.Model
{
    [Table("v_logentries")]
    public class LogEntryModel : ModelBase<LogEntryModel>
    {
        [Column("id"), PrimaryKey, NotNull]
        public override int Id { get; set; }
        [Column("pod")]
        public string Pod { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("hostname")]
        public string Hostname { get; set; }
        [Column("severity")]
        public string Severity { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("message")]
        public string Message { get; set; }

        public LogEntryModel()
        {
            this.Severity = "1";
        }
        public LogEntryModel(int id, string pod, string location, string hostname, string severity, DateTime timestamp, string message)
        {
            this.Id = id;
            this.Pod = pod;
            this.Location = location;
            this.Hostname = hostname;
            this.Severity = severity;
            this.Timestamp = timestamp;
            this.Message = message;
        }
        public bool Equals(LogEntryModel secondLogModel)
        {
            if (Object.ReferenceEquals(null, secondLogModel)) return false;
            if (Object.ReferenceEquals(this, secondLogModel)) return true;

            return String.Equals(Severity, secondLogModel.Severity) && String.Equals(Message, secondLogModel.Message);
        }
        public override bool Equals(object value)
        {
            return Equals(value as LogEntryModel);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Message) ? Message.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Severity) ? Severity.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
