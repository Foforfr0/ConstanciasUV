using Spire.Doc;
using System.IO;

namespace Constancias.Utils {
    internal class ConvertFiles {
        public static byte[] WordToPdf (byte[] wordBytes) {
            using (MemoryStream inputStream = new MemoryStream (wordBytes)) {
                // Crea un documento Spire.Doc
                Document document = new Document ();
                document.LoadFromStream (inputStream, FileFormat.Docx);

                // Crea un stream de salida para el PDF
                using (MemoryStream outputStream = new MemoryStream ()) {
                    document.SaveToStream (outputStream, FileFormat.PDF);

                    return outputStream.ToArray ();
                }
            }
        }
    }
}
