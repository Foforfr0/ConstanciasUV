using Constancias.DTO;
using Constancias.POCO;
using Constancias.Views.ManageCertificates;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.Administrative {
    /// <summary>
    /// Interaction logic for ManageCertificates.xaml
    /// </summary>
    public partial class ManageCertificates : Page {
        ObservableCollection<Certificate> retrievedCertificates = new ObservableCollection<Certificate> ();

        public ManageCertificates () {
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

        public void ClickRegistryNewCertificate (object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate (new RegistryCertificate());
        }

        public void ClickRenewSignature (object sender, RoutedEventArgs e) {

        }
    }
}
