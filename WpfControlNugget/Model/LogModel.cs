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
        public int idLogging { get; set; }
        public string pod { get; set; }
        public string location { get; set; }
        public string hostname { get; set; }
        public string severity { get; set; }
        public DateTime zeit { get; set; }
        public string message { get; set; }

        public LogModel(int idLogging, string pod, string location, string hostname, string severity, DateTime zeit, string message)
        {
            this.idLogging = idLogging;
            this.pod = pod;
            this.location = location;
            this.hostname = hostname;
            this.severity = severity;
            this.zeit = zeit;
            this.message = message;
        }
    }
}
