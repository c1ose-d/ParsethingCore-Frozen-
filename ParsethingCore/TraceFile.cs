namespace ParsethingCore;

internal static class TraceFile
{
    public static void Set()
    {
        FileInfo trace = new("Trace.txt");
        trace.Create().Close();
        _ = Trace.Listeners.Add(new TextWriterTraceListener(trace.OpenWrite()));
        _ = Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        Trace.AutoFlush = true;
    }
}