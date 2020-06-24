
namespace FlightMobileApp.Model.Manager
{
    public class ScreenshotManager
    {
        private string ip;
        private int port;
        public ScreenshotManager(string IP, int port)
        {
            this.ip = IP;
            this.port = port;
        }

        public string IP { get { return this.ip; } set { this.ip = value; } }
        public int PORT { get { return this.port; } set { this.port = value; } }
    }
}