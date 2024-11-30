using System;
using System.IO;
using System.Windows;

namespace Constancias.Utils {
    internal class ManageTempFile {
        public static void ClearTempFolder () {
            try {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string tempDirectory = Path.Combine (appDirectory, "TempFiles");

                if (Directory.Exists (tempDirectory)) {
                    Directory.Delete (tempDirectory, true); // Eliminar todos los archivos y carpetas
                }
            } catch (Exception ex) {
                MessageBox.Show ($"Error al limpiar archivos temporales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
