using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Task5
{
    class Program
    {
        static void Main(string[] args)
         {

            string ConnectingString = ConfigurationManager.AppSettings.Get("PerPath");
            
            AppController appController = new AppController();

            appController.StartApplication(ConnectingString);
   
        }        
    }


}
