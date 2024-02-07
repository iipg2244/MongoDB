

namespace MongoDB.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using Dto;
using MongoDB.Models;
using MongoDB.Models.Store;

public partial class VMMainWindow : VMBase
{
    #region Variables

    private int do1 = 0;
    private int do2 = 0;
    private int ed1 = 0;
    private int ed2 = 0;
    private int dorsal2 = 0;
    private int edad2 = 0;
    private int n2 = 0;
    private int km2 = 0;
    private int premio2 = 0;

    [ObservableProperty]
    private List<CommutatorDto> commutador = new List<CommutatorDto>();

    [ObservableProperty]
    private CommutatorDto? selectCommutador = null;

    [ObservableProperty]
    private List<Puerto> puertos = new List<Puerto>();

    [ObservableProperty]
    private Puerto? selectPuerto = null;

    [ObservableProperty]
    private List<Maillot> _maillots = new List<Maillot>();

    [ObservableProperty]
    private Maillot? selectMaillot = null;

    [ObservableProperty]
    private List<Etapa> etapas = new List<Etapa>();

    [ObservableProperty]
    private Etapa? selectEtapa = null;

    [ObservableProperty]
    private bool rbc = false;

    [ObservableProperty]
    private bool rbe = false;

    [ObservableProperty]
    private bool rbeta = false;

    [ObservableProperty]
    private bool rbpue = false;

    [ObservableProperty]
    private bool rbmai = false;

    [ObservableProperty]
    private bool rbasc = false;

    [ObservableProperty]
    private bool rbdesc = false;

    [ObservableProperty]
    private bool rbtodo = false;

    [ObservableProperty]
    private string tb11 = string.Empty;

    [ObservableProperty]
    private string tb12 = string.Empty;

    [ObservableProperty]
    private string tb21 = string.Empty;

    [ObservableProperty]
    private string tb22 = string.Empty;

    [ObservableProperty]
    private string tb3 = string.Empty;

    [ObservableProperty]
    private string tb4 = string.Empty;

    [ObservableProperty]
    private bool ckna = false;

    [ObservableProperty]
    private string dorsal = string.Empty;

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private string edad = string.Empty;

    [ObservableProperty]
    private string equipo = string.Empty;

    [ObservableProperty]
    private string n = string.Empty;

    [ObservableProperty]
    private string km = string.Empty;

    [ObservableProperty]
    private string salida = string.Empty;

    [ObservableProperty]
    private string llegada = string.Empty;

    [ObservableProperty]
    private string premio = string.Empty;

    [ObservableProperty]
    private string codigo = string.Empty;

    [ObservableProperty]
    private string tipo = string.Empty;

    [ObservableProperty]
    private string color = string.Empty;

    [ObservableProperty]
    private bool cka = false;

    [ObservableProperty]
    private bool rbae = false;

    [ObservableProperty]
    private bool rbam = false;

    private Repository r = new Repository();

    #endregion

    public VMMainWindow()
    {
        Repository.Connect();
        Rbc = true;
        Rbeta = true;
        Rbasc = true;
        Tb11 = string.Empty;
        Tb12 = string.Empty;
        Tb21 = string.Empty;
        Tb22 = string.Empty;
        Tb3 = string.Empty;
        Tb4 = string.Empty;
        Dorsal = string.Empty;
        Nombre = string.Empty;
        Edad = string.Empty;
        Equipo = string.Empty;
        N = string.Empty;
        Salida = string.Empty;
        Llegada = string.Empty;
        Km = string.Empty;
        Premio = string.Empty;
        Codigo = string.Empty;
        Tipo = string.Empty;
        Color = string.Empty;
        Ckna = false;
        Cka = false;
        Rbae = true;
    }

    #region OnChanged

    partial void OnSelectCommutadorChanged(CommutatorDto? value)
    {
        if (!Cka)
        {
            if (value != null)
            {
                PopulatePorts();
                PopulateMaillots();
                PopulatePhases();
                if (value.col1 != null)
                {
                    Dorsal = $"{value.col1}";
                }
                Nombre = value.col2;
                if (value.col3 != null)
                {
                    Edad = $"{value.col3}";
                }
                Equipo = value.col4;
                Ckna = false;
            }
            else
            {
                CleanCommutator();
            }
        }
    }

    partial void OnSelectMaillotChanged(Maillot? value)
    {
        if (!Cka)
        {
            if (value != null)
            {
                Codigo = (value._id != null ? value._id : string.Empty);
                Tipo = value.tipo;
                Color = value.color;
                Premio = $"{value.premio}";
            }
            else
            {
                CleanMaillot();
            }
        }
    }

