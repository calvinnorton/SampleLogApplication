using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace SampleLogApplication
{
    public class EventLogListener : EventListener
    {
        private const string EventLogNameKey = "EventLog.Name";
        private const string EventLogSourceKey = "EventLog.Source";
        private const string ApplicationLogIdentifier = "Application";

        private readonly EventLog _eventLog;

        public EventLogListener()
        {
            var eventLogName = "SampleLog";
            var eventLogSource = "SampleLog";
            _eventLog = new EventLog(eventLogName, ".", eventLogSource);

            try
            {
                _eventLog.WriteEntry("Logging service starting.");
            }
            catch (System.Security.SecurityException)
            {
                EventLog appLog = new EventLog(ApplicationLogIdentifier);
                appLog.Source = ApplicationLogIdentifier;
                appLog.WriteEntry( "CannotWriteDueToAdminPrivileges", EventLogEntryType.Error);
                System.Console.WriteLine("CannotWriteDueToAdminPrivileges");
                throw;
            }
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            this.EnableEvents(eventSource, EventLevel.LogAlways);
            base.OnEventSourceCreated(eventSource);
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            var test = new EventInstance(eventData.EventId, (int)eventData.Task, EventLogEntryType.Warning);
            _eventLog.WriteEvent(test, new string[] { "OpCode=1" });

            _eventLog.WriteEntry("Event Occurring", EventLogEntryType.Information, eventData.EventId, (short)eventData.Task);
        }
    }
}
