using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DebtDAL
    {
        private string ConnetctionString = ConfigurationManager.AppSettings.Get("PerPath");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DebtDAL));

        public List<Debt> GetList()
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                List<Debt> data = new List<Debt>();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT from DEBTS";

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
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

        public int Add(Debt debt)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                int id = 0;

                try
                {
                    command.CommandText = $"INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES(@0,@1,@2); Select SCOPE_IDENTITY();";
                    command.Parameters.AddWithValue("@0", debt.PersonId);
                    command.Parameters.AddWithValue("@1", debt.Date);
                    command.Parameters.AddWithValue("@2", debt.Amount);

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

        public List<Debt> GetSearchList(int personID)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                List<Debt> data = new List<Debt>();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT FROM DEBTS WHERE PERSON_ID  = @0";
                    command.Parameters.AddWithValue("@0", personID);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
                    }
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

        public Debt GetSearchById(int ID)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                Debt debt = new Debt();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT FROM DEBTS WHERE ID  = @0";
                    command.Parameters.AddWithValue("@0", ID);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        debt.Id = Convert.ToInt32(dataReader.GetValue(0));
                        debt.PersonId = Convert.ToInt32(dataReader.GetValue(1));
                        debt.Date = Convert.ToDateTime(dataReader.GetValue(2));
                        debt.Amount = Convert.ToDouble(dataReader.GetValue(3));    
                    }
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info($"Search by id completed.");
                return debt;
            }
        }

        public void Update(char select, Debt debt)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    switch (select)
                    {
                        case '1':
                            command.CommandText = $"UPDATE DEBTS SET PERSON_ID = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", debt.PersonId);
                            command.Parameters.AddWithValue("@1", debt.Id);
                            break;
                        case '2':
                            command.CommandText = $"UPDATE DEBTS SET SET R_DATE = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", debt.Date);
                            command.Parameters.AddWithValue("@1", debt.Id);
                            break;
                        case '3':
                            command.CommandText = $"UPDATE DEBTS SET DEBT_AMOUNT = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", debt.Amount);
                            command.Parameters.AddWithValue("@1", debt.Id);
                            break;
                        case '4':
                            command.CommandText = $"UPDATE DEBTS SET PERSON_ID = @0, R_DATE = @1, DEBT_AMOUNT = @2 WHERE ID = @3";
                            command.Parameters.AddWithValue("@0", debt.PersonId);
                            command.Parameters.AddWithValue("@1", debt.Date);
                            command.Parameters.AddWithValue("@2", debt.Amount);
                            command.Parameters.AddWithValue("@3", debt.Id);
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
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"DELETE FROM DEBTS WHERE ID = @0";
                    command.Parameters.AddWithValue("@0", id);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info("Debt was deleted.");
            }         
        }
    }
}
