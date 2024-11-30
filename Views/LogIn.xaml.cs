using Constancias.DTO;
using Constancias.POCO;
using Constancias.Singleton;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Constancias.Views {
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page {
        EmployeeDAO employeeDAO = null;
        public LogIn () {
            InitializeComponent ();
            txtEmail.Text = "administrador@example.com";
            txtEmail.Text = "administrativo@example.com";
          
            txtPassword.Text = "1234";
        }

        private void ClickAcept (object sender, RoutedEventArgs e) {
            if (!CheckFields ()) {
                MessageBox.Show ("Por favor ingrese sus credenciales", "Campos fatantes");
            } else {
                try {
                    Employee employee = new Employee ();
                    employeeDAO = new EmployeeDAO ();

                    employee = employeeDAO.LogIn (txtEmail.Text, txtPassword.Text);

                    if (employee != null) {
                        SessionManager.Instance.login (employee);
                        NavigationService navService = NavigationService.GetNavigationService (this);
                        navService.Navigate (new MainWindow ());
                    } else {
                        MessageBox.Show ("Credenciales incorrectas", "Iniciar sesion");
                    }
                } catch (Exception ex) {
                    MessageBox.Show ("Error al obtener la sesion", "Error");
                }
            }
        }

        private bool CheckFields () {
            if (txtEmail.Text.Length == 0 || txtPassword.Text.Length == 0) {
                return false;
            } else {
                return true;
            }
        }

        private void ClickCloseProgram (object sender, RoutedEventArgs e) {
        }

        private void MainFrame_Navigated (object sender, NavigationEventArgs e) {

        }
    }
}
