using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PaymentDAL
    {
        private string ConnetctionString = ConfigurationManager.AppSettings.Get("PerPath");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentDAL));

        public List<Payment> GetList()
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                List<Payment> data = new List<Payment>();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT from PAYMENTS";

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Payment(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
                    }

                    dataReader.Close();
                    log.Info("Payments was writed to console");
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

        public int Add(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                int id = 0;

                try
                {
                    command.CommandText = "INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES(@0,@1,@2); Select SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("@0", payment.PersonId);
                    command.Parameters.AddWithValue("@1", payment.Date);
                    command.Parameters.AddWithValue("@2", payment.PaymentAmount);
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

        public List<Payment> GetSearchList(int personID)
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                List<Payment> data = new List<Payment>();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = $"Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT FROM PAYMENTS WHERE PERSON_ID  = @0";
                    command.Parameters.AddWithValue("@0", personID);

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Payment(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
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

        public void Update(int id, char select, int personID, string date, double amount)
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
                            command.CommandText = $"UPDATE PAYMENTS SET PERSON_ID = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", personID);
                            command.Parameters.AddWithValue("@1", id);
                            break;
                        case '2':
                            command.CommandText = $"UPDATE PAYMENTS SET SET R_DATE = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", date);
                            command.Parameters.AddWithValue("@1", id);
                            break;
                        case '3':
                            command.CommandText = $"UPDATE PAYMENTS SET PAYMENT_AMOUNT = @0 WHERE ID = @1";
                            command.Parameters.AddWithValue("@0", amount);
                            command.Parameters.AddWithValue("@1", id);
                            break;
                        case '4':
                            command.CommandText = $"UPDATE PAYMENTS SET PERSON_ID = @0, R_DATE = @1, PAYMENT_AMOUNT = @2 WHERE ID = @3";
                            command.Parameters.AddWithValue("@0", personID);
                            command.Parameters.AddWithValue("@1", date);
                            command.Parameters.AddWithValue("@2", amount);
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
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {

                    command.CommandText = $"DELETE FROM PAYMENTS WHERE ID = @0";
                    command.Parameters.AddWithValue("@0", id);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    log.Error(ex.GetType());
                    log.Error(ex.Message);
                }
                connection.Close();
                log.Info("Payment was deleted.");
            }
        }
    }
}
