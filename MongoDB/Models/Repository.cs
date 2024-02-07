namespace MongoDB.Models;

using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Models.Store;
using System.Globalization;

public class Repository
{
    private static string dataBase = "ciclisme";
    private static IMongoDatabase? db = null;
    private static IMongoCollection<Ciclista>? colCiclistas = null;
    private static IMongoCollection<Etapa>? colEtapas = null;
    private static IMongoCollection<Maillot>? colMaillots = null;
    private static IMongoCollection<Equipo>? colEquipos = null;
    private static List<Puerto> colPuertos = new List<Puerto>();
    private const string Language = "en-US";
    private const string MessageError = "¡El campo no es correcto!";

    public Repository()
    {
    }

    public static void Connect()
    {
        try
        {
            MongoClient? client = new MongoClient(new MongoClientSettings()
            {
                Scheme = ConnectionStringScheme.MongoDB,
                SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.None },
                Server = new MongoServerAddress("localhost", 27017),
                Credential = MongoCredential.CreateCredential(
                    databaseName: "admin",
                    username: "root",
                    password: "password123!"),
                ConnectTimeout = new TimeSpan(0, 0, 5),
                DirectConnection = true,
                ReadPreference = new ReadPreference(ReadPreferenceMode.Primary)
            });
            db = client.GetDatabase(dataBase);
            colCiclistas = db.GetCollection<Ciclista>("ciclistas");
            colEtapas = db.GetCollection<Etapa>("etapas");
            colMaillots = db.GetCollection<Maillot>("maillots");
            colEquipos = db.GetCollection<Equipo>("equipos");
            colPuertos = new List<Puerto>();
            foreach (List<Puerto> puertos in GetPhases().Where(x => x.puertos?.Count > 0).Select(x => x.puertos))
            {
                foreach (Puerto puerto in puertos)
                {
                    if (!colPuertos.Contains(puerto))
                    {
                        colPuertos.Add(puerto);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static List<Ciclista> GetCyclists()
    {
        try
        {
            return colCiclistas.AsQueryable<Ciclista>().OrderBy(x => x._id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Ciclista>();
    }

    public static List<Equipo> GetTeams()
    {
        try
        {
            return colEquipos.AsQueryable<Equipo>().OrderBy(x => x._id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Equipo>();
    }

    public static List<Etapa> GetPhases()
    {
        try
        {
            return colEtapas.AsQueryable<Etapa>().OrderBy(x => x._id).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex}");
        }
        return new List<Etapa>();
    }

    public static List<Puerto> GetPorts() => colPuertos.OrderBy(x => x._id).ToList();

    public static List<Maillot> GetMaillots() => colMaillots.AsQueryable<Maillot>().OrderBy(x => x._id).ThenBy(x => x.tipo).ToList();

    public bool IsInt(string number)
    {
        int intent;
        try
        {
            intent = Convert.ToInt32(number, new CultureInfo(Language));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public int CheckInt(string number, string message)
    {
        try
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (IsInt(number))
                {
                    int numberInt = Convert.ToInt32(number, new CultureInfo(Language));
                    if (numberInt >= 0)
                        return numberInt;
                    else
                        Dialogs.GenerateMessage(MessageBoxImage.Warning, message);
                }
                else
                    Dialogs.GenerateMessage(MessageBoxImage.Warning, MessageError);
            }
        }
        catch (Exception)
        {
            Dialogs.GenerateMessage(MessageBoxImage.Warning, MessageError);
        }
        return 0;
    }

    public List<Etapa> UnassignedPhase()
    {
        List<int> letapasA = new List<int>();
        List<Etapa> letapasNA = GetPhases();
        foreach (List<EtapaT> etapas in GetCyclists().Where(x => x.etapas_g?.Count > 0).Select(x => x.etapas_g))
        {
            foreach (int netapa in etapas.Select(x => x.netapa))
            {
                if (netapa > 0 && !letapasA.Contains(netapa))
                {
                    letapasA.Add(netapa);
                }
            }
        }
        letapasNA.RemoveAll(x => letapasA.Contains(x._id));
        return letapasNA;
    }

    public List<Puerto> UnassignedPorts()
    {
        List<string> lpuertosA = new List<string>();
        List<Puerto> lpuertosNA = GetPorts();
        foreach (List<PuertoO> puertos in GetCyclists().Where(x => x.puertos_g?.Count > 0).Select(x => x.puertos_g))
        {
            foreach (string puerto in puertos.Select(x => x.nompuerto))
            {
                if (!string.IsNullOrEmpty(puerto) && !lpuertosA.Contains(puerto))
                {
                    lpuertosA.Add(puerto);
                }
            }
        }
        lpuertosNA.RemoveAll(x => lpuertosA.Contains(x._id));
        return lpuertosNA;
    }

    public List<Maillot> UnassignedMaillots()
    {
        List<string> lmaillotsA = new List<string>();
        List<Maillot> lmaillotsNA = GetMaillots();
        foreach (List<MaillotT> maillots in GetCyclists().Where(x => x.maillots_g?.Count > 0).Select(x => x.maillots_g))
        {
            foreach (string? maillot in maillots.Select(x => x.codigo))
            {
                if (!string.IsNullOrEmpty(maillot) && !lmaillotsA.Contains(maillot))
                {
                    lmaillotsA.Add(maillot);
                }
            }
        }
        lmaillotsNA.RemoveAll(x => lmaillotsA.Contains((x._id != null ? x._id : string.Empty)));
        return lmaillotsNA;
    }

    public Ciclista AddCyclist(Ciclista cyclist)
    {
        db?.GetCollection<Ciclista>("ciclistas").InsertOne(cyclist);
        return cyclist;
    }

    public bool ExistCyclist(int? dorsal)
    {
        if (GetCyclists().Find(x => x._id == dorsal) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Ciclista? UpdateCyclist(int? dorsal, Ciclista cyclist)
    {
        if (db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == dorsal, Builders<Ciclista>.Update.Set(x => x.nombre, cyclist.nombre).Set(x => x.edad, cyclist.edad).Set(x => x.nomeq, cyclist.nomeq)) == null)
        {
            return null;
        }
        else
        {
            return cyclist;
        }
    }

    public void DeleteCyclist(Ciclista cyclist) => db?.GetCollection<Ciclista>("ciclistas").FindOneAndDelete(x => x._id == cyclist._id);

    public Equipo AddTeam(Equipo team)
    {
        db?.GetCollection<Equipo>("equipos").InsertOne(team);
        return team;
    }

    public bool ExistTeam(string name)
    {
        if (GetTeams().Find(x => x._id.Equals(name)) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Equipo? UpdateTeam(string name, Equipo team)
    {
        if (db?.GetCollection<Equipo>("equipos").FindOneAndUpdate(x => x._id.Equals(name), Builders<Equipo>.Update.Set(x => x.director, team.director)) == null)
        {
            return null;
        }
        else
        {
            return team;
        }
    }

    public void DeleteTeam(Equipo team) => db?.GetCollection<Equipo>("equipos").FindOneAndDelete(x => x._id.Equals(team._id));

    public Etapa AddPhase(Etapa team)
    {
        db?.GetCollection<Etapa>("etapas").InsertOne(team);
        return team;
    }

    public bool ExistPhase(int number)
    {
        if (GetPhases().Find(x => x._id == number) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Etapa? UpdatePhase(int number, Etapa phase)
    {
        if (db?.GetCollection<Etapa>("etapas").FindOneAndUpdate(x => x._id == number, Builders<Etapa>.Update.Set(x => x.salida, phase.salida).Set(x => x.llegada, phase.llegada).Set(x => x.km, phase.km)) == null)
        {
            return null;
        }
        else
        {
            return phase;
        }
    }

    public void DeletePhase(Etapa phase)
    {
        db?.GetCollection<Etapa>("etapas").FindOneAndDelete(x => x._id == phase._id);
        foreach (int id in GetCyclists().Select(x => x._id))
        {
            db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == id, Builders<Ciclista>.Update.PullFilter<EtapaT>("etapas_g", Builders<EtapaT>.Filter.Eq("netapa", phase._id)));
            db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == id, Builders<Ciclista>.Update.PullFilter<MaillotT>("maillots_g", Builders<MaillotT>.Filter.Eq("netapa", phase._id)));
        }
    }


    public Maillot AddMaillot(Maillot maillot)
    {
        db?.GetCollection<Maillot>("maillots").InsertOne(maillot);
        return maillot;
    }

    public bool ExistMaillot(string code)
    {
        if (GetMaillots().Find(x => (x._id != null ? x._id : string.Empty).Equals(code)) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Maillot? UpdateMaillot(string code, Maillot maillot)
    {
        if (db?.GetCollection<Maillot>("maillots").FindOneAndUpdate(x => (x._id != null ? x._id : string.Empty).Equals(code), Builders<Maillot>.Update.Set(x => x.tipo, maillot.tipo).Set(x => x.color, maillot.color).Set(x => x.premio, maillot.premio)) == null)
        {
            return null;
        }
        else
        {
            return maillot;
        }
    }

    public void DeleteMaillot(Maillot maillot)
    {
        db?.GetCollection<Maillot>("maillots").FindOneAndDelete(x => (x._id != null ? x._id : string.Empty).Equals(maillot._id));
        foreach (Ciclista ciclista in GetCyclists())
        {
            db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == ciclista._id, Builders<Ciclista>.Update.PullFilter<MaillotT>("maillots_g", Builders<MaillotT>.Filter.Eq("codigo", maillot._id)));
        }
    }

    public Etapa? AddPhaseCyclist(Ciclista ciclista, Etapa etapa)
    {

        if (db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == ciclista._id, Builders<Ciclista>.Update.AddToSet<EtapaT>(a => a.etapas_g, new EtapaT(etapa))) == null)
        {
            return null;
        }
        else
        {
            foreach (Ciclista ciclista1 in GetCyclists().Where(x => x._id != ciclista._id))
            {
                db.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == ciclista1._id, Builders<Ciclista>.Update.PullFilter<EtapaT>("etapas_g", Builders<EtapaT>.Filter.Eq("netapa", etapa._id)));
            }
            return etapa;
        }
    }

    public Etapa? AddMaillotCiclista(Ciclista ciclista, Maillot maillot, Etapa etapa)
    {

        if (db?.GetCollection<Ciclista>("ciclistas").FindOneAndUpdate(x => x._id == ciclista._id, Builders<Ciclista>.Update.AddToSet<MaillotT>(a => a.maillots_g, new MaillotT(maillot, etapa))) == null)
        {
            return null;
        }
        else
        {
            foreach (Ciclista ciclista1 in GetCyclists().Where(x => x._id != ciclista._id))
            {
                if (ciclista1.maillots_g?.Count > 0 &&
                    (ciclista1.maillots_g.RemoveAll(x => x.codigo == maillot._id && x.netapa == etapa._id) > 0))
                {
                    db?.GetCollection<Ciclista>("ciclistas").FindOneAndReplace(Builders<Ciclista>.Filter.Eq("_id", ciclista1._id), ciclista1);
                }
            }
            return etapa;
        }
    }

}
