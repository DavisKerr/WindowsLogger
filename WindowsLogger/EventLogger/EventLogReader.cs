using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using WindowsLogger;
using System.Threading;

namespace WindowsLogger.EventLogger
{
    class EventLogReader
    {
        EventLog eventLog = new EventLog();

        public EventLogReader()
        {

        }
        public void run()
        {
            string option = "1";

            while(option != "0")
            {
                Console.WriteLine("Which feature would you like to use?");
                Console.WriteLine("(0) - quit");
                Console.WriteLine("(1) - retrieve login data");
                Console.WriteLine("(2) - retrieve login data for a specific user");

                option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        Console.WriteLine("exiting logger...");
                        break;
                    case "1":
                        getLoginData(100);
                        break;
                    case "2":
                        Console.WriteLine("Not implemented");
                        break;
                    default:
                        Console.WriteLine("Command not recognized.");
                        break;
                }
            }
        }

        public void getLoginData(int limit)
        {
            eventLog.Log = "Security";

            

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if(entry.Index < limit)
                {
                    Console.WriteLine("User: " + entry.UserName + "\n");
                    Console.WriteLine("Time: " + entry.TimeWritten + "\n");
                    Console.WriteLine("Message: " + entry.Message + "\n");
                    Console.WriteLine("--------------------------------\n");
                }
                

                
            }
        }
    }
}
