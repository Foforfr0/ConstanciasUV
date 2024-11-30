using Constancias.Singleton;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page {
        public MainWindow () {
            InitializeComponent ();

            UpdateWindow ();
        }

        private void UpdateWindow () {
            //TODO Left option bar

            label_NameEmployee.Content = SessionManager.Instance.loggedInEmployee.FirstName + " " + SessionManager.Instance.loggedInEmployee.MiddleName;
            label_RolEmployee.Content = SessionManager.Instance.loggedInEmployee.Rol;
        }

        private void ClickCheckAccount (object sender, RoutedEventArgs e) {

        }

        private void ClickShowManagementCertificates (object sender, RoutedEventArgs e) {
            int currentRol = SessionManager.Instance.loggedInEmployee.IdRole;

            switch (currentRol) {
                case 1: // Administrador
                    break;
                case 2: // Administrativo
                    innerFrameContainer.Navigate (new Constancias.Views.Administrative.ManageCertificates ());
                    break;
                case 3: // Profesor
                    break;
                default:
                    break;
            }
        }

        private void ClickShowManagementProfessorsFrame (object sender, RoutedEventArgs e) {
            innerFrameContainer.Navigate (new Constancias.Views.Administrative.ManageCertificates ());
        }

        private void ClickLogOut (object sender, RoutedEventArgs e) {
            try {
                this.NavigationService.GoBack ();
                SessionManager.Instance.logOut ();
            } catch (Exception ex) {
                MessageBox.Show (ex.Message);
            }
        }
    }
}
