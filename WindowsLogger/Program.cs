/* When setting up the project, run 
 * Install-Package Microsoft.Extensions.Logging.EventLog
 * in the NuGet terminal
 * 
 * 
 */
using System;
using System.Diagnostics;
using WindowsLogger;
using System.Threading;
using WindowsLogger.EventLogger;

namespace WindowsLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to windows logger!");
            string option = "1";

            while(option != "0")
            {
                Console.WriteLine("Which feature would you like to use?");
                Console.WriteLine("(0) - quit");
                Console.WriteLine("(1) - Event Viewer");
                Console.WriteLine("(2) - KeyLogger");

                option = Console.ReadLine();

                if(option == "0")
                {
                    Console.WriteLine("Exiting...");
                }
                else if(option == "1")
                {
                    EventLogReader reader = new EventLogReader();
                    reader.run();
                }
                else if(option == "2")
                {
                    Console.WriteLine("Not Implemented");
                }
                else
                {
                    Console.WriteLine("Command not recognized. Try again.");
                }
            }
            


        }
    }
}
