using System.Windows;

namespace Constancias.Views {
    /// <summary>
    /// Interaction logic for WindowContainer.xaml
    /// </summary>
    public partial class WindowContainer : Window {
        public WindowContainer () {
            InitializeComponent ();
            mainFrame.Navigate (new LogIn ());
        }
    }
}
