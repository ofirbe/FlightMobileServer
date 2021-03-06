﻿using FlightMobileApp.Model.Concrets_Objects;
using FlightMobileApp.Model.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMobileApp.Manager
{
    public interface IConnectionManager
    {
        public Task<Result> Execute(Command cmd);
        public void Start();
        public void ProcessCommands();
        public int Connect(string ip, int port);
        public void Disconnect();
        public int SetAllValues(Command command);
    }
}
