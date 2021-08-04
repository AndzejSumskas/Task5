﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using ClassLibrary;




namespace Task5
{
    public class Transaction
    {
        private PersonDAL personDAL = new PersonDAL();
        private DebtDAL debtDAL = new DebtDAL();
        private VariableEntries variableEntries = new VariableEntries();
        LogHelper logHelper = new LogHelper();

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
                            var persons1 = personDAL.GetList();

                            foreach (var person in persons1)
                            {
                                Console.WriteLine($"{person.Id} {person.Name} {person.SurName} {person.PhoneNumber} ");
                            }
                            logHelper.GetLog().Info("Persons was writed to console");
                            break;
                        case 'b':
                            var persons = personDAL.GetList();
                            List<string> personsData = new List<string>();

                            foreach (var person in persons)
                            {
                                personsData.Add($"{person.Id} {person.Name} {person.SurName} {person.PhoneNumber}");
                            }
                            string json = JsonConvert.SerializeObject(personsData.ToArray(), Formatting.Indented);
                            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\Persons.json", json);
                            logHelper.GetLog().Info("All files was writen to json file.");
                            break;
                        case 'c':
                            Person newPerson = CreateNewPerson();
                            int addedId = personDAL.Add(newPerson);
                            logHelper.GetLog().Info("Added person id = " + addedId);
                            //Console.WriteLine("Added person id = " + addedId);
                            break;
                        case 'd':
                            Console.WriteLine("Make a search:");
                            string search = Console.ReadLine();
                            var data = personDAL.GetSearchList(search);
                            foreach (var person in data)
                            {
                                Console.WriteLine($"{person.Id} {person.Name} {person.SurName} {person.PhoneNumber}");
                            }
                            logHelper.GetLog().Info("Search completed");
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
                                personDAL.Update(personId, selectOption, name, surName, phoneNumber);
                            }
                            logHelper.GetLog().Info("Person was updated");
                            break;
                        case 'f':
                            int idOfPersonToDelete = variableEntries.EnterPersonID();
                            personDAL.Delete(idOfPersonToDelete);
                            logHelper.GetLog().Info("Person was deleted");
                            break;
                        case 'g':
                            var debts1 = debtDAL.GetList();
                            foreach (var debt in debts1)
                            {
                                Console.WriteLine($"{debt.Id} {debt.PersonId} {debt.Date} {debt.Amount}");
                            }
                            logHelper.GetLog().Info("Debts was writed to console.");
                            break;
                        case 'h':
                            var debts = debtDAL.GetList();
                            List<string> debtsData = new List<string>();

                            foreach (var debt in debts)
                            {
                                debtsData.Add($"{debt.Id} {debt.PersonId} {debt.Date} {debt.Amount}");
                            }
                            string jsonDebt = JsonConvert.SerializeObject(debtsData.ToArray(), Formatting.Indented);
                            File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\Debts.json", jsonDebt);
                            //Console.WriteLine("All files was writen to json file.");
                            logHelper.GetLog().Info("All files was writen to json file.");
                            break;
                        case 'i':
                            Debt newDebt = CreateNeDebt();
                            int addedID = debtDAL.Add(newDebt);
                            //Console.WriteLine($"Added debt with id {addedID}");
                            logHelper.GetLog().Info($"Added debt with id {addedID}.");
                            break;
                        case 'j':
                            Console.WriteLine("Make a search by person id:");
                            int idSearch = Convert.ToInt32(Console.ReadLine());
                            var searchData = debtDAL.GetSearchList(idSearch);
                            foreach (var debt in searchData)
                            {
                                Console.WriteLine($"{debt.Id} {debt.PersonId} {debt.Date} {debt.Amount}");
                            }
                            logHelper.GetLog().Info("Search completed");
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
                                debtDAL.Update(IdOfPerson, selectOpt, person_id, date.ToString(), debtAmount);

                            }
                            //Console.WriteLine("Debt was updated");
                            logHelper.GetLog().Info("Debt was updated");
                            break;
                        case 'm':
                            int idOfDebtToDelete = variableEntries.EnterPersonID();
                            personDAL.Delete(idOfDebtToDelete);
                            logHelper.GetLog().Info("Debt was deleted");
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
                            logHelper.GetLog().Info("SelectAllPersonsWithDebtSum");
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
                            logHelper.GetLog().Info("Select All Persons With DebtSum PaymnetSum Balance");
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
                        logHelper.GetLog().Error(ex2);
                    }
                    connection.Close();

                    logHelper.GetLog().Error(ex);
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

        VariableEntries textEntries = new VariableEntries();

        public Person CreateNewPerson()
        {
            Console.Clear();
            Person person = new Person();
            person.Name = textEntries.WriteNewName();
            person.SurName = textEntries.WriteNewSurName();
            person.PhoneNumber = textEntries.WriteNewPhoneNumber();

            return person;
        }

        internal Debt CreateNeDebt()
        {
            Debt debt = new Debt(variableEntries.EnterPersonID(), DateTime.Now, variableEntries.EnterDebtAmount());

            return debt;
        }

        public List<Person> ExecuteSqlTransaction2(int numberOfAction, int id, Person person)
        {
            string ConnectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andzej\DataBaseTest.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection connection = new SqlConnection(ConnectingString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                transaction = connection.BeginTransaction("Transaction");

                command.Connection = connection;
                command.Transaction = transaction;
                List<Person> persons = new List<Person>();

                try
                {
                    switch (numberOfAction)
                    {
                        case 1:
                            persons = personDAL.GetList();
                            break;
                        case 2:
                            persons = personDAL.GetSearchByID(id);
                            break;
                        case 3:
                            personDAL.Add(person);
                            break;
                        case 4:
                            personDAL.Delete( id);
                            break;
                        case 5:
                            string name = "Teodoras";
                            string surName = "Ruzveltas";
                            string phoneNumber = "+37062541236";
                            personDAL.Update(id, '1', name, surName, phoneNumber);
                            break;
                    }
                    
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {

                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    connection.Close();     
                }
                return persons;
            }
        }
    }
}
