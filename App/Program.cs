﻿using Database.Entities;

namespace App;

internal class Program
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void Main(string[] args)
    {
        Console.Title = "Parsething Core";
        TraceFile.Set();
        Trace.WriteLine($"Session started at {DateTime.Now}.\n");
        while (true)
        {
            Sources sources = new();
            foreach (Source source in sources)
            {
                try
                {
                    ParsethingContext db = new();
                    Procurement? def = null;
                    try
                    {
                        def = db.Procurements.Where(p => p.Number == source.Number).First();
                    }
                    catch { }
                    if (def == null)
                    {
                        _ = db.Procurements.Add(source);
                    }
                    else
                    {
                        def.SourceStateId = source.SourceStateId;
                        def.LawId = source.LawId;
                        def.Object = source.Object;
                        def.InitialPrice = source.InitialPrice;
                        def.OrganizationId = source.OrganizationId;
                        def.IsSuitable = source.IsSuitable;
                        if (source.IsGetted == true)
                        {
                            def.MethodId = source.MethodId;
                            def.PlatformId = source.PlatformId;
                            def.Location = source.Location;
                            def.StartDate = source.StartDate;
                            def.Deadline = source.Deadline;
                            def.TimeZoneId = source.TimeZoneId;
                            def.Securing = source.Securing;
                            def.Enforcement = source.Enforcement;
                            def.Warranty = source.Warranty;
                        }
                        _ = db.Procurements.Update(def);
                    }
                    _ = db.SaveChanges();
                    Trace.WriteLine($"{DateTime.Now}\n{source.Number}\nIs saved successfully.\n");
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"{DateTime.Now}\n{source.Number}\n{e.InnerException?.Message}\n");
                }
            }
        }
    }
}