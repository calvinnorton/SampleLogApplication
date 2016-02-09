namespace SampleLogApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EventLogListener listener = new EventLogListener())
            {
                LogEventSource eventSource = new LogEventSource();
                eventSource.SampleLogFunction();
            }
        }
    }
}
