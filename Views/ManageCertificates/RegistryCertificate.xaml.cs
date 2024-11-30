using Constancias.DTO;
using Constancias.POCO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Views.ManageCertificates {
    /// <summary>
    /// Interaction logic for RegistryCertificate.xaml
    /// </summary>
    public partial class RegistryCertificate : Page {
        public RegistryCertificate () {
            InitializeComponent ();

            InitializeComboBox ();
        }

        private void InitializeComboBox () {
            List<CertificateType> availableCertificates = CertificateDAO.GetCertificateTypes ();
            List<Employee> availableProfessors = new EmployeeDAO ().GetProfessors ();

            List<object> certificateOptions = new List<object> { "Seleccione una opción" };
            certificateOptions.AddRange (availableCertificates);

            List<object> professorOptions = new List<object> { "Seleccione una opción" };
            professorOptions.AddRange (availableProfessors);

            comboBox_Certificates.ItemsSource = certificateOptions;
            comboBox_Certificates.DisplayMemberPath = "Type";
            comboBox_Certificates.SelectedValuePath = "IdType";
            comboBox_Certificates.SelectedIndex = 0;

            comboBox_Professors.ItemsSource = professorOptions;
            comboBox_Professors.DisplayMemberPath = "Tuition";
            comboBox_Professors.SelectedValuePath = "IdEmployee";
            comboBox_Professors.SelectedIndex = 0;
        }



        private bool ValidateForm () {
            bool isCorrect = true;

            if (comboBox_Certificates.SelectedIndex == 0) {
                label_ErrorCertificates.Content = "Se requiere seleccionar un tipo de certificado.";
                label_ErrorCertificates.Visibility = Visibility.Visible;
                isCorrect = false;
            } else {
                label_ErrorCertificates.Content = "";
                label_ErrorCertificates.Visibility = Visibility.Collapsed;
            }
            if (comboBox_Professors.SelectedIndex == 0) {
                label_ErrorProfessors.Content = "Se requiere seleccionar algún profesor.";
                label_ErrorProfessors.Visibility = Visibility.Visible;
                isCorrect = false;
            } else {
                label_ErrorProfessors.Content = "";
                label_ErrorProfessors.Visibility = Visibility.Collapsed;
            }

            return isCorrect;
        }

        private void ClickFinish (object sender, RoutedEventArgs e) {
            if (ValidateForm ()) {
                if (comboBox_Certificates.SelectedIndex == 0) {
                    MessageBox.Show ("No es posible generar el documento. Seleccione un certificado válido.", "Error");
                    return;
                }
                CertificateType selectedCertificate = comboBox_Certificates.SelectedItem as CertificateType;
                Employee selectedEmployee = comboBox_Professors.SelectedItem as Employee;

            }
        }

        private void ClickGetBackPage (object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack ();
        }
    }
}