    partial void OnSelectEtapaChanged(Etapa? value)
    {
        if (!Cka)
        {
            if (value != null)
            {
                N = $"{value._id}";
                Salida = value.salida;
                Llegada = value.llegada;
                Km = $"{value.km}";
            }
            else
            {
                CleanPhase();
            }
        }
    }

    partial void OnRbcChanged(bool value)
    {
        Filter();
    }

    partial void OnRbeChanged(bool value)
    {
        Filter();
    }

    partial void OnRbetaChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnRbpueChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnRbmaiChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnRbascChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnRbdescChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnRbtodoChanged(bool value)
    {
        OrderByRb();
    }

    partial void OnTb11Changed(string value)
    {
        do1 = r.CheckInt(value, "El rango de dorsal tiene que ser positivo!");
        Filter();
    }

    partial void OnTb12Changed(string value)
    {
        do2 = r.CheckInt(value, "El rango de dorsal tiene que ser positivo!");
        Filter();
    }

    partial void OnTb21Changed(string value)
    {
        ed1 = r.CheckInt(value, "El rango de edad tiene que ser positivo!");
        Filter();
    }

    partial void OnTb22Changed(string value)
    {
        ed2 = r.CheckInt(value, "El rango de edad tiene que ser positivo!");
        Filter();
    }

    partial void OnTb3Changed(string value)
    {
        Filter();
    }

    partial void OnTb4Changed(string value)
    {
        Filter();
    }

    partial void OnCknaChanged(bool value)
    {
        Unassigned();
    }

    partial void OnDorsalChanged(string value)
    {
        dorsal2 = r.CheckInt(value, "El dorsal tiene que ser positivo!");
    }

    partial void OnEdadChanged(string value)
    {
        edad2 = r.CheckInt(value, "La edad tiene que ser positiva!");
    }

    partial void OnNChanged(string value)
    {
        n2 = r.CheckInt(value, "El nº tiene que ser positivo!");
    }

    partial void OnKmChanged(string value)
    {
        km2 = r.CheckInt(value, "El Km tiene que ser positivo!");
    }

    partial void OnPremioChanged(string value)
    {
        premio2 = r.CheckInt(value, "El premio tiene que ser positivo!");
    }

    partial void OnCkaChanged(bool value)
    {
        ModeAssign();
    }

    #endregion

    public void PopulateCyclists()
    {
        Commutador = new List<CommutatorDto>();
        foreach (Ciclista ciclista in Repository.GetCyclists())
        {
            Commutador.Add(new CommutatorDto(ciclista));
        }
    }

    public void PopulateTeams()
    {
        Commutador = new List<CommutatorDto>();
        foreach (Equipo equipoTmp in Repository.GetTeams())
        {
            Commutador.Add(new CommutatorDto(equipoTmp));
        }
    }

    public void PopulatePorts()
    {
        Puertos = new List<Puerto>();
        if (SelectCommutador != null)
        {
            if (Rbc)
            {
                PopulatePortsForCyclist(SelectCommutador);
            }
            else
            {
                PopulatePortsForTeam(SelectCommutador);
            }
        }
        Puertos = Puertos.OrderBy(x => x._id).ToList();
    }

    private void PopulatePortsForCyclist(CommutatorDto commutator)
    {
        Ciclista ciclista = new Ciclista(commutator);
        if (ciclista.puertos_g?.Count > 0)
        {
            foreach (PuertoO puerto in ciclista.puertos_g)
            {
                Puertos.Add(new Puerto(puerto));
            }
        }
    }

    private void PopulatePortsForTeam(CommutatorDto commutator)
    {
        Equipo equipoTmp = new Equipo(commutator);
        List<Ciclista> lciclistas = Repository.GetCyclists().Where(x => x.nomeq.Equals(equipoTmp._id)).ToList();
        if (lciclistas.Count > 0)
        {
            foreach (Ciclista ciclista in lciclistas.Where(x => x.puertos_g?.Count > 0))
            {
                foreach (PuertoO puerto in ciclista.puertos_g)
                {
                    Puerto puerto1 = new Puerto(puerto);
                    if (!Puertos.Contains(puerto1))
                    {
                        Puertos.Add(new Puerto(puerto1));
                    }
                }
            }
        }
    }

