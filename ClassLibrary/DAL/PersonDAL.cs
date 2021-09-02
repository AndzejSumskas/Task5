using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ClassLibrary
{
    public class PersonDAL
    {
        private Path path = new Path();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PersonDAL));

        public List<Person> GetList()
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                List<Person> data = new List<Person>();
                command.Connection = connection;
               
                try
                {
                    command.CommandText = $"Select ID, NAME, SURNAME, PHONENUMBER from PERSONS";

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
                    }

                    dataReader.Close();                  
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info("Created list from DB.");
                return data;
            }   
        }

        public int Add(Person person)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                int id = 0;

                try
                {
                    command.CommandText = "INSERT INTO PERSONS(NAME,SURNAME,PHONENUMBER) Values(@0,@1,@2); Select SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("@0", person.Name);
                    command.Parameters.AddWithValue("@1", person.SurName);
                    command.Parameters.AddWithValue("@2", person.PhoneNumber);
                    id = Convert.ToInt32(command.ExecuteScalar());
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info($"Added to DB with id = {id}");
                return id;
            }            
        }

        public List<Person> GetSearchList(string search)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();
                List<Person> data = new List<Person>();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE NAME=@0 OR SURNAME=@1 or PHONENUMBER=@2";
                    command.Parameters.AddWithValue("@0", search);
                    command.Parameters.AddWithValue("@1", search);
                    command.Parameters.AddWithValue("@2", search);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
                    }
                    dataReader.Close();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info($"Search completed.");
                return data;
            }
        }

        public List<Person> GetSearchListByID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();
                List<Person> data = new List<Person>();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE ID=@0";
                    command.Parameters.AddWithValue("@0", ID);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString()));
                    }
                    dataReader.Close();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info($"Search completed.");
                return data;
            }
        }

        public Person GetSearchByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();
                Person person = new Person();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"SELECT ID,NAME,SURNAME,PHONENUMBER FROM PERSONS WHERE ID=@0";
                    command.Parameters.AddWithValue("@0", id);

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        person.Id = Convert.ToInt32(dataReader.GetValue(0));
                        person.Name = dataReader.GetValue(1).ToString();
                        person.SurName = dataReader.GetValue(2).ToString();
                        person.PhoneNumber = dataReader.GetValue(3).ToString();
                    }
                    dataReader.Close();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info($"Search by id completed.");
                return person;
            }       
        }
        public void Update(int id, char select, string name, string surname, string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
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
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info("Data updated.");
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(path.PathToDB))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"DELETE FROM PERSONS WHERE ID = @0";
                    command.Parameters.AddWithValue("@0", id);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info("Person was deleted.");
            }
        }
        
    }
}
