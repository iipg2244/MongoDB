namespace MongoDB.Infrastructure.Dtos;

public class CommutatorDto
{
    public int? Col1 { get; set; } = null;
    public string Col2 { get; set; } = string.Empty;
    public int? Col3 { get; set; } = null;
    public string Col4 { get; set; } = string.Empty;
    public int CountPorts { get; set; } = 0;
    public int CountMaillots { get; set; } = 0;
    public int CountPhases { get; set; } = 0;
    public int CountAll { get; set; } = 0;

    public CommutatorDto()
    {
    }

    public CommutatorDto(Cyclist cyclist)
    {
        Col1 = cyclist.Id;
        Col2 = cyclist.Name;
        Col3 = cyclist.Age;
        Col4 = cyclist.TeamName;
        CountPorts = 0;
        CountMaillots = 0;
        CountPhases = 0;
        if (cyclist.Maillots?.Count > 0)
        {
            CountPorts = cyclist.Maillots.Count;
        }
        if (cyclist.Maillots?.Count > 0)
        {
            CountMaillots = cyclist.Maillots.Count;
        }
        if (cyclist.Phases?.Count > 0)
        {
            CountPhases = cyclist.Phases.Count;
        }
        CountAll = CountPorts + CountMaillots + CountPhases;
    }

    public CommutatorDto(Team team)
    {
        Col1 = null;
        Col2 = team.Id;
        Col3 = null;
        Col4 = team.Manager;
        CountPorts = 0;
        CountMaillots = 0;
        CountPhases = 0;
        List<Cyclist> lciclistas = Repository.GetCyclists().Where(x => x.TeamName.Equals(team.Id)).ToList();
        if (lciclistas.Count > 0)
        {
            foreach (Cyclist cyclist in lciclistas)
            {
                if (cyclist.Ports?.Count > 0)
                {
                    CountPorts += cyclist.Ports.Count;

                }
                if (cyclist.Maillots?.Count > 0)
                {
                    CountMaillots += cyclist.Maillots.Count;
                }
                if (cyclist.Phases?.Count > 0)
                {
                    CountPhases += cyclist.Phases.Count;
                }
            }
        }
        CountAll = CountPorts + CountMaillots + CountPhases;
    }



}
