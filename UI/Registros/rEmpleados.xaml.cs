using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//Using agregados
using EmpleadoNomina.BLL;
using EmpleadoNomina.Entidades;
using RegistroAgenda.BLL;

namespace EmpleadoNomina.UI.Registros
{
    public partial class rEmpleados : Window
    {
        private Empleados empleados = new Empleados();
        public rEmpleados()
        {
            InitializeComponent();
            this.DataContext = empleados;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = empleados;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.empleados = new Empleados();
            this.DataContext = empleados;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (EmpleadoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida\n\nNo se pudo validar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Empleados encontrado = EmpleadosBLL.Buscar(Utilidades.ToInt(EmpleadoIdTextBox.Text));

            if (encontrado != null)
            {
                this.empleados = encontrado;
                Cargar();
            }
            else
            {
                this.empleados = new Empleados();
                this.DataContext = this.empleados;

                MessageBox.Show($"Este Contacto no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                Limpiar();
                EmpleadoIdTextBox.SelectAll();
                EmpleadoIdTextBox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO ]———————————————————————————————————————————————————————
                //—————————————————————————————————[ Contacto Id ]—————————————————————————————————
                if (EmpleadoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Contacto Id) está vacío.\n\nPorfavor, Asigne un Id al Contacto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmpleadoIdTextBox.Text = "0";
                    EmpleadoIdTextBox.Focus();
                    EmpleadoIdTextBox.SelectAll();
                    return;
                }
                
                //—————————————————————————————————[ NombreCompleto ]—————————————————————————————————
                if (NombreCompletoTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Completo) está vacío.\n\nPorfavor, Asigne un Nombre al Contacto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombreCompletoTextBox.Clear();
                    NombreCompletoTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Telefono ]—————————————————————————————————
                if (TelefonoTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Teléfono) está vacío.\n\nAsigne un Teléfono al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Text = "0";
                    TelefonoTextBox.Focus();
                    TelefonoTextBox.SelectAll();
                    return;
                }
                if (TelefonoTextBox.Text.Length != 10)
                {
                    MessageBox.Show($"El Teféfono ({TelefonoTextBox.Text}) no es válido.\n\nEl Teléfono debe tener 10 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Focus();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = EmpleadosBLL.Guardar(empleados);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("El Registro se pudo guardar satisfactoriamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("El Registro no se pudo guardar :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (EmpleadosBLL.Eliminar(Utilidades.ToInt(EmpleadoIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //—————————————————————————————————[ Contacto Id ]—————————————————————————————————
        private void EmpleadoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EmpleadoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EmpleadoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Contacto Id) debe ser un número.\n\nPor favor, digite un número valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                EmpleadoIdTextBox.Text = "0";
                EmpleadoIdTextBox.Focus();
                EmpleadoIdTextBox.SelectAll();
            }
        }
        //—————————————————————————————————[ Telefono ]—————————————————————————————————
        private void TelefonoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TelefonoTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(TelefonoTextBox.Text);
                }

                if (TelefonoTextBox.Text.Length != 10)
                {
                    TelefonoTextBox.Foreground = Brushes.Red;
                }
                else
                {
                    TelefonoTextBox.Foreground = Brushes.Green;
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Teléfono) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Text = "0";
                TelefonoTextBox.Focus();
                TelefonoTextBox.SelectAll();
            }
        }
    }
}