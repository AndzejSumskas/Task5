using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class DataAccessLayer
    {
       public SqlTransaction transaction { get; set; }

        VariableEntries textEntries= new VariableEntries();

        public void SearchPersonInDataBase(SqlConnection connection)
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction("Transaction");

            command.Connection = connection;
            command.Transaction = transaction;          
            
            try
            {
                Console.Clear();
                Console.WriteLine("Make a search:");
                string search = Console.ReadLine();
                command.CommandText = $"exec SearchAllPersons @Search = '{search}'";
                    //$"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE NAME LIKE '%{search}%' OR SURNAME Like '%{search}%' or PHONENUMBER like '%{search}%'";
                int searchCount = 0;
                SqlDataReader dataReader = command.ExecuteReader();
                Console.WriteLine("Search result:");
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
                    searchCount++;
                }
                if (searchCount == 0)
                {
                    Console.WriteLine("Person not found");
                }
                Console.WriteLine("Press key to continue...");
                Console.ReadKey();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            connection.Close();
        }

        public void ReadAllPersons(SqlConnection connection)
        {
            Console.Clear();
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                transaction = connection.BeginTransaction("ReadAllPersonsTransaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText =
                         "Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
                    }
                    dataReader.Close();
                    transaction.Commit();
                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
                connection.Close();
        }

        public void WriteAllPersonsToJson(SqlConnection connection)
        {
            Console.Clear();
            SqlDataReader dataReader;
            List<string> data = new List<string>();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction("WriteAllPersonsToJsonTransaction");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandText =
                     $"Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";
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
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            connection.Close();

        }

        internal void DeletePerson(SqlConnection connection)
        {
            int personId = textEntries.EnterPersonID();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction("SampleTransaction");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandText =
                     $"DELETE FROM PERSONS WHERE ID = {personId}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Person was deleted");
                Console.WriteLine("Press key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            connection.Close();
        }

        public void UpdatePersonalData(SqlConnection connection)
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction("UpdatePersonTransaction");

            command.Connection = connection;
            command.Transaction = transaction;

            int personId = textEntries.EnterPersonID();
            Console.WriteLine("Select option:");
            Console.WriteLine("1 - Update name\n2 - Update surname\n3 - Update phone number \n4 - Update all person data");
            char select = Console.ReadKey().KeyChar;

            switch (select)
            {
                case '1':
                    command.CommandText = $"BEGIN TRANSACTION; UPDATE PERSONS SET NAME = '{textEntries.WriteNewName()}' WHERE ID = {personId}; COMMIT TRANSACTION;";
                    break;
                case '2':
                    command.CommandText = $"BEGIN TRANSACTION; UPDATE PERSONS SET SURNAME = '{textEntries.WriteNewSurName()}' WHERE ID = {personId}; COMMIT TRANSACTION;";
                    break;
                case '3':
                    command.CommandText = $"BEGIN TRANSACTION; UPDATE PERSONS SET PHONENUMBER = '{textEntries.WriteNewPhoneNumber()}' WHERE ID = {personId}; COMMIT TRANSACTION;";
                    break;
                case '4':
                    command.CommandText = $"BEGIN TRANSACTION; UPDATE PERSONS SET NAME = '{textEntries.WriteNewName()}',SURNAME = '{textEntries.WriteNewSurName()}', PHONENUMBER = '{textEntries.WriteNewPhoneNumber()}' WHERE ID = {personId}; COMMIT TRANSACTION;";
                    break;
                default:
                    Console.WriteLine("Wrong input.");
                    Console.WriteLine("Select option:");
                    Console.WriteLine("1 - Update name\n2 - Update surname\n3 - Update phone number \n4 - Update all person data");
                    break;
            }

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();

                Console.WriteLine($"Person data was updated.");
                Console.WriteLine("Press key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
            }
            connection.Close();
        }
  
        public void AddPersonToDataBase(SqlConnection connection, Person person)
        {
            int modified = 0;

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction("AddNewPersonTransaction");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandText =
                    $"INSERT INTO PERSONS(NAME,SURNAME,PHONENUMBER) output INSERTED.ID VALUES('{person.Name}', '{person.SurName}', '{person.PhoneNumber}')";
                command.ExecuteNonQuery();
                modified = (int)command.ExecuteScalar();

                transaction.Commit();


                Console.WriteLine($"Last added peson ID = {modified}");
                Console.WriteLine("Press key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
            }
            connection.Close();
        }
    }
}
