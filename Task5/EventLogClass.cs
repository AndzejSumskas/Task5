using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class EventLogClass
    {
        internal void LogInfo(string eventDescription)
        {
            string Event = eventDescription;

            using (EventLog eventLog = new EventLog("Application"))
            {

                eventLog.Source = "Application";
                eventLog.WriteEntry(Event, EventLogEntryType.Information);
            }
        }

        internal void LogError(string errorDescription)
        {
            string Event = errorDescription;

            using (EventLog eventLog = new EventLog("Application"))
            {

                eventLog.Source = "Application";
                eventLog.WriteEntry(Event, EventLogEntryType.Error);
            }
        }
    }
}
