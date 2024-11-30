using System;

namespace Constancias.POCO {
    public class Certificate {
        public int IdCertificate {
            get; set;
        }
        public DateTime DateEmited {
            get; set;
        }
        public int IdType {
            get; set;
        }
        public string Type {
            get; set;
        }
        public Employee Profesor {
            get; set;
        }
        public byte[] Doc {
            get; set;
        }
    }
}
