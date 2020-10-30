using System.Windows;
using EmpleadoNomina.UI.Consultas;
using EmpleadoNomina.UI.Registros;


namespace EmpleadoNomina
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rEmpleadosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEmpleados rEmpleado = new rEmpleados();
            rEmpleado.Show();
        }

        private void rNominasMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rNominas rNomina = new rNominas();
            rNomina.Show();
        }

        private void cEmpleadosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEmpleados cEmpleado = new cEmpleados();
            cEmpleado.Show();
        }

        private void InformacionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Creado por:\t\t{CreadoPorLabel.Content}\n\nVersión:\t\t\t{VersionLabel.Content}\n\nCreación:\t\t{CreacionLabel.Content}\n\nUltima Modificación:\t{ModificacionLabel.Content}\n\nPara más información:\tjose_burgos3@ucne.edu.do", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
