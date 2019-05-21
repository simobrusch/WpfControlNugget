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
        public int id { get; set; }
        public string pod { get; set; }
        public string location { get; set; }
        public string hostname { get; set; }
        public string severity { get; set; }
        public DateTime timestamp { get; set; }
        public string message { get; set; }

        public LogModel(int id, string pod, string location, string hostname, string severity, DateTime timestamp, string message)
        {
            this.id = id;
            this.pod = pod;
            this.location = location;
            this.hostname = hostname;
            this.severity = severity;
            this.timestamp = timestamp;
            this.message = message;
        }
        public bool Equals(LogModel secondLogModel)
        {
            if (Object.ReferenceEquals(null, secondLogModel)) return false;
            if (Object.ReferenceEquals(this, secondLogModel)) return true;

            return String.Equals(severity, secondLogModel.severity) && String.Equals(message, secondLogModel.message);
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
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, message) ? message.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, severity) ? severity.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
