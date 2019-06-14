using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateCheckerLib;

namespace WpfControlNugget.Model
{
    public class LogModel :IEntity
    {
        public int Id { get; set; }
        public string Pod { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public string Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public LogModel()
        {
            this.Severity = "1";
        }
        public LogModel(int id, string pod, string location, string hostname, string severity, DateTime timestamp, string message)
        {
            this.Id = id;
            this.Pod = pod;
            this.Location = location;
            this.Hostname = hostname;
            this.Severity = severity;
            this.Timestamp = timestamp;
            this.Message = message;
        }
        public bool Equals(LogModel secondLogModel)
        {
            if (Object.ReferenceEquals(null, secondLogModel)) return false;
            if (Object.ReferenceEquals(this, secondLogModel)) return true;

            return String.Equals(Severity, secondLogModel.Severity) && String.Equals(Message, secondLogModel.Message);
        }
        public override bool Equals(object value)
        {
            return Equals(value as LogModel);
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
