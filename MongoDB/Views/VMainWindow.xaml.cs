namespace MongoDB.Views;

using MongoDB.Infrastructure;
using MongoDB.ViewModels;

/// <summary>
/// Lógica de interacción para VMainWindow.xaml
/// </summary>
public partial class VMainWindow : Window
{
    public VMainWindow()
    {
        InitializeComponent();
        ChangeHeaders();
    }

    private void InsertCommutator(object sender, RoutedEventArgs e) => ((VMMainWindow)DataContext).InsertCommutator();

    private void UpdateCommutator(object sender, RoutedEventArgs e) => ((VMMainWindow)DataContext).UpdateCommutator();

    private void DeleteCommutator(object sender, RoutedEventArgs e) => ((VMMainWindow)DataContext).DeleteCommutator();

    private void InsertPhase(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).InsertPhase();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void UpdatePhase(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).UpdatePhase();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void DeletePhase(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).DeletePhase();
        Phases.Items.Refresh();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void InsertMaillot(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).InsertMaillot();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void UpdateMaillot(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).UpdateMaillot();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void DeleteMaillot(object sender, RoutedEventArgs e)
    {
        ((VMMainWindow)DataContext).DeleteMaillot();
        Maillots.Items.Refresh();
        ((VMMainWindow)DataContext).Unassigned();
        ((VMMainWindow)DataContext).OrderByRb();
    }

    private void AssignPhase(object sender, RoutedEventArgs e) => ((VMMainWindow)DataContext).AssignPhase();

    private void RadioButton_Checked(object sender, RoutedEventArgs e) => ChangeHeaders();

    private void ChangeHeaders()
    {
        if (Commutator != null)
        {
            if (RbCiclista.IsChecked != null && (bool)RbCiclista.IsChecked)
            {
                Commutator.Columns[0].Header = "Dorsal";
                Commutator.Columns[0].Visibility = Visibility.Visible;
                Commutator.Columns[1].Header = "Nombre";
                Commutator.Columns[2].Header = "Edad";
                Commutator.Columns[2].Visibility = Visibility.Visible;
                Commutator.Columns[3].Header = "Equipo";
            }
            else
            {
                Commutator.Columns[0].Header = "";
                Commutator.Columns[0].Visibility = Visibility.Hidden;
                Commutator.Columns[1].Header = "Nombre";
                Commutator.Columns[2].Header = "";
                Commutator.Columns[2].Visibility = Visibility.Hidden;
                Commutator.Columns[3].Header = "Director";
            }
        }
    }
}
