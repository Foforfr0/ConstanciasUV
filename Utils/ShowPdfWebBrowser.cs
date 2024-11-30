using Microsoft.Web.WebView2.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Constancias.Utils {
    internal class ShowPdfWebBrowser {
        public static void ShowPdfInWebView2 (WebView2 webView, byte[] pdfBytes, string fileName) {
            try {
                // Crear carpeta temporal si no existe
                string tempDirectory = Path.Combine (Path.GetTempPath (), "TempFiles");

                if (!Directory.Exists (tempDirectory)) {
                    Directory.CreateDirectory (tempDirectory);
                }

                // Ruta completa del archivo PDF temporal
                string pdfFilePath = Path.Combine (tempDirectory, $"{fileName}_Temp.pdf");

                // Escribir el archivo PDF en la carpeta temporal
                File.WriteAllBytes (pdfFilePath, pdfBytes);

                // Cargar el PDF en WebView2 usando una URI válida
                webView.Source = new Uri ($"file:///{pdfFilePath.Replace ("\\", "/")}");
            } catch (Exception ex) {
                MessageBox.Show ($"Error al mostrar el PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
