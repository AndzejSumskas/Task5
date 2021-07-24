using log4net;
using System;
using System.Configuration;

namespace Task5
{
    class Program
    {
        static protected ILog log = LogManager.GetLogger("");
        static void Main(string[] args)
         {

            string ConnectingString = ConfigurationManager.AppSettings.Get("PerPath");
            
            AppController appController = new AppController();

            //appController.StartApplication(ConnectingString);

            Console.ReadKey();

        }        
    }

}
