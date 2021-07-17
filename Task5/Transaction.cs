using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;




namespace Task5
{
    class Transaction
    {
        private PersonDAL personDAL = new PersonDAL();
        private DebtDAL debtDAL = new DebtDAL();
        private Person person = new Person();
        private Debt debt = new Debt();
        private VariableEntries variableEntries = new VariableEntries();
        EventLogClass eventLogClass = new EventLogClass();

        public void ExecuteSqlTransaction(string connectionString, char select)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                transaction = connection.BeginTransaction("Transaction");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    Console.Clear();

                    switch (select)
                    {
                        case 'a':
                            personDAL.WriteToConsole(command, transaction);

                            //Event log
                            eventLogClass.LogInfo("All persons from db wasr writed to console.");

                            break;
                        case 'b':
                            var persons = personDAL.GetList(command, transaction);
                            List<string> personsData = new List<string>();

                            foreach (var person in persons)
                            {
                                personsData.Add($"{person.Id} {person.Name} {person.SurName} {person.PhoneNumber}");
                            }
                            string json = JsonConvert.SerializeObject(personsData.ToArray(), Formatting.Indented);
                            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\Persons.json", json);
                            Console.WriteLine("All files was writen to json file.");
                            break;
                        case 'c':
                            Person newPerson = person.CreateNewPerson();
                            personDAL.Add(command, transaction, newPerson);
                            break;
                        case 'd':
                            Console.WriteLine("Make a search:");
                            string search = Console.ReadLine();
                            personDAL.GetSearchList(transaction, command, search);
                            break;
                        case 'e':
                            bool updateIsActive = false;
                            string name = "", surName = "", phoneNumber = "";
                            int personId = variableEntries.EnterPersonID();
                            char selectOption = ' ';

                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Select option:");
                                Console.WriteLine("1 - Update name\n2 - Update surname\n3 - Update phone number \n4 - Update all person data");
                                selectOption = Console.ReadKey().KeyChar;
                                switch (selectOption)
                                {
                                    case '1':
                                        name = variableEntries.WriteNewName();
                                        break;
                                    case '2':
                                        surName = variableEntries.WriteNewSurName();
                                        break;
                                    case '3':
                                        phoneNumber = variableEntries.WriteNewPhoneNumber();
                                        break;
                                    case '4':
                                        name = variableEntries.WriteNewName();
                                        surName = variableEntries.WriteNewSurName();
                                        phoneNumber = variableEntries.WriteNewPhoneNumber();
                                        break;
                                    default:
                                        Console.WriteLine("Wrong selection!");
                                        updateIsActive = true;
                                        break;
                                }

                            } while (updateIsActive);

                            if (selectOption == '1' || selectOption == '2' || selectOption == '3' || selectOption == '4')
                            {
                                personDAL.Update(transaction, command, personId, selectOption, name, surName, phoneNumber);
                            }
                            Console.WriteLine("Person was updated");
                            break;
                        case 'f':
                            int idOfPersonToDelete = variableEntries.EnterPersonID();
                            personDAL.Delete(transaction, command, idOfPersonToDelete);
                            break;
                        case 'g':
                            debtDAL.WriteToConsole(command,transaction);
                            break;
                        case 'h':
                            var debts = debtDAL.GetList(command, transaction);
                            List<string> debtsData = new List<string>();

                            foreach (var debt in debts)
                            {
                                debtsData.Add($"{debt.Id} {debt.PersonId} {debt.Date} {debt.Amount}");
                            }
                            string jsonDebt = JsonConvert.SerializeObject(debtsData.ToArray(), Formatting.Indented);
                            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\Debts.json", jsonDebt);
                            Console.WriteLine("All files was writen to json file.");
                            break;
                        case 'i':
                            Debt newDebt = debt.CreateNeDebt();
                            debtDAL.Add(command, transaction, newDebt);
                            break;
                        case 'j':
                            Console.WriteLine("Make a search by person id:");
                            int idSearch = Convert.ToInt32(Console.ReadLine());
                            debtDAL.GetSearchList(transaction, command, idSearch);
                            break;
                        case 'l':
                            bool debtUpdateIsActive = false;
                            int person_id = 0;
                            DateTime date = DateTime.Now;
                            double debtAmount = 0;
                            int IdOfPerson = variableEntries.EnterPersonID();
                            char selectOpt = ' ';

                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Select option:");
                                Console.WriteLine("1 - Update PERSON_ID\n2 - Update R_DATE\n3 - Update DEBT_AMOUNT \n4 - Update all debt data");
                                selectOpt= Console.ReadKey().KeyChar;
                                switch (selectOpt)
                                {
                                    case '1':
                                        person_id = variableEntries.EnterPersonID();
                                        break;
                                    case '2':
                                        date =  DateTime.Now;
                                        break;
                                    case '3':
                                        debtAmount = variableEntries.EnterDebtAmount();
                                        break;
                                    case '4':
                                        person_id = variableEntries.EnterPersonID();
                                        date = DateTime.Now;
                                        debtAmount = variableEntries.EnterDebtAmount();
                                        break;
                                    default:
                                        Console.WriteLine("Wrong selection!");
                                        debtUpdateIsActive = true;
                                        break;
                                }

                            } while (debtUpdateIsActive);