    public void PopulateMaillots()
    {
        Maillots = new List<Maillot>();
        if (SelectCommutador != null)
        {
            if (Rbc)
            {
                PopulateMaillotsForCyclist(SelectCommutador);
            }
            else
            {
                PopulateMaillotsForTeam(SelectCommutador);
            }
        }
        Maillots = Maillots.OrderBy(x => x.codigo).ThenBy(x => x.netapa).ToList();
    }

    private void PopulateMaillotsForCyclist(CommutatorDto commutator)
    {
        Ciclista ciclista = new Ciclista(commutator);
        if (ciclista.maillots_g?.Count > 0)
        {
            foreach (MaillotT maillot in ciclista.maillots_g)
            {
                Maillots.Add(new Maillot(maillot));
            }
        }
    }

    private void PopulateMaillotsForTeam(CommutatorDto commutator)
    {
        Equipo equipoTmp = new Equipo(commutator);
        List<Ciclista> lciclistas = Repository.GetCyclists().Where(x => x.nomeq.Equals(equipoTmp._id)).ToList();
        if (lciclistas.Count > 0)
        {
            foreach (Ciclista ciclista in lciclistas.Where(x => x.maillots_g?.Count > 0))
            {
                foreach (MaillotT maillot in ciclista.maillots_g)
                {
                    Maillot maillot1 = new Maillot(maillot);
                    if (!Maillots.Contains(maillot1))
                    {
                        Maillots.Add(maillot1);
                    }
                }
            }
        }
    }

    public void PopulatePhases()
    {
        Etapas = new List<Etapa>();
        if (SelectCommutador != null)
        {
            if (Rbc)
            {
                PopulatePhasesForCyclist(SelectCommutador);
            }
            else
            {
                PopulatePhasesForTeam(SelectCommutador);
            }
        }
        Etapas = Etapas.OrderBy(x => x._id).ToList();
    }

    private void PopulatePhasesForCyclist(CommutatorDto commutator)
    {
        Ciclista ciclista = new Ciclista(commutator);
        if (ciclista.etapas_g?.Count > 0)
        {
            foreach (EtapaT etapa in ciclista.etapas_g)
            {
                Etapas.Add(new Etapa(etapa));
            }
        }
    }

    private void PopulatePhasesForTeam(CommutatorDto commutator)
    {
        Equipo equipoTmp = new Equipo(commutator);
        List<Ciclista> lciclistas = Repository.GetCyclists().Where(x => x.nomeq.Equals(equipoTmp._id)).ToList();
        if (lciclistas.Count > 0)
        {
            foreach (Ciclista ciclista in lciclistas.Where(x => x.etapas_g?.Count > 0))
            {
                foreach (EtapaT etapa in ciclista.etapas_g)
                {
                    Etapa etapa1 = new Etapa(etapa);
                    if (!Etapas.Contains(etapa1))
                    {
                        Etapas.Add(etapa1);
                    }
                }
            }
        }
    }

    public void OrderByRb()
    {
        if (Commutador?.Count > 0)
        {
            if (Rbeta)
            {
                if (Rbasc)
                {
                    OrderByRbAndEtaAsc();
                }
                else
                {
                    OrderByRbAndEtaDesc();
                }
            }
            else if (Rbpue)
            {
                if (Rbasc)
                {
                    OrderByRbAndPueAsc();
                }
                else
                {
                    OrderByRbAndPueDesc();
                }
            }
            else if (Rbmai)
            {
                if (Rbasc)
                {
                    OrderByRbAndMaiAsc();
                }
                else
                {
                    OrderByRbAndMaiDesc();
                }
            }
            else if (Rbtodo)
            {
                if (Rbasc)
                {
                    OrderByRbAndTodoAsc();
                }
                else
                {
                    OrderByRbAndTodoDesc();
                }
            }
        }
    }

