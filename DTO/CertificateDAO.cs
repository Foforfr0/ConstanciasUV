using Constancias.Connection;
using Constancias.POCO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Constancias.DTO {
    public class CertificateDAO {
        private static string stringConnection = DBConnection.getStringConnection ();

        public static List<Certificate> GetCertificate () {
            using (SqlConnection sqlConnection = new SqlConnection (stringConnection)) {
                sqlConnection.Open ();
                string query =
                    "SELECT crt.IdCertifcade, crtty.IdCertificadeType, crtty.Type, crt.DateApplied AS DateEmited, " +
                    "empl.IdEmployee, CONCAT(empl.FirstName, ' ', empl.MiddleName) AS ProfesorName " +
                    "FROM Certificade crt " +
                    "LEFT JOIN CertificadeType crtty ON crt.IdCertifiedType = crtty.IdCertificadeType " +
                    "LEFT JOIN Employee empl ON crt.IdProfesor = empl.IdEmployee;";

                using (SqlCommand command = new SqlCommand (query, sqlConnection)) {
                    using (SqlDataReader reader = command.ExecuteReader ()) {
                        List<Certificate> certificates = new List<Certificate> ();
                        while (reader.Read ()) {
                            certificates.Add (new Certificate {
                                IdCertificate = reader.GetInt32 (0),
                                IdType = reader.GetInt32 (1),
                                Type = reader.GetString (2),
                                DateEmited = reader.GetDateTime (3),
                                Profesor = new Employee {
                                    IdEmployee = reader.GetInt32 (4),
                                    CompleteName = reader.GetString (5),
                                },
                            });
                        }
                        return certificates;
                    }
                }
            }
        }

        public static Certificate GetCertificate (int idCertificate) {
            using (SqlConnection sqlConnection = new SqlConnection (stringConnection)) {
                sqlConnection.Open ();
                string query =
                    "SELECT crt.IdCertifcade, crt.DateApplied AS DateEmited, crtty.IdCertificadeType, crtty.Type, " +
                    "empl.IdEmployee, CONCAT(empl.FirstName, ' ', empl.MiddleName) AS ProfesorName, crtty.Doc, empl.Gender, empl.Tuition " +
                    "FROM Certificade crt " +
                    "LEFT JOIN CertificadeType crtty ON crt.IdCertifiedType = crtty.IdCertificadeType " +
                    "LEFT JOIN Employee empl ON crt.IdProfesor = empl.IdEmployee " +
                    "WHERE crt.IdCertifcade = @IdCertificate";
                SqlCommand command = new SqlCommand (query, sqlConnection);
                command.Parameters.AddWithValue ("@IdCertificate", idCertificate);
                SqlDataReader reader = command.ExecuteReader ();
                Certificate certificate = null;
                if (reader.Read ()) {
                    certificate = new Certificate {
                        IdCertificate = reader.GetInt32 (0),
                        DateEmited = reader.GetDateTime (1),
                        IdType = reader.GetInt32 (2),
                        Type = reader.GetString (3),
                        Profesor = new Employee {
                            IdEmployee = reader.GetInt32 (4),
                            CompleteName = reader.GetString (5),
                            Gender = reader.GetString (7),
                            Tuition = reader.GetString (8),
                        }
                    };
                    if (reader.IsDBNull (6)) {
                        certificate.Doc = null;
                    } else {
                        long bytesRead = 0;
                        long length = reader.GetBytes (6, 0, null, 0, 0);
                        certificate.Doc = new byte[length];
                        bytesRead = reader.GetBytes (6, 0, certificate.Doc, 0, (int)length);
                    }
                }
                return certificate;
            }
        }

        public static List<CertificateType> GetCertificateTypes () {
            using (SqlConnection sqlConnection = new SqlConnection (stringConnection)) {
                sqlConnection.Open ();
                string query = "SELECT * FROM CertificadeType;";

                using (SqlCommand command = new SqlCommand (query, sqlConnection)) {
                    using (SqlDataReader reader = command.ExecuteReader ()) {
                        List<CertificateType> certificates = new List<CertificateType> ();
                        while (reader.Read ()) {
                            certificates.Add (new CertificateType {
                                IdType = reader.GetInt32(0),
                                Type = reader.GetString (1)
                            });
                        }
                        return certificates;
                    }
                }
            }
        }
    }
}
