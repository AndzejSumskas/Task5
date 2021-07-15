using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class DebtDAL
    {
        internal void WriteToConsole(SqlCommand command, SqlTransaction transaction)
        {
            command.CommandText = "Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT from DEBTS";

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

            command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT from DEBTS";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
            }

            dataReader.Close();
            transaction.Commit();
            return data;
        }

        internal int Add(SqlCommand command, SqlTransaction transaction, Debt debt)
        {
            command.CommandText = $"INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES('{debt.PersonId}', '{debt.Date}', '{debt.Amount}'); Select @@IDENTITY;";

            int id = Convert.ToInt32(command.ExecuteScalar());

            transaction.Commit();
            return id;
        }

        internal List<Debt> GetSearchList(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int personID)
        {
            List<Debt> data = new List<Debt>();

            command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT FROM DEBTS WHERE PERSON_ID  = '{personID}'";
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                data.Add(new Debt(Convert.ToInt32(dataReader.GetValue(0)), Convert.ToInt32(dataReader.GetValue(1)), Convert.ToDateTime(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3).ToString())));
            }
            
            transaction.Commit();
            return data;
        }

        internal void Update(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id, char select, int personID, string date, double debtAmount)
        {
            switch (select)
            {
                case '1':
                    command.CommandText = $"UPDATE DEBTS SET PERSON_ID = '{personID}' WHERE ID = {id}";
                    break;
                case '2':
                    command.CommandText = $"UPDATE DEBTS SET SET R_DATE = '{date}' WHERE ID = {id}";
                    break;
                case '3':
                    command.CommandText = $"UPDATE DEBTS SET DEBT_AMOUNT = '{debtAmount}' WHERE ID = {id}";
                    break;
                case '4':
                    command.CommandText = $"UPDATE DEBTS SET PERSON_ID = '{personID}', R_DATE = '{date}', DEBT_AMOUNT = '{debtAmount}' WHERE ID = {id}";
                    break;
            }
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        internal void Delete(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM DEBTS WHERE ID = {id}";
            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }
}
