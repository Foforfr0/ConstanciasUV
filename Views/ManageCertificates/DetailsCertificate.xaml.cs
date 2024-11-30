using Constancias.DTO;
using Constancias.POCO;
using Constancias.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.ManageCertificates {
    /// <summary>
    /// Interaction logic for DetailsCertificate.xaml
    /// </summary>
    public partial class DetailsCertificate : Page {
        private Certificate selectedCertificate;

        public DetailsCertificate (int idCertificate) {
            InitializeComponent ();

            RetrieveDataCertificate (idCertificate);
        }

        private void RetrieveDataCertificate (int idCertificate) {
            selectedCertificate = CertificateDAO.GetCertificate (idCertificate);
            if (selectedCertificate != null) {
                label_Profesor.Content = selectedCertificate.Profesor.CompleteName;
                label_DateEmited.Content = selectedCertificate.DateEmited.ToString ("D");
                label_TypeCertificate.Content = selectedCertificate.Type;

                GeneratePDF ();
            } else {
                MessageBox.Show ("No se pudo cargar los datos del certificado.\nIntente más tarde.", "Error al cargar los datos.");
            }
        }

        private void GeneratePDF () {
            try {
                selectedCertificate.Doc = ModifyWordTemplate.ParticipacionActualizacionEEProyectoIntegrador (selectedCertificate);
                if (selectedCertificate.Doc == null) {
                    label_StatusFile.Content = "No se pudo obtener el PDF.\nIntente más tarde.";
                } else {
                    selectedCertificate.Doc = ConvertFiles.WordToPdf (selectedCertificate.Doc);
                    ShowPdfWebBrowser.ShowPdfInWebView2 (PdfViewer, selectedCertificate.Doc, selectedCertificate.Profesor.Tuition + "_" + selectedCertificate.Type);
                    PdfViewer.Visibility = Visibility.Visible;
                }
            } catch (ArgumentException ae) {
                MessageBox.Show (ae.Message);
            } catch (Exception ex) {
                label_StatusFile.Content = "No se pudo obtener la constancia.\nIntente más tarde.";
                MessageBox.Show (ex.ToString ());
            }
        }

        private void ClickDownload (object sender, RoutedEventArgs e) {

        }

        private void ClickGetBackPage (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }
    }
}
