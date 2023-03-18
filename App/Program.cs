namespace App;

internal class Program
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void Main(string[] args)
    {
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
                                Console.WriteLine("Add new Procurement");
                            }
                            catch
                            {
                                Debug.WriteLine(procurement.ToString());
                            }
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
                            Console.WriteLine("Update Procurement");
                        }
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
        procurement.Law = null;
        if (source.Method != null)
        {
            procurement.MethodId = GetIdTo.Method(source.Method);
        }
        procurement.Method = null;
        if (source.Platform != null)
        {
            procurement.PlatformId = GetIdTo.Platform(source.Platform);
        }
        procurement.Platform = null;
        if (source.Organization != null)
        {
            procurement.OrganizationId = GetIdTo.Organization(source.Organization);
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