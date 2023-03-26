namespace Database;

public static class GetIdTo
{
    public static int Law(Law law)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (law != null)
        {
            Law? def = null;
            try
            {
                def = db.Laws.Where(l => l.Number == law.Number).First();
            }
            catch { }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.Laws.Add(law);
                _ = db.SaveChanges();
                id = db.Laws.Where(l => l.Number == law.Number).First().Id;
            }
        }
        return id;
    }

    public static int Method(Method method)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (method != null)
        {
            Method? def = null;
            try
            {
                def = db.Methods.Where(m => m.Text == method.Text).First();
            }
            catch { }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.Methods.Add(method);
                _ = db.SaveChanges();
                id = db.Methods.Where(m => m.Text == method.Text).First().Id;
            }
        }
        return id;
    }

    public static int Organization(Organization organization)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (organization != null)
        {
            Organization? def = null;
            if (organization.PostalAddress != null)
            {
                try
                {
                    def = db.Organizations.Where(o => o.Name == organization.Name).Where(o => o.PostalAddress == organization.PostalAddress).First();
                }
                catch { }
            }
            else
            {
                try
                {
                    def = db.Organizations.Where(o => o.Name == organization.Name).First();
                }
                catch { }
            }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.Organizations.Add(organization);
                _ = db.SaveChanges();
                id = db.Organizations.Where(o => o.Name == organization.Name).Where(o => o.PostalAddress == organization.PostalAddress).First().Id;
            }
        }
        return id;
    }

    public static int Platform(Platform platform)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (platform != null)
        {
            Platform? def = null;
            try
            {
                def = db.Platforms.Where(p => p.Name == platform.Name).Where(p => p.Address == platform.Address).First();
            }
            catch { }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.Platforms.Add(platform);
                _ = db.SaveChanges();
                id = db.Platforms.Where(p => p.Name == platform.Name).Where(p => p.Address == platform.Address).First().Id;
            }
        }
        return id;
    }

    public static int SourceState(SourceStatе sourceStatе)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (sourceStatе != null)
        {
            SourceStatе? def = null;
            try
            {
                def = db.SourceStatеs.Where(pss => pss.Kind == sourceStatе.Kind).First();
            }
            catch { }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.SourceStatеs.Add(sourceStatе);
                _ = db.SaveChanges();
                id = db.SourceStatеs.Where(pss => pss.Kind == sourceStatе.Kind).First().Id;
            }
        }
        return id;
    }

    public static int TimeZone(TimeZone timeZone)
    {
        using ParsethingContext db = new();
        int id = 0;
        if (timeZone != null)
        {
            TimeZone? def = null;
            try
            {
                def = db.TimeZones.Where(tz => tz.Offset == timeZone.Offset).First();
            }
            catch { }
            if (def != null)
            {
                id = def.Id;
            }
            else
            {
                _ = db.TimeZones.Add(timeZone);
                _ = db.SaveChanges();
                id = db.TimeZones.Where(tz => tz.Offset == timeZone.Offset).First().Id;
            }
        }
        return id;
    }
}