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
            string path = ConfigurationManager.AppSettings.Get("PerPath");
            SqlConnection con = new SqlConnection($@"{path}");         
            char select = ' ';

            while (select != '1' || select != '2')
            {
                con.Open();
                Console.Clear();
                Console.WriteLine("Select option:");
                Console.WriteLine("1 - ReadAllPersons ");
                Console.WriteLine("2 - WriteAllDataToJson");
                Console.WriteLine("3 - AddPersonToDB");
                Console.WriteLine("4 - SearchPersonInDB");
                Console.WriteLine("5 - UpdatePhoneNumber");
                Console.WriteLine("6 - DeletePerson");
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
                    case '3':
                        //
                        AddPersonToDataBase(con, CreateNewPerson());
                        break;
                    case '4':
                        //
                        SearchPersonInDataBase(con);
                        break;
                    case '5':
                        // change person phone number from db
                        UpdatePersonPhoneNumber(con);
                        break;
                    case '6':
                        // Delete person from db
                        DeletePerson(con);
                        break;
                    default:
                        Console.WriteLine("Wrong select.");
                        break;
                }
                con.Close();
            }        
        }

        private static void DeletePerson(SqlConnection con)
        {
            Console.Clear();
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the surName:");
            string surName = Console.ReadLine();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"DELETE FROM PERSONS WHERE NAME = '{name}' and SURNAME = '{surName}'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }

        public static void UpdatePersonPhoneNumber(SqlConnection con)
        {
            Console.Clear();
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the surName:");
            string surName = Console.ReadLine();

            Console.Write("Enter new phone number\n +370");
            long telephonNumber = Convert.ToInt32(Console.ReadLine());
            while (telephonNumber.ToString().Length != 8)
            {
                Console.WriteLine("Incorrect entry.");
                Console.Write("Enter phone number\n +370");
                telephonNumber = Convert.ToInt32(Console.ReadLine());
            }
            string phoneNumber = "+370" + telephonNumber;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"UPDATE PERSONS SET PHONENUMBER = '{phoneNumber}' WHERE NAME = '{name}' and SURNAME = '{surName}'";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

           
        }

        public static void SearchPersonInDataBase(SqlConnection con)
        {
            string search = null;
            Console.Clear();
            Console.WriteLine("Make a search:");
            search = Console.ReadLine();

            string sql = $"SELECT * FROM PERSONS WHERE NAME LIKE '%{search}%' OR SURNAME Like '%{search}%' or PHONENUMBER like '%{search}%'";
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader dataReader = command.ExecuteReader();

            Console.WriteLine("Search result:");
            int searchCount = 0;
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)}");
                searchCount++;
            }
            if(searchCount == 0)
            {
                Console.WriteLine("Person not found");
            }
            searchCount = 0;
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }

        public static Person CreateNewPerson()
        {
            Console.Clear();
            Console.WriteLine("Enter the name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the last name.");
            string surName = Console.ReadLine();

            Console.Write("Enter phone number\n +370");
            long telephonNumber = Convert.ToInt32(Console.ReadLine());
            while (telephonNumber.ToString().Length != 8)
            {
                Console.WriteLine("Incorrect entry.");
                Console.Write("Enter phone number\n +370");
                telephonNumber = Convert.ToInt32(Console.ReadLine());
            }
            string telNumberString = "+370" + telephonNumber;

            return new Person(name, surName, telNumberString);
        }

        public static void AddPersonToDataBase(SqlConnection con, Person person)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"INSERT PERSONS VALUES ('{person.GetName()}', '{person.GetSurName()}', '{person.GetPhoneNumber()}')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
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
