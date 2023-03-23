namespace App
{
    internal static class TraceFile
    {
        public static void Set()
        {
            FileInfo trace = new("Trace.txt");
            trace.Create().Close();
            Trace.Listeners.Add(new TextWriterTraceListener(trace.OpenWrite()));
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
        }
    }
}
