using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.Profesor {
    /// <summary>
    /// Interaction logic for DetailsProfesor.xaml
    /// </summary>
    public partial class DetailsProfesor : Page {
        public DetailsProfesor () {
            InitializeComponent ();
        }

        private void ClickGetBackPage (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }
    }
}
