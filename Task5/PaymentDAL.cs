using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class PaymentDAL
    {
        internal void WriteToConsole(SqlCommand command, SqlTransaction transaction)
        {
            command.CommandText = "Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT from PAYMENTS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
            }
            dataReader.Close();
            transaction.Commit();
        }

        internal List<Debt> GetList(SqlCommand command, SqlTransaction transaction)
        {
            List<Debt> data = new List<Debt>();

            command.CommandText = $"Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT from PAYMENTS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
            }

            dataReader.Close();
            transaction.Commit();
            return data;
        }

        internal int Add(SqlCommand command, SqlTransaction transaction, Payment payment)
        {
            command.CommandText = "INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES(@0,@1,@2); Select SCOPE_IDENTITY()";
            command.Parameters.AddWithValue("@0", payment.PersonId);
            command.Parameters.AddWithValue("@1", payment.Date);
            command.Parameters.AddWithValue("@2", payment.PaymentAmount);

            int id = Convert.ToInt32(command.ExecuteScalar());

            transaction.Commit();
            return id;
        }

        internal List<Debt> GetSearchList(SqlTransaction transaction, SqlCommand command, int personID)
        {
            List<Debt> data = new List<Debt>();

            command.CommandText = $"Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT FROM PAYMENTS WHERE PERSON_ID  = @0";
            command.Parameters.AddWithValue("@0", personID);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
            }
            dataReader.Close();
            transaction.Commit();
            return data;
        }

        internal void Update(SqlTransaction transaction, SqlCommand command, int id, char select, int personID, string date, double amount)
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
            transaction.Commit();
        }

        internal void Delete(SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM PAYMENTS WHERE ID = @0";
            command.Parameters.AddWithValue("@0", id);
            command.ExecuteNonQuery();
            transaction.Commit();
        }
    }
}
