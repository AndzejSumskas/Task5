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
            AppController appController = new AppController();

            appController.StartApplication();

            Console.ReadKey();
        }        
    }
}
