using Constancias.POCO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Linq;

namespace Constancias.Utils {
    public class ModifyWordTemplate {
        public static byte[] ParticipacionActualizacionEEProyectoIntegrador (Certificate newCertificate) {
            if (newCertificate?.Doc == null || newCertificate.Doc.Length == 0)
                throw new ArgumentException ("El documento base es inválido.");

            using (MemoryStream memoryStream = new MemoryStream ()) {
                memoryStream.Write (newCertificate.Doc, 0, newCertificate.Doc.Length);

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open (memoryStream, true)) {
                    // Obtén el cuerpo del documento
                    Body body = wordDoc.MainDocumentPart.Document.Body;

                    // Combinar todo el contenido de los nodos Text en los párrafos para buscar y reemplazar etiquetas
                    foreach (Paragraph paragraph in body.Descendants<Paragraph> ()) {
                        string combinedText = string.Join ("", paragraph.Descendants<Text> ().Select (t => t.Text));

                        // Reemplazar etiquetas en el texto combinado
                        combinedText = combinedText
                            .Replace ("{{ProfesorG}}", newCertificate.Profesor.Gender.Equals ("H", StringComparison.OrdinalIgnoreCase) ? "el profesor " : "la profesora ")
                            .Replace ("{{NombreProfesor}}", newCertificate.Profesor.CompleteName)
                            .Replace ("{{ExperienciaEducativa}}", newCertificate.Profesor.EE)
                            .Replace ("{{Carrera}}", newCertificate.Profesor.Career)
                            .Replace ("{{fecha}}", DateTime.Now.ToString ("D"))
                            .Replace ("{{del profesor interesado}}", newCertificate.Profesor.Gender.Equals ("H", StringComparison.OrdinalIgnoreCase) ? "del profesor interesado" : "de la profesora interesada")
                            .Replace ("{{dias}}", DateTime.Now.ToString ("d"))
                            .Replace ("{{mes}}", DateTime.Now.ToString ("MMMM"))
                            .Replace ("{{anio}}", DateTime.Now.ToString ("yyyy"));

                        // Eliminar nodos Text existentes en el párrafo
                        foreach (var textNode in paragraph.Descendants<Text> ().ToList ()) {
                            textNode.Remove ();
                        }

                        // Agregar el texto modificado en un solo nodo Text
                        paragraph.AppendChild (new Text (combinedText));
                    }

                    // Guarda los cambios en el documento
                    wordDoc.MainDocumentPart.Document.Save ();
                }

                return memoryStream.ToArray ();
            }
        }

    }
}