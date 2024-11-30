using Constancias.DTO;
using Constancias.POCO;
using Constancias.Views.ManageCertificates;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.Administrative {
    /// <summary>
    /// Interaction logic for HistoryCertificades.xaml
    /// </summary>
    public partial class HistoryCertificades : Page {
        ObservableCollection<Certificate> retrievedCertificates = new ObservableCollection<Certificate> ();

        public HistoryCertificades () {
            InitializeComponent ();

            RetrieveCertificates ();
        }

        private void RetrieveCertificates () {
            List<Certificate> certs = CertificateDAO.GetCertificate ();
            if (certs != null) {
                retrievedCertificates = null;
                retrievedCertificates = new ObservableCollection<Certificate> (certs);
                dataGrid_Certificates.ItemsSource = retrievedCertificates;
            }
        }

        private void ClicShowDetailsCertificate (object sender, RoutedEventArgs e) {
            Button button = sender as Button;

            if (button.DataContext is Certificate rowData) {
                NavigationService.Navigate (
                    new DetailsCertificate (
                        (dataGrid_Certificates.SelectedItem as Certificate).IdCertificate)
                );
            }
        }

        private void Back_Label_Click (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }

        private void Back_Click (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }
    }
}
