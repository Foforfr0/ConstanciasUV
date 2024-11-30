using Constancias.POCO;

namespace Constancias.Singleton {
    internal class SessionManager {
        private static SessionManager instance;
        private static readonly object _lock = new object ();

        public Employee loggedInEmployee {
            get; private set;
        }
        private SessionManager () {
        }
        public static SessionManager Instance {
            get {
                if (instance == null) {
                    lock (_lock) {
                        if (instance == null) {
                            instance = new SessionManager ();
                        }
                    }
                }
                return instance;
            }
        }

        public void login (Employee employee) {
            loggedInEmployee = employee;
        }

        public void logOut () {
            loggedInEmployee = null;
        }
    }
}
