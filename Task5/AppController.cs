using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class AppController
    {
        private PersonDAL personDAL = new PersonDAL();
        private List<string> persons = new List<string>();

        public void ExecuteSqlTransaction(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start transaction.
                transaction = connection.BeginTransaction("Transaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    Console.Clear();

                    //personDAL.ReadAllPersons(command, transaction);
                    //persons = personDAL.GetListOfPersonsFromDB(command, transaction);

                    Person person = new Person();
                    person.Name = "Ruta";
                    person.SurName = "Jakimaliene";
                    person.PhoneNumber = "+37065412224";
                    //personDAL.AddNewPersonToDataBase(command, transaction, person);

                    //personDAL.SearchPersonInDataBase(connection, transaction, command, "lo");


                    //personDAL.UpdatePersonalData(connection, transaction, command, 3, '2', "", "Gerdauskas", "");

                    personDAL.DeletePerson(connection, transaction, command, 94);

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
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
            }
        }

    }
}
