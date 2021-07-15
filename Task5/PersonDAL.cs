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
        internal void WriteToConsole(SqlCommand command, SqlTransaction transaction )
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

        internal List<Person> GetList(SqlCommand command, SqlTransaction transaction)
        {
            List<Person> data = new List<Person>();

            command.CommandText = $"Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Person(Convert.ToInt32( dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
            }

            dataReader.Close();
            transaction.Commit();
            return data;           
        }

        internal int Add(SqlCommand command, SqlTransaction transaction, Person person)
        {
            command.CommandText = "INSERT INTO PERSONS(NAME,SURNAME,PHONENUMBER) Values(@0,@1,@2); Select SCOPE_IDENTITY()";
            command.Parameters.AddWithValue("@0", person.Name);
            command.Parameters.AddWithValue("@1", person.SurName);
            command.Parameters.AddWithValue("@2", person.PhoneNumber);
            int id = Convert.ToInt32(command.ExecuteScalar());
            transaction.Commit();
            return id;
        }

        internal List<Person> GetSearchList(SqlConnection connection, SqlTransaction transaction, SqlCommand command, string search)
        {
            List<Person> data = new List<Person>();

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
            }
            dataReader.Close();
            transaction.Commit();
            return data;
        }

        internal void Update(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id, char select, string name, string surname, string phoneNumber)
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
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        internal void Delete(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM PERSONS WHERE ID = {id}";
            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }
}
