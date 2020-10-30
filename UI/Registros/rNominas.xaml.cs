using System;
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
    public partial class rNominas : Window
    {
        private Nominas nominas = new Nominas();
        public rNominas()
        {
            InitializeComponent();
            this.DataContext = nominas;
            //——————————————————————————[ VALORES DEL ComboBox Empleado Id]——————————————————————————
            EmpleadoIdComboBox.ItemsSource = EmpleadosBLL.GetEmpleados();
            EmpleadoIdComboBox.SelectedValuePath = "EmpleadoId";
            EmpleadoIdComboBox.DisplayMemberPath = "NombreCompleto";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = nominas;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.nominas = new Nominas();
            this.DataContext = nominas;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (NominaIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida\n\nNo se pudo validar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Nominas encontrado = NominasBLL.Buscar(Utilidades.ToInt(NominaIdTextBox.Text));

            if (encontrado != null)
            {
                this.nominas = encontrado;
                Cargar();
            }
            else
            {
                this.nominas = new Nominas();
                this.DataContext = this.nominas;

                MessageBox.Show($"Este Contacto no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                Limpiar();
                NominaIdTextBox.SelectAll();
                NominaIdTextBox.Focus();
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
                //—————————————————————————————————[ Nomina Id ]—————————————————————————————————
                if (NominaIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Contacto Id) está vacío.\n\nPorfavor, Asigne un Id al Contacto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NominaIdTextBox.Text = "0";
                    NominaIdTextBox.Focus();
                    NominaIdTextBox.SelectAll();
                    return;
                }


                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = NominasBLL.Guardar(nominas);
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
                if (NominasBLL.Eliminar(Utilidades.ToInt(NominaIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //—————————————————————————————————[ Empleado Id ]—————————————————————————————————
        private void NominasIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (NominaIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(NominaIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Contacto Id) debe ser un número.\n\nPor favor, digite un número valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                NominaIdTextBox.Text = "0";
                NominaIdTextBox.Focus();
                NominaIdTextBox.SelectAll();
            }
        }

        private void SalarioMensualTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double SM = double.Parse(SalarioMensualTextBox.Text);

            //double HorasExtra = double.Parse(HorasExtraTextBox.Text);

            //—————————————[Formulas para Seguro Familiar de Salud o SFS]—————————————
            double SFS = 0.0304;

            //—————————————[Formulas para Administradora de Fondos de Pensiones o AFP]—————————————
            double AFP = 0.0287;

            //—————————————[Formulas para Salario Mensual y Horas Extra]—————————————
            //double SMEX = SM + HorasExtra;

            //—————————————[Formulas para Impuesto sobre renta o ISR]—————————————
            double DEDUC = SFS + AFP;
            double RDEDUC = SM - DEDUC;
            double IMPOx12 = RDEDUC * 12;
            double EXCEDENT = IMPOx12 - 624329.01;
            double APLICA20 = EXCEDENT * 0.2;
            double PASO_ADICC = APLICA20 + 31216;

            double ISR = PASO_ADICC / 12;
            //————————————————————————————————————————————————
            double TotalD = SM - (SFS + AFP + ISR);

            SFSTextBox.Text = Convert.ToString(SM * SFS);
            AFPTextBox.Text = Convert.ToString(SM * AFP);
            ISRTextBox.Text = Convert.ToString(ISR);

            SueldoTotalTextBox.Text = Convert.ToString(SM);
            TotalDecuentosTextBox.Text = Convert.ToString(TotalD);
        }
    }
}