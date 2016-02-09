using System.Diagnostics.Tracing;


namespace SampleLogApplication
{
    [EventSource(Name = "SampleLog")]
    public sealed class LogEventSource : EventSource
    {
        public LogEventSource()
        {
        }

        [Event(72, Level = EventLevel.Error, Opcode = EventOpcode.Start, Task = (EventTask)42)]
        public void SampleLogFunction()
        {
            if (IsEnabled())
            {
                WriteEvent(72);
            }
        }
    }
}
