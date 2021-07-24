using log4net;
using System;
using System.Configuration;
using System.IO;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
         {

            string ConnectingString = ConfigurationManager.AppSettings.Get("PerPath");
            
            AppController appController = new AppController();

            appController.StartApplication(ConnectingString);

            Console.ReadKey();

        }        
    }

}
