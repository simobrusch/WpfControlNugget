using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.Model
{
    public class LogModel
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
    }
}
