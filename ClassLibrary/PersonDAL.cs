using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PersonDAL
    {
        public void WriteToConsole(SqlCommand command, SqlTransaction transaction)
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

        public List<Person> GetList(SqlCommand command, SqlTransaction transaction)
        {
            List<Person> data = new List<Person>();

            command.CommandText = $"Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
            }

            dataReader.Close();
            transaction.Commit();
            return data;
        }

        public int Add(SqlCommand command, SqlTransaction transaction, Person person)
        {
            command.CommandText = "INSERT INTO PERSONS(NAME,SURNAME,PHONENUMBER) Values(@0,@1,@2); Select SCOPE_IDENTITY()";
            command.Parameters.AddWithValue("@0", person.Name);
            command.Parameters.AddWithValue("@1", person.SurName);
            command.Parameters.AddWithValue("@2", person.PhoneNumber);
            int id = Convert.ToInt32(command.ExecuteScalar());
            transaction.Commit();
            return id;
        }

        public List<Person> GetSearchList(SqlTransaction transaction, SqlCommand command, string search)
        {
            List<Person> data = new List<Person>();

            command.CommandText = $"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE ID=@3 OR NAME=@0 OR SURNAME=@1 or PHONENUMBER=@2";
            command.Parameters.AddWithValue("@0", search);
            command.Parameters.AddWithValue("@1", search);
            command.Parameters.AddWithValue("@2", search);
            command.Parameters.AddWithValue("@3", search);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
            }
            dataReader.Close();
            transaction.Commit();
            return data;
        }

        public void Update(SqlTransaction transaction, SqlCommand command, int id, char select, string name, string surname, string phoneNumber)
        {
            switch (select)
            {
                case '1':
                    command.CommandText = $"UPDATE PERSONS SET NAME = @0 WHERE ID = @1";
                    command.Parameters.AddWithValue("@0", name);
                    command.Parameters.AddWithValue("@1", id);
                    break;
                case '2':
                    command.CommandText = $"UPDATE PERSONS SET SURNAME = @0 WHERE ID = @1";
                    command.Parameters.AddWithValue("@0", surname);
                    command.Parameters.AddWithValue("@1", id);
                    break;
                case '3':
                    command.CommandText = $"UPDATE PERSONS SET PHONENUMBER = @0 WHERE ID = @1";
                    command.Parameters.AddWithValue("@0", phoneNumber);
                    command.Parameters.AddWithValue("@1", id);
                    break;
                case '4':
                    command.CommandText = $"UPDATE PERSONS SET NAME = @0,SURNAME = @1, PHONENUMBER = @2 WHERE ID = @3";
                    command.Parameters.AddWithValue("@0", name);
                    command.Parameters.AddWithValue("@1", surname);
                    command.Parameters.AddWithValue("@2", phoneNumber);
                    command.Parameters.AddWithValue("@3", id);

                    break;
            }
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        public void Delete(SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM PERSONS WHERE ID = @0";
            command.Parameters.AddWithValue("@0", id);
            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }
}