                            if (selectOpt == '1' || selectOpt == '2' || selectOpt == '3' || selectOpt == '4')
                            {
                                debtDAL.Update(transaction, command, IdOfPerson, selectOpt, person_id, date.ToString(), debtAmount);

                            }
                            Console.WriteLine("Person was updated");
                            break;
                        case 'm':
                            int idOfDebtToDelete = variableEntries.EnterPersonID();
                            personDAL.Delete(transaction, command, idOfDebtToDelete);
                            break;
                        case 'n':
                            //Print Name, Surname, debtAountSum
                            command.CommandText = "exec SelectAllPersonsWithDebtSum";

                            SqlDataReader dataReader = command.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)} {dataReader.GetValue(3)}");
                            }
                            dataReader.Close();
                            transaction.Commit();
                            break;
                        case 'o':
                             PrintPersonTableWithDebtPaymentBalanceAmount(command, transaction);
                                                        
                            break;
                        case 'p':
                            command.CommandText = "SELECT p.ID, p.NAME, p.SURNAME, d.DEBT_SUM, pay.PAYMENT_SUM, (d.DEBT_SUM-pay.PAYMENT_SUM) as BALANCE FROM PERSONS p left JOIN(SELECT PERSON_ID, SUM(DEBT_AMOUNT) AS DEBT_SUM FROM DEBTS group by PERSON_ID) d ON p.ID = d.PERSON_ID left JOIN(SELECT PERSON_ID, SUM(PAYMENT_AMOUNT) AS PAYMENT_SUM FROM PAYMENTS group by PERSON_ID) pay ON p.ID = pay.PERSON_ID WHERE d.DEBT_SUM is not null or pay.PAYMENT_SUM is not null";

                            SqlDataReader dataReader1 = command.ExecuteReader();

                            while (dataReader1.Read())
                            {
                                Console.WriteLine($"{dataReader1.GetValue(0)} {dataReader1.GetValue(1)} {dataReader1.GetValue(2)} {dataReader1.GetValue(3)} {dataReader1.GetValue(4)} {dataReader1.GetValue(5)}");
                            }
                            dataReader1.Close();
                            transaction.Commit();
                            break;
                        case 'q':

                            break;
                        default:
                            Console.WriteLine("Wrong select.");
                            break;
                    }

                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    connection.Close();

                    eventLogClass.LogError(ex.Message);


                    Console.WriteLine("Press key to continue...");
                    Console.ReadKey();
                }
            }
        }
        public void PrintPersonTableWithDebtPaymentBalanceAmount(SqlCommand command, SqlTransaction transaction)
        {
            List<Person> persons = new List<Person>();

            command.CommandText = "GET_TABLE_DEBT_PAY_PROCEDURE";

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Person person = new Person();
                person.Id = Convert.ToInt32(dataReader.GetValue(0));
                person.Name = dataReader.GetValue(1).ToString();
                person.SurName = dataReader.GetValue(2).ToString();
                person.DebtSumAmount = Convert.ToDouble(dataReader.GetValue(3));
                person.PaymentSumAmount = Convert.ToDouble(dataReader.GetValue(4));
                person.Balance = person.DebtSumAmount - person.PaymentSumAmount;
                persons.Add(person); 
            }
            dataReader.Close();
            transaction.Commit();

            foreach(Person person in persons)
            {
                Console.WriteLine($"[{person.Id}] [{person.Name}] [{person.SurName}] [{person.DebtSumAmount}] [{person.PaymentSumAmount}] [{person.Balance}]");
            }
        }
    }
}
