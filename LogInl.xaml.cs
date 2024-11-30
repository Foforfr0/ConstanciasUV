using Constancias.DTO;
using Constancias.POCO;
using Constancias.Singleton;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace Constancias {
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogInl : Window {
        EmployeeDAO employeeDAO = null;
        public LogInl () {
            InitializeComponent ();
            txtEmail.Text = "administrador@example.com";
            txtPassword.Text = "1234";
        }

        private void Button_Aceptar (object sender, RoutedEventArgs e) {
            if (!checkFields ()) {
                MessageBox.Show ("Por favor ingrese sus credenciales", "Campos fatantes");
            } else {
                try {
                    Employee employee = new Employee ();
                    employeeDAO = new EmployeeDAO ();
                    employee = employeeDAO.LogIn (txtEmail.Text, txtPassword.Text);
                    if (employee != null) {
                        SessionManager.Instance.login (employee);
                        MainFrame.Navigate (new Constancias.Views.MainWindow());
                    } else {
                        MessageBox.Show ("Credenciales incorrectas", "Iniciar sesion");
                    }
                } catch (Exception ex) {
                    MessageBox.Show ("Error al obtener la sesion", "Error");
                }
            }
        }

        private bool checkFields () {
            if (txtEmail.Text.Length == 0 || txtPassword.Text.Length == 0) {
                return false;
            } else {
                return true;
            }
        }

        private void Button_Salir (object sender, RoutedEventArgs e) {
            this.Close ();
        }

        private void MainFrame_Navigated (object sender, NavigationEventArgs e) {

        }
    }
}
