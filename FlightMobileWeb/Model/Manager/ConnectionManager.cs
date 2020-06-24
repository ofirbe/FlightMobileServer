using FlightMobileApp.Model.Concrets_Objects;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using FlightMobileApp.Manager;
using System;

namespace FlightMobileApp.Model.Managers
{
    public enum Result { Ok, NotOk }

    public class AsyncCommand
    {
        public Command Command { get; private set; }
        public Task<Result> Task { get => Completion.Task; }
        public TaskCompletionSource<Result> Completion { get; private set; }
        public AsyncCommand(Command input)
        {
            Command = input;
            Completion = new TaskCompletionSource<Result>(TaskCreationOptions.RunContinuationsAsynchronously);
        }
    }

    public class ConnectionManager : IConnectionManager
    {
        // fields
        private readonly BlockingCollection<AsyncCommand> _queue;
        private ITelnetClient telnetClientHandler;
        private bool isConnected;
        private double readbackGarbage;
        private string ip = "none";
        private int port = -1;
        // CTOR
        public ConnectionManager(CommandManager commandManager)
        {
            this.ip = commandManager.IP;
            this.port = commandManager.PORT;
            _queue = new BlockingCollection<AsyncCommand>();
            this.telnetClientHandler = new MyTelnetClient();
            this.isConnected = false;
            this.Start();
        }

        public Task<Result> Execute(Command cmd)
        {
            var asyncCommand = new AsyncCommand(cmd);
            _queue.Add(asyncCommand);
            return asyncCommand.Task;
        }

        public void Start()
        {
            Task.Factory.StartNew(ProcessCommands);
        }

        public void ProcessCommands()
        {

            int checkConnect = this.connect(this.ip, this.port);
            foreach (AsyncCommand command in _queue.GetConsumingEnumerable())
            {
                Result res;
                try
                {
                    res = (Result)this.setAllValues(command.Command);
                }
                catch
                {
                    res = Result.NotOk;
                }
                command.Completion.SetResult(res);
            }
        }
        /*
        * The method gets string and check if it can be converted to double, by using double.Parse.
        * If yes - return true.
        * If no - turn on the "invalid value" error, and return false.
        */
        private bool tryToConvert(string str)
        {
            try
            {
                double.Parse(str);
                return true;
            }
            catch
            {
                //ERROR
                return false;
            }
        }


        /*
         * The method connect to the server by using the telnet client
         */
        public int connect(string ip, int port)
        {
            int check = 0;
            try
            {
                check = telnetClientHandler.connect(ip, port);
                if (check == 1)
                    isConnected = true;
            }
            catch
            {
                // disconnect in case of error
                this.disconnect();
            }
            return check;
        }

        /*
         * The method disconnect to the server by using the telnet client
         */
        public void disconnect()
        {
            if (isConnected == true)
            {
                this.telnetClientHandler.disconnect();
                isConnected = false;
            }
        }

        /*
         * The method set the all values by the command values it gets and by using the telnet client, and if there is any kind of prolbem in the command sending, the method return 1.
         */
        public int setAllValues(Command command)
        {
            try
            {
                // if (this.isConnected == true)
                if (telnetClientHandler.isConnected())
                {
                    string tmp = "";

                    telnetClientHandler.write("set /controls/flight/aileron " + String.Format("{0:0.##}", command.Aileron) + "\r\n");
                    /*   telnetClientHandler.write("get /controls/flight/aileron" + "\r\n");
                       tmp = telnetClientHandler.read();
                       if (tryToConvert(tmp) == true)
                       {
                           if (command.Aileron != double.Parse(tmp))
                           {
                               return 1; //notOk
                           }
                       }*/

                    telnetClientHandler.write("set /controls/engines/current-engine/throttle " + String.Format("{0:0.##}", command.Throttle) + "\r\n");
                    /* telnetClientHandler.write("get /controls/engines/current-engine/throttle" + "\r\n");
                     tmp = this.telnetClientHandler.read();
                     if (tryToConvert(tmp) == true)
                     {
                         if (command.Throttle != double.Parse(tmp))
                         {
                             return 1; //notOk
                         }
                     }*/
                    telnetClientHandler.write("set /controls/flight/elevator " + String.Format("{0:0.##}", command.Elevator) + "\r\n");
                    /*telnetClientHandler.write("get /controls/flight/elevator" + "\r\n");
                    tmp = this.telnetClientHandler.read();
                    if (tryToConvert(tmp) == true)
                    {
                        if (command.Elevator != double.Parse(tmp))
                        {
                            return 1; //notOk
                        }
                    }*/
                    telnetClientHandler.write("set /controls/flight/rudder " + String.Format("{0:0.##}", command.Rudder) + "\r\n");
                    /* telnetClientHandler.write("get /controls/flight/rudder" + "\r\n");
                     tmp = this.telnetClientHandler.read();
                     if (tryToConvert(tmp) == true)
                     {
                         if (command.Rudder != double.Parse(tmp))
                         {
                             return 1; //notOk
                         }
                     }*/
                }
                return 0; //Ok
            }
            catch
            {
                throw new Exception("Could not read / write"); //notOk
            }
        }
    }
}