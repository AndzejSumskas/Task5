using System.Configuration;




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
