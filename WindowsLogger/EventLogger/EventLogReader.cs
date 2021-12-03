using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Text;
using WindowsLogger;
using System.Threading;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                Console.WriteLine("(2) - retrieve application error data");
                Console.WriteLine("(3) - retrieve system data");
                //Console.WriteLine("(2) - retrieve login data for a specific user");

                option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        Console.WriteLine("exiting logger...");
                        break;
                    case "1":
                        string loginData = getLoginData();
                        using (StreamWriter writer = new StreamWriter("./loginData.txt"))
                        {
                            writer.Write(loginData);
                        }
                        Console.WriteLine("Wrote to file.");
                            break;
                    case "2":
                        string appData = getApplicationData();
                        using (StreamWriter writer = new StreamWriter("./appData.txt"))
                        {
                            writer.Write(appData);
                        }
                        Console.WriteLine("Wrote to file.");
                        break;
                    case "3":
                        string sysData = getSystemData();
                        using (StreamWriter writer = new StreamWriter("./sysData.txt"))
                        {
                            writer.Write(sysData);
                        }
                        Console.WriteLine("Wrote to file.");
                        break;
                    default:
                        Console.WriteLine("Command not recognized");
                        break;
                }
            }
        }

        public string getSystemData()
        {
            eventLog.Log = "System";
            string finalText = string.Empty;

            foreach (EventLogEntry entry in eventLog.Entries)
            {

                finalText += "Event Type: " + entry.EntryType + "\n";
                finalText += "Time: " + entry.TimeWritten + "\n";
                finalText += entry.Message + "\n";
                finalText += "---------------------------------\n";
            }

            return finalText;
        }

        public string getApplicationData()
        {
            eventLog.Log = "Application";
            string finalText = string.Empty;

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if(entry.EntryType.ToString() == "Error")
                {
                    finalText += "Error:\n";
                    finalText += "Time: " + entry.TimeWritten + "\n"; 
                    finalText += entry.Message + "\n";
                    finalText += "---------------------------------\n";
                }
            }

            return finalText;
        }

        public string getLoginData()
        {
            eventLog.Log = "Security";


            string prevDate = string.Empty;
            string finalText = string.Empty;

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if(true/*entry.TimeWritten.ToString() != prevDate*/)
                {
                    string message = entry.Message;
                    string user = parseTextForLine("Account Name:", message);
                    string text = "";
                    prevDate = entry.TimeWritten.ToString();

                    finalText += "Time: " + entry.TimeWritten.ToString() + "\n";

                    finalText += user + "\n";

                    using (StringReader reader = new StringReader(message))
                    {
                        text = reader.ReadLine();
                    }

                    finalText += "Message: " + text + "\n";
                    finalText += "-----------------------------------------" + '\n';
                }
                
                
            }

            return finalText;
        }

        public string parseTextForLine(string keyword, string input)
        {
            string found = string.Empty;

            using (StringReader reader = new StringReader(input))
            {
                string line = "EMPTY";

                while (line != null)
                {
                    line = reader.ReadLine();

                    if(line != null)
                    {
                        if(line.Contains(keyword))
                        {
                            found = line;
                        }
                    }
                }
 
            }

            return found;
        }
        

    }
}