    public void OrderByRbAndEtaAsc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderBy(x => x.countEtapas).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderBy(x => x.countEtapas).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndEtaDesc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderByDescending(x => x.countEtapas).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderByDescending(x => x.countEtapas).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndPueAsc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderBy(x => x.countPuertos).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderBy(x => x.countPuertos).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndPueDesc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderByDescending(x => x.countPuertos).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderByDescending(x => x.countPuertos).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndMaiAsc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderBy(x => x.countMaillots).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderBy(x => x.countMaillots).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndMaiDesc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderByDescending(x => x.countMaillots).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderByDescending(x => x.countMaillots).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndTodoAsc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderBy(x => x.countTodo).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderBy(x => x.countTodo).ThenBy(x => x.col2).ToList();
        }
    }

    public void OrderByRbAndTodoDesc()
    {
        if (Rbc)
        {
            Commutador = Commutador.OrderByDescending(x => x.countTodo).ThenBy(x => x.col1).ToList();
        }
        else
        {
            Commutador = Commutador.OrderByDescending(x => x.countTodo).ThenBy(x => x.col2).ToList();
        }
    }

    public void ChangeCommutator()
    {
        Puertos = new List<Puerto>();
        Maillots = new List<Maillot>();
        Etapas = new List<Etapa>();
        if (Rbc)
        {
            PopulateCyclists();
        }
        else
        {
            PopulateTeams();
            Cka = false;
        }
        OrderByRb();
        SelectCommutador = null;
    }

    public void Filter()
    {
        if (Tb11 != null && Tb12 != null && Tb21 != null && Tb22 != null && Tb3 != null && Tb4 != null)
        {
            ChangeCommutator();
            if (Rbc)
            {
                FilterCommutatorByRbc();
            }
            else
            {
                FilterCommutator();
            }
        }
    }

    private void FilterCommutatorByRbc()
    {
        if (do1 != 0 && do2 != 0 && ed1 != 0 && ed2 != 0)
        {
            Commutador = Commutador.Where(x => x.col1 >= do1 && x.col1 <= do2 && x.col3 >= ed1 && x.col3 <= ed2 && x.col2.ToUpper().Contains(Tb3.ToUpper()) && x.col4.ToUpper().Contains(Tb4.ToUpper())).ToList();
        }
        else if (do1 != 0 && do2 != 0)
        {
            Commutador = Commutador.Where(x => x.col1 >= do1 && x.col1 <= do2 && x.col2.ToUpper().Contains(Tb3.ToUpper()) && x.col4.ToUpper().Contains(Tb4.ToUpper())).ToList();
        }
        else if (ed1 != 0 && ed2 != 0)
        {
            Commutador = Commutador.Where(x => x.col3 >= ed1 && x.col3 <= ed2 && x.col2.ToUpper().Contains(Tb3.ToUpper()) && x.col4.ToUpper().Contains(Tb4.ToUpper())).ToList();
        }
        else
        {
            Commutador = Commutador.Where(x => x.col2.ToUpper().Contains(Tb3.ToUpper()) && x.col4.ToUpper().Contains(Tb4.ToUpper())).ToList();
        }
    }

    private void FilterCommutator() => Commutador = Commutador.Where(x => x.col2.ToUpper().Contains(Tb3.ToUpper()) && x.col4.ToUpper().Contains(Tb4.ToUpper())).ToList();

    public void Unassigned()
    {
        if (Ckna)
        {
            SelectCommutador = null;
            Cka = false;
            Etapas = r.UnassignedPhase();
            Puertos = r.UnassignedPorts();
            Maillots = r.UnassignedMaillots();
        }
        else
        {
            if (SelectCommutador == null)
            {
                Etapas = new List<Etapa>();
                Puertos = new List<Puerto>();
                Maillots = new List<Maillot>();
            }
        }
    }

    public void ModeAssign()
    {
        if (Cka)
        {
            SelectCommutador = null;
            SelectEtapa = null;
            SelectMaillot = null;
            SelectPuerto = null;
            Ckna = false;
            CleanCommutator();
            CleanPhase();
            CleanMaillot();
            Etapas = Repository.GetPhases();
            Puertos = Repository.GetPorts();
            Maillots = Repository.GetMaillots();
        }
        else
        {
            SelectCommutador = null;
            Etapas = new List<Etapa>();
            Puertos = new List<Puerto>();
            Maillots = new List<Maillot>();
        }
    }

    public void AssignPhase()
    {
        if (Rbae)
        {
            if (SelectCommutador != null && SelectEtapa != null)
            {
                r.AddPhaseCyclist(new Ciclista(SelectCommutador), SelectEtapa);
                SelectCommutador = null;
                SelectEtapa = null;
                Cka = false;
                Filter();
            }
            else
            {
                Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Warning, "Seleccione un ciclista, y una etapa primero!");
            }
        }
        else if (Rbam)
        {
            if (SelectCommutador != null && SelectEtapa != null && SelectMaillot != null)
            {
                r.AddMaillotCiclista(new Ciclista(SelectCommutador), SelectMaillot, SelectEtapa);
                SelectCommutador = null;
                SelectEtapa = null;
                SelectMaillot = null;
                Cka = false;
                Filter();
            }
            else
            {
                Dialogs.GenerateMessage(System.Windows.MessageBoxImage.Warning, "Seleccione un ciclista, una etapa y un maillot primero!");
            }
        }
    }

    public void InsertCommutator()
    {
        if (Rbc)
        {
            if (dorsal2 != 0 && edad2 != 0 && !r.ExistCyclist(dorsal2))
            {
                r.AddCyclist(new Ciclista(dorsal2, Nombre, edad2, Equipo));
                SelectCommutador = null;
                Filter();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Nombre) && !r.ExistTeam(Nombre))
            {
                r.AddTeam(new Equipo(Nombre, Equipo));
                SelectCommutador = null;
                Filter();
            }
        }
    }

    public void UpdateCommutator()
    {
        if (Rbc)
        {
            if (dorsal2 != 0 && edad2 != 0 && SelectCommutador != null && r.ExistCyclist(SelectCommutador.col1))
            {
                r.UpdateCyclist(SelectCommutador.col1, new Ciclista(dorsal2, Nombre, edad2, Equipo));
                SelectCommutador = null;
                Filter();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Nombre) && SelectCommutador != null && r.ExistTeam(SelectCommutador.col2))
            {
                r.UpdateTeam(SelectCommutador.col2, new Equipo(Nombre, Equipo));
                SelectCommutador = null;
                Filter();
            }
        }
    }

    public void DeleteCommutator()
    {
        if (Rbc)
        {
            if (dorsal2 != 0 && edad2 != 0 && SelectCommutador != null && r.ExistCyclist(SelectCommutador.col1))
            {
                r.DeleteCyclist(new Ciclista(SelectCommutador));
                SelectCommutador = null;
                Filter();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Nombre) && SelectCommutador != null && r.ExistTeam(SelectCommutador.col2))
            {
                r.DeleteTeam(new Equipo(SelectCommutador));
                SelectCommutador = null;
                Filter();
            }
        }
    }

    public void InsertPhase()
    {
        if (n2 != 0 && km2 != 0 && !r.ExistPhase(n2))
        {
            r.AddPhase(new Etapa(n2, km2, Salida, Llegada));
            SelectEtapa = null;
        }
    }

    public void UpdatePhase()
    {
        if (n2 != 0 && km2 != 0 && SelectEtapa != null && r.ExistPhase(SelectEtapa._id))
        {
            r.UpdatePhase(SelectEtapa._id, new Etapa(n2, km2, Salida, Llegada));
            SelectEtapa = null;
        }
    }

    public void DeletePhase()
    {
        if (n2 != 0 && km2 != 0 && SelectEtapa != null && r.ExistPhase(SelectEtapa._id))
        {
            r.DeletePhase(new Etapa(SelectEtapa._id, km2, Salida, Llegada));
            SelectEtapa = null;
        }
    }

    public void InsertMaillot()
    {
        if (!string.IsNullOrEmpty(Codigo) && premio2 != 0 && !r.ExistMaillot(Codigo))
        {
            r.AddMaillot(new Maillot(Codigo, Tipo, Color, premio2));
            SelectMaillot = null;
        }
    }

    public void UpdateMaillot()
    {
        if (!string.IsNullOrEmpty(Codigo) && premio2 != 0 && SelectMaillot != null)
        {
            string selectCodigo = SelectMaillot._id != null ? SelectMaillot._id : string.Empty;
            if (r.ExistMaillot(selectCodigo))
            {
                r.UpdateMaillot(selectCodigo, new Maillot(Codigo, Tipo, Color, premio2));
                SelectMaillot = null;
            }
        }
    }

    public void DeleteMaillot()
    {
        if (!string.IsNullOrEmpty(Codigo) && premio2 != 0 && SelectMaillot != null)
        {
            string selectCodigo = SelectMaillot._id != null ? SelectMaillot._id : string.Empty;
            if (r.ExistMaillot(selectCodigo))
            {
                r.DeleteMaillot(new Maillot(selectCodigo, Tipo, Color, premio2));
                SelectMaillot = null;
            }
        }
    }

    public void CleanCommutator()
    {
        Dorsal = string.Empty;
        Nombre = string.Empty;
        Edad = string.Empty;
        Equipo = string.Empty;
    }

    public void CleanMaillot()
    {
        Codigo = string.Empty;
        Tipo = string.Empty;
        Color = string.Empty;
        Premio = string.Empty;
    }

    public void CleanPhase()
    {
        N = string.Empty;
        Salida = string.Empty;
        Llegada = string.Empty;
        Km = string.Empty;
    }

}
