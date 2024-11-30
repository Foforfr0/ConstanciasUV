using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.Profesor {
    /// <summary>
    /// Interaction logic for RegistryProfesor.xaml
    /// </summary>
    public partial class RegistryProfesor : Page {
        public RegistryProfesor () {
            InitializeComponent ();
        }

        private void ClickFinish (object sender, RoutedEventArgs e) {
        
        }

        private void ClickGetBackPage (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }
    }
}
