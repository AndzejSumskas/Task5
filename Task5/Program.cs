using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using Microsoft.Build.Framework.XamlTypes;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
         {

            string path = ConfigurationManager.AppSettings.Get("PerPath");
            
            AppController appController = new AppController();

            //appController.ExecuteSqlTransaction(path);

            appController.StartApplication(path);








































                        //Person person = new Person();
                        //DataAccessLayer dataAccessLayer = new DataAccessLayer();
                        //string path = ConfigurationManager.AppSettings.Get("PerPath");
                        //SqlConnection con = new SqlConnection($@"{path}");         
                        //char select = ' ';

            //while (select != '9')
            //{
            //    using (SqlConnection connection = new SqlConnection(
            //  $@"{path}"))
            //    {
            //        try
            //        {
            //            Console.Clear();
            //            Console.WriteLine("Select option:");
            //            Console.WriteLine("1 - ReadAllPersons ");
            //            Console.WriteLine("2 - WriteAllDataToJson");
            //            Console.WriteLine("3 - AddPersonToDB");
            //            Console.WriteLine("4 - SearchPersonInDB");
            //            Console.WriteLine("5 - UpdatePersonData");
            //            Console.WriteLine("6 - DeletePerson");
            //            Console.WriteLine("9 - Exit");
            //            select = Console.ReadKey().KeyChar;
            //            Console.WriteLine();
            //            Console.Clear();
            //            switch (select)
            //            {
            //                case '1':
            //                    //Reads All Persons from local database(PERSONS)
            //                    dataAccessLayer.ReadAllPersons(con);
            //                    break;
            //                case '2':
            //                    //Write All Persons from local database(PERSONS) to json file
            //                    dataAccessLayer.WriteAllPersonsToJson(con);
            //                    break;
            //                case '3':
            //                    //
            //                    dataAccessLayer.AddPersonToDataBase(con, person.CreateNewPerson());
            //                    break;
            //                case '4':
            //                    //
            //                    dataAccessLayer.SearchPersonInDataBase(con);
            //                    break;
            //                case '5':
            //                    // change person phone number from db
            //                    dataAccessLayer.UpdatePersonalData(con);
            //                    break;
            //                case '6':
            //                    // Delete person from db
            //                    dataAccessLayer.DeletePerson(con);
            //                    break;
            //                case '9':
            //                    break;
            //                default:
            //                    Console.WriteLine("Wrong select.");
            //                    break;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //            Console.WriteLine("Something go wrong! ");
            //            MessageBox.Show("Something go wrong! ");
            //        }

            //    }
        }        
    }


}
