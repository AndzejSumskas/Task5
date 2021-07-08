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
        internal void PrintAllDebts(SqlCommand command, SqlTransaction transaction)
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

        internal List<Debt> GetListOfDebtsFromDB(SqlCommand command, SqlTransaction transaction)
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

        internal void AddNewDebtToDataBase(SqlCommand command, SqlTransaction transaction, Debt debt)
        {
            command.CommandText =
                   $"INSERT INTO DEBTS(PERSON_ID, R_DATE, DEBT_AMOUNT) VALUES('{debt.PersonId}', '{debt.Date}', '{debt.DeptAmount}'); Select @@IDENTITY;";

            int ID = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"Debt with ID {ID} was added to DB");

            transaction.Commit();
        }

        internal void SearchDebtsInDataBase(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int personID)
        {
            command.CommandText = $"Select ID, PERSON_ID, R_DATE, DEBT_AMOUNT FROM DEBTS WHERE PERSON_ID  = '{personID}'";
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

        internal void UpdateDebtData(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id, char select, int personID, string date, double debtAmount)
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
            Console.WriteLine("Person data was updated.");
            command.ExecuteNonQuery();
            transaction.Commit();
        }

        internal void DeletePerson(SqlConnection connection, SqlTransaction transaction, SqlCommand command, int id)
        {
            command.CommandText = $"DELETE FROM DEPTS WHERE ID = {id}";
            command.ExecuteNonQuery();

            transaction.Commit();
            Console.WriteLine("Person was deleted");
        }
    }
}
