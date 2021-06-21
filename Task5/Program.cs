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

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andzej\DataBaseTest.mdf;Integrated Security=True;Connect Timeout=30");         
            char select = ' ';

            while(select != '1' || select != '2')
            {
                con.Open();
                Console.Clear();
                Console.WriteLine("Select option:");
                Console.WriteLine("1 - ReadAllPersons ");
                Console.WriteLine("2 - WriteAllDataToJson");
                select = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (select)
                {
                    case '1':
                        //Reads All Persons from local database(PERSONS)
                        ReadAllPersons(con);
                        break;
                    case '2':
                        //Write All Persons from local database(PERSONS) to json file
                        WriteAllDataToJson(con);
                        break;
                    default:
                        Console.WriteLine("Wrong select.");
                        break;
                }
                con.Close();
            }        
        }

        public static void ReadAllPersons(SqlConnection con)
        {
            Console.Clear();
            SqlCommand command;
            SqlDataReader dataReader;
            string sql = "";

            sql = "Select * from PERSONS";
            command = new SqlCommand(sql, con);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)}");
            }
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }

        public static void WriteAllDataToJson(SqlConnection con)
        {
            Console.Clear();
            SqlCommand command;
            SqlDataReader dataReader;
            string sql = "";
            List<string> data = new List<string>();

            sql = "Select * from PERSONS";
            command = new SqlCommand(sql, con);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)}");
            }
            string json = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);
            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\path.json", json);

            Console.WriteLine("All files was writen to json file.");
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }
    }
}
