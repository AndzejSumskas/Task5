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

            while (select != '9')
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
                Console.WriteLine("9 - Exit");
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
                        WriteAllPersonsToJson(con);
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
                        UpdatePersonalData(con);
                        break;
                    case '6':
                        // Delete person from db
                        DeletePerson(con);
                        break;
                    case '9':
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
            int personId = EnterIDOfPerson();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"DELETE FROM PERSONS WHERE ID = {personId}";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }

        public static void UpdatePersonalData(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;

            int personId = EnterIDOfPerson();
            Console.WriteLine("Select option:");
            Console.WriteLine("1 - Update name\n2 - Update surname\n3 - Update phone number \n4 - Update all person data");
            char select = Console.ReadKey().KeyChar;

            switch (select)
            {
                case '1':
                    cmd.CommandText = $"UPDATE PERSONS SET NAME = '{WriteNewName()}' WHERE ID = {personId}";
                    break;
                case '2':
                    cmd.CommandText = $"UPDATE PERSONS SET SURNAME = '{WriteNewSurName()}' WHERE ID = {personId}";
                    break;
                case '3':
                    cmd.CommandText = $"UPDATE PERSONS SET PHONENUMBER = '{WriteNewPhoneNumber()}' WHERE ID = {personId}";
                    break;
                case '4':
                    cmd.CommandText = $"UPDATE PERSONS SET NAME = '{WriteNewName()}',SURNAME = '{WriteNewSurName()}', PHONENUMBER = '{WriteNewPhoneNumber()}' WHERE ID = {personId}";
                    break;
            }
            cmd.ExecuteNonQuery();
        }
        private static int EnterIDOfPerson()
        {
            Console.WriteLine("Enter the person id whose details you want to change");
            int personId = Convert.ToInt32(Console.ReadLine());
            return personId;
        }
        private static string WriteNewName()
        {
            Console.WriteLine("Enter the name:");
            string Name = Console.ReadLine();
            return Name;
        }
        private static string WriteNewSurName()
        {
            Console.WriteLine("Enter the surname:");
            string surName = Console.ReadLine();
            return surName;
        }
        private static string WriteNewPhoneNumber()
        {
            Console.Write("Enter new phone number\n +370");
            long telephonNumber = Convert.ToInt32(Console.ReadLine());
            while (telephonNumber.ToString().Length != 8)
            {
                Console.WriteLine("Incorrect entry.");
                Console.Write("Enter phone number\n +370");
                telephonNumber = Convert.ToInt32(Console.ReadLine());
            }
            return "+370" + telephonNumber;
        }

        public static void SearchPersonInDataBase(SqlConnection con)
        {
            Console.Clear();
            Console.WriteLine("Make a search:");
            string search = Console.ReadLine();

            string sql = $"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE NAME LIKE '%{search}%' OR SURNAME Like '%{search}%' or PHONENUMBER like '%{search}%' or ID = {search}";
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataReader dataReader = command.ExecuteReader();

            Console.WriteLine("Search result:");
            int searchCount = 0;
            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
                searchCount++;
            }
            if (searchCount == 0)
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
            Person person = new Person();
            person.Name = WriteNewName();
            person.SurName = WriteNewSurName();
            person.PhoneNumber = WriteNewPhoneNumber();

            return person;
        }

        public static void AddPersonToDataBase(SqlConnection con, Person person)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"INSERT INTO PERSONS VALUES ('{person.Name}', '{person.SurName}', '{person.PhoneNumber}')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }


        public static void ReadAllPersons(SqlConnection con)
        {
            Console.Clear();
            string sql = "Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";
            SqlCommand command = new SqlCommand(sql, con); ;
            SqlDataReader dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
            }
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }

        public static void WriteAllPersonsToJson(SqlConnection con)
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
                data.Add($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
            }
            string json = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);
            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\path.json", json);

            Console.WriteLine("All files was writen to json file.");
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }
    }
}
