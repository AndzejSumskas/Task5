using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class PersonDAL
    {
        internal void ReadAllPersons(SqlCommand command, SqlTransaction transaction )
        {
            command.CommandText = "Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
            }
            dataReader.Close();
            transaction.Commit();          
        }

        internal List<string> GetListOfPersonsFromDB(SqlCommand command, SqlTransaction transaction)
        {
            List<string> data = new List<string>();

            command.CommandText = $"Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
            }

            dataReader.Close();
            transaction.Commit();
            return data;           
        }

        internal void AddNewPersonToDataBase(SqlCommand command, SqlTransaction transaction, Person person)
        {
            command.CommandText =
                   $"INSERT INTO PERSONS(NAME,SURNAME,PHONENUMBER) VALUES('{person.Name}', '{person.SurName}', '{person.PhoneNumber}'); Select @@IDENTITY;";

            int ID = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"Person with {ID} was added to DB");

            transaction.Commit();
        }

        internal void SearchPersonInDataBase(SqlConnection connection, SqlTransaction transaction, SqlCommand command, string search)
        {
            command.CommandText = $"EXEC SearchPersons @Search = '{search}'";
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

        internal void UpdatePersonalData(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id, char select, string name, string surname, string phoneNumber)
        {
            switch (select)
            {
                case '1':
                    command.CommandText = $"UPDATE PERSONS SET NAME = '{name}' WHERE ID = {id}";
                    break;
                case '2':
                    command.CommandText = $"UPDATE PERSONS SET SURNAME = '{surname}' WHERE ID = {id}";
                    break;
                case '3':
                    command.CommandText = $"UPDATE PERSONS SET PHONENUMBER = '{phoneNumber}' WHERE ID = {id}";
                    break;
                case '4':
                    command.CommandText = $"UPDATE PERSONS SET NAME = '{name}',SURNAME = '{surname}', PHONENUMBER = '{phoneNumber}' WHERE ID = {id}";
                    break;
            }
            Console.WriteLine("Person data was updated.");
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        internal void DeletePerson(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM PERSONS WHERE ID = {id}";
            command.ExecuteNonQuery();

            transaction.Commit();
            Console.WriteLine("Person was deleted");
        }
    }
}
