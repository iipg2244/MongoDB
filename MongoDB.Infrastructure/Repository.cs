namespace MongoDB.Infrastructure;

using MongoDB.Driver;
using System.Diagnostics;

public class Repository
{
    private static string connectionString = "mongodb://root:password123!@localhost:27017/?retryWrites=true&w=majority";
    private static string database = "ciclisme";

    public Repository()
    {
    }

    public static List<Cyclist> GetCyclists()
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Cyclists.OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Cyclist>();
    }

    public static List<Team> GetTeams()
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Teams.OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Team>();
    }

    public static List<Phase> GetPhases()
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Phases.OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Phase>();
    }

    public static List<Port> GetPorts()
    {
        List<Port> colPorts = new List<Port>();
        foreach (List<Port> ports in GetPhases().Where(x => x.Ports?.Count > 0).Select(x => x.Ports))
        {
            foreach (Port port in ports)
            {
                if (!colPorts.Contains(port))
                {
                    colPorts.Add(port);
                }
            }
        }
        return colPorts.OrderBy(x => x.Id).ToList();
    }

    public static List<Maillot> GetMaillots()
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Maillots.OrderBy(x => x.Id).ThenBy(x => x.Type).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Maillot>();
    }

    public List<Phase> UnassignedPhase()
    {
        List<int> lphasesA = new List<int>();
        List<Phase> lphasesNA = GetPhases();
        foreach (List<PhaseCyclist> phases in GetCyclists().Where(x => x.Phases.Count > 0).Select(x => x.Phases))
        {
            foreach (int NumberPhase in phases.Select(x => x.NumberPhase))
            {
                if (NumberPhase > 0 && !lphasesA.Contains(NumberPhase))
                {
                    lphasesA.Add(NumberPhase);
                }
            }
        }
        lphasesNA.RemoveAll(x => lphasesA.Contains(x.Id));
        return lphasesNA;
    }

    public List<Port> UnassignedPorts()
    {
        List<string> lportsA = new List<string>();
        List<Port> lportsNA = GetPorts();
        foreach (List<PortCyclist> ports in GetCyclists().Where(x => x.Ports.Count > 0).Select(x => x.Ports))
        {
            foreach (string port in ports.Select(x => x.NamePort))
            {
                if (!string.IsNullOrEmpty(port) && !lportsA.Contains(port))
                {
                    lportsA.Add(port);
                }
            }
        }
        lportsNA.RemoveAll(x => lportsA.Contains(x.Id));
        return lportsNA;
    }

    public List<Maillot> UnassignedMaillots()
    {
        List<string> lmaillotsA = new List<string>();
        List<Maillot> lmaillotsNA = GetMaillots();
        foreach (List<MaillotCyclist> maillots in GetCyclists()
            .Where(x => x.Maillots.Count > 0)
            .Select(x => x.Maillots))
        {
            foreach (string? maillot in maillots.Select(x => x.Code))
            {
                if (!string.IsNullOrEmpty(maillot) && !lmaillotsA.Contains(maillot))
                {
                    lmaillotsA.Add(maillot);
                }
            }
        }
        lmaillotsNA.RemoveAll(x => lmaillotsA.Contains((x.Id != null ? x.Id : string.Empty)));
        return lmaillotsNA;
    }

    public Cyclist AddCyclist(Cyclist cyclist)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            db.Cyclists.Add(cyclist);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return cyclist;
    }

    public bool ExistCyclist(int? dorsal)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Cyclists.Any(x => x.Id == dorsal);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return false;
    }

    public static Cyclist? GetCyclist(int? dorsal)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Cyclists.FirstOrDefault(x => x.Id == dorsal);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public Cyclist? UpdateCyclist(int? dorsal, Cyclist cyclist)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == dorsal);
            if (cyclistTmp != null)
            {
                cyclistTmp.Name = cyclist.Name;
                cyclistTmp.Age = cyclist.Age;
                cyclistTmp.TeamName = cyclist.TeamName;
                db.SaveChanges();
            }
            return cyclist;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public void DeleteCyclist(Cyclist cyclist)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == cyclist.Id);
            if (cyclistTmp != null)
            {
                db.Cyclists.Remove(cyclistTmp);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
    }

    public Team AddTeam(Team team)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            db.Teams.Add(team);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return team;
    }

    public bool ExistTeam(string name)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Teams.Any(x => x.Id.Equals(name));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return false;
    }

    public static Team? GetTeam(string name)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Teams.FirstOrDefault(x => x.Id.Equals(name));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public Team? UpdateTeam(string name, Team team)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var teamTmp = db.Teams.FirstOrDefault(x => x.Id.Equals(name));
            if (teamTmp != null)
            {
                teamTmp.Manager = team.Manager;

                db.SaveChanges();
            }
            return team;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public void DeleteTeam(Team team)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var teamTmp = db.Teams.FirstOrDefault(x => x.Id.Equals(team.Id));
            if (teamTmp != null)
            {
                db.Teams.Remove(teamTmp);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
    }

    public Phase AddPhase(Phase phase)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            db.Phases.Add(phase);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return phase;
    }

    public bool ExistPhase(int number)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Phases.Any(x => x.Id == number);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return false;
    }

    public static Phase? GetPhase(int number)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Phases.FirstOrDefault(x => x.Id == number);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public Phase? UpdatePhase(int number, Phase phase)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var phaseTmp = db.Phases.FirstOrDefault(x => x.Id == number);
            if (phaseTmp != null)
            {
                phaseTmp.Start = phase.Start;
                phaseTmp.Finish = phase.Finish;
                phaseTmp.Distance = phase.Distance;
                db.SaveChanges();
            }
            return phase;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public void DeletePhase(Phase phase)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var phaseTmp = db.Phases.FirstOrDefault(x => x.Id == phase.Id);
            if (phaseTmp != null)
            {
                db.Phases.Remove(phaseTmp);
                foreach (int id in db.Cyclists
                    .Where(x => x.Phases.Any(y => y.NumberPhase == phase.Id) || 
                    x.Maillots.Any(y => y.NumberPhase == phase.Id))
                    .Select(x => x.Id))
                {
                    var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == id);
                    if (cyclistTmp != null)
                    {
                        cyclistTmp.Phases.RemoveAll(x => x.NumberPhase == phase.Id);
                        cyclistTmp.Maillots.RemoveAll(x => x.NumberPhase == phase.Id);
                    }
                }
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
    }

    public Maillot AddMaillot(Maillot maillot)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            db.Maillots.Add(maillot);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return maillot;
    }

    public bool ExistMaillot(string code)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Maillots.Any(x => x.Id.Equals(code));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return false;
    }

    public static Maillot? GetMaillot(string code)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            return db.Maillots.FirstOrDefault(x => x.Id.Equals(code));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public Maillot? UpdateMaillot(string code, Maillot maillot)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var maillotTmp = db.Maillots.FirstOrDefault(x => x.Id.Equals(code));
            if (maillotTmp != null)
            {
                maillotTmp.Type = maillot.Type;
                maillotTmp.Color = maillot.Color;
                maillotTmp.Reward = maillot.Reward;
                db.SaveChanges();
            }
            return maillot;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public void DeleteMaillot(Maillot maillot)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var maillotTmp = db.Maillots.FirstOrDefault(x => x.Id == maillot.Id);
            if (maillotTmp != null)
            {
                db.Maillots.Remove(maillotTmp);
                foreach (int id in db.Cyclists
                    .Where(x => x.Maillots.Any(y => y.Code == maillot.Id))
                    .Select(x => x.Id))
                {
                    var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == id);
                    if (cyclistTmp != null)
                    {
                        cyclistTmp.Maillots.RemoveAll(x => x.Code == maillot.Id);
                    }
                }
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
    }

    public Phase? AddPhaseCyclist(Cyclist cyclist, Phase phase)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == cyclist.Id);
            if (cyclistTmp != null)
            {
                cyclistTmp.Phases.Add(new PhaseCyclist(phase));
                foreach (int id in db.Cyclists.Where(x => x.Id != cyclist.Id && 
                x.Phases.Any(y => y.NumberPhase == phase.Id)).Select(x => x.Id))
                {
                    var cyclistTmp2 = db.Cyclists.FirstOrDefault(x => x.Id == id);
                    if (cyclistTmp2 != null)
                    {
                        cyclistTmp2.Phases.RemoveAll(x => x.NumberPhase == phase.Id);
                    }
                }
                db.SaveChanges();
            }
            return phase;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

    public Phase? AddMaillotCiclista(Cyclist cyclist, Maillot maillot, Phase phase)
    {
        try
        {
            var db = CyclingEntitiesFactory.Create(connectionString, database);
            var cyclistTmp = db.Cyclists.FirstOrDefault(x => x.Id == cyclist.Id);
            if (cyclistTmp != null)
            {
                cyclistTmp.Maillots.Add(new MaillotCyclist(maillot, phase));
                foreach (int id in db.Cyclists.Where(x => x.Id != cyclist.Id && 
                x.Maillots.Any(x => x.Code == maillot.Id && x.NumberPhase == phase.Id)).Select(x => x.Id))
                {
                    var cyclistTmp2 = db.Cyclists.FirstOrDefault(x => x.Id == id);
                    if (cyclistTmp2 != null)
                    {
                        cyclistTmp2.Maillots.RemoveAll(x => x.Code == maillot.Id && x.NumberPhase == phase.Id);
                    }
                }
                db.SaveChanges();
            }
            return phase;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return null;
    }

}
