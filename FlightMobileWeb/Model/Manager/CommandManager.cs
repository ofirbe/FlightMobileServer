using FlightMobileApp.Model;
using FlightMobileApp.Model.Concrets_Objects;
using FlightMobileApp.Model.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMobileApp.Manager
{
    public class CommandManager
    {
        private string ip;
        private int port;
        public CommandManager(string IP, int port)
        {
            this.ip = IP;
            this.port = port;
        }

        public string IP { get { return this.ip; } set { this.ip = value; } }
        public int PORT { get { return this.port; } set { this.port = value; } }

    }
}
