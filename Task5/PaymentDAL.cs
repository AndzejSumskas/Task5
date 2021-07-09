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
        internal void PrintAllPayments(SqlCommand command, SqlTransaction transaction)
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

        internal List<Debt> GetListOfPaymentsFromDB(SqlCommand command, SqlTransaction transaction)
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

        internal void AddNewPayMentToDataBase(SqlCommand command, SqlTransaction transaction, Payment payment)
        {
            command.CommandText =
                   $"INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES('{payment.PersonId}', '{payment.Date}', '{payment.PaymentAmount}'); Select @@IDENTITY;";

            int ID = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"Payment with ID {ID} was added to DB");

            transaction.Commit();
        }

        internal void SearchPaymentsInDataBase(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int personID)
        {
            command.CommandText = $"Select ID, PERSON_ID, R_DATE, PAYMENT_AMOUNT FROM PAYMENTS WHERE PERSON_ID  = '{personID}'";
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

        internal void UpdatePaymentData(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id, char select, int personID, string date, double debtAmount)
        {
            switch (select)
            {
                case '1':
                    command.CommandText = $"UPDATE PAYMENTS SET PERSON_ID = '{personID}' WHERE ID = {id}";
                    break;
                case '2':
                    command.CommandText = $"UPDATE PAYMENTS SET SET R_DATE = '{date}' WHERE ID = {id}";
                    break;
                case '3':
                    command.CommandText = $"UPDATE PAYMENTS SET PAYMENT_AMOUNT = '{debtAmount}' WHERE ID = {id}";
                    break;
                case '4':
                    command.CommandText = $"UPDATE PAYMENTS SET PERSON_ID = '{personID}', R_DATE = '{date}', PAYMENT_AMOUNT = '{debtAmount}' WHERE ID = {id}";
                    break;
            }
            Console.WriteLine("Payment data was updated.");
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        internal void DeletePerson(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM PAYMENTS WHERE ID = {id}";
            command.ExecuteNonQuery();

            transaction.Commit();
            Console.WriteLine("Payment was deleted");
        }
    }
}
