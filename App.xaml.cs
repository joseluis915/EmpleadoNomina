using System.Windows;

namespace EmpleadoNomina
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"**Ha ocurrido una excepcion**\n\n {e.Exception.Message}");
            e.Handled = true;
        }
    }
}
