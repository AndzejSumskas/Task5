using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ExtraDAL
    {
        private string ConnetctionString = ConfigurationManager.AppSettings.Get("PerPath");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ExtraDAL));

        public List<Person> GetPersonListWithSum()
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                List<Person> data = new List<Person>();
                command.Connection = connection;

                try
                {
                    command.CommandText = "exec SelectAllPersonsWithDebtSum";

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), Convert.ToDouble(dataReader.GetValue(3))));
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

        public List<Person> GetPersonListWithDebtSumPaymentSumBalance()
        {
            using (SqlConnection connection = new SqlConnection(ConnetctionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                List<Person> data = new List<Person>();
                command.Connection = connection;

                try
                {
                    command.CommandText = "SELECT p.ID, p.NAME, p.SURNAME, d.DEBT_SUM, pay.PAYMENT_SUM, (d.DEBT_SUM-pay.PAYMENT_SUM) as BALANCE FROM PERSONS p left JOIN(SELECT PERSON_ID, SUM(DEBT_AMOUNT) AS DEBT_SUM FROM DEBTS group by PERSON_ID) d ON p.ID = d.PERSON_ID left JOIN(SELECT PERSON_ID, SUM(PAYMENT_AMOUNT) AS PAYMENT_SUM FROM PAYMENTS group by PERSON_ID) pay ON p.ID = pay.PERSON_ID WHERE d.DEBT_SUM is not null or pay.PAYMENT_SUM is not null";

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(new Person(Convert.ToInt32(dataReader.GetValue(0)), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), Convert.ToDouble(dataReader.GetValue(3)), Convert.ToDouble(dataReader.GetValue(4)), Convert.ToDouble(dataReader.GetValue(5))));
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
    }
}
