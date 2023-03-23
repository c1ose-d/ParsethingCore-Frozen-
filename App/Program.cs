namespace App;

internal class Program
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void Main(string[] args)
    {
        TraceFile.Set();

        while (true)
        {
            Sources sources = new();
            foreach (Source source in sources)
            {
                try
                {
                    ParsethingContext db = new();
                    if (source.Number != string.Empty)
                    {
                        Procurement procurement = SetProcurement(source);
                        Procurement? def = null;
                        try
                        {
                            def = db.Procurements.Where(p => p.Number == procurement.Number).First();
                        }
                        catch { }
                        if (def == null)
                        {
                            try
                            {
                                _ = db.Procurements.Add(procurement);
                            }
                            catch { }
                        }
                        else
                        {
                            def.LawId = procurement.LawId;
                            def.MethodId = procurement.MethodId;
                            def.PlatformId = procurement.PlatformId;
                            def.OrganizationId = procurement.OrganizationId;
                            def.Object = procurement.Object;
                            def.Location = procurement.Location;
                            def.StartDate = procurement.StartDate;
                            def.Deadline = procurement.Deadline;
                            def.TimeZoneId = procurement.TimeZoneId;
                            def.InitialPrice = procurement.InitialPrice;
                            def.Securing = procurement.Securing;
                            def.Enforcement = procurement.Enforcement;
                            def.Warranty = procurement.Warranty;
                            _ = db.Procurements.Update(def);
                        }
                        Trace.WriteLine(procurement.ToString());
                        _ = db.SaveChanges();
                    }
                }
                catch { }
            }
        }
    }

    private static Procurement SetProcurement(Procurement source)
    {
        Procurement procurement = source;
        if (source.Law != null)
        {
            procurement.LawId = GetIdTo.Law(source.Law);
        }
        else
        {
            procurement.LawId = null;
        }
        procurement.Law = null;
        if (source.Method != null)
        {
            procurement.MethodId = GetIdTo.Method(source.Method);
        }
        else
        {
            procurement.MethodId = null;
        }
        procurement.Method = null;
        if (source.Platform != null)
        {
            procurement.PlatformId = GetIdTo.Platform(source.Platform);
        }
        else
        {
            procurement.PlatformId = null;
        }
        procurement.Platform = null;
        if (source.Organization != null)
        {
            procurement.OrganizationId = GetIdTo.Organization(source.Organization);
        }
        else
        {
            procurement.OrganizationId = null;
        }
        procurement.Organization = null;
        if (source.TimeZone != null)
        {
            procurement.TimeZoneId = GetIdTo.TimeZone(source.TimeZone);
        }
        procurement.TimeZone = null;
        return procurement;
    }
}