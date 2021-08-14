using ClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Task5
{
    class AppController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AppController));

        private PersonDAL personDAL = new PersonDAL();
        private DebtDAL debtDAL = new DebtDAL();
        private ExtraDAL extraDAL = new ExtraDAL();
        private VariableEntries variableEntries = new VariableEntries();

        public void StartApplication()
        {
            log.Info("The application has started.");
            char mainMenuSelect;
            bool mainMenuActive = true;
            while(mainMenuActive)
            {
                bool selectActive = true;
                mainMenuSelect = MainMenuSelect();
                switch (mainMenuSelect)
                {
                    case '1':
                        while(selectActive)
                        {
                            log.Info("Actions with Persons menu.");
                            char personMenuSelect = PersonMenuSelect();
                            ActionsWithPersons(personMenuSelect);
                            if(personMenuSelect == 'q')
                            {
                                selectActive = false;
                            }
                        }                        
                        break;
                    case '2':
                        while (selectActive)
                        {
                            log.Info("Actions with Debts menu.");
                            char debtMenuSelect = DebtMenuSelect();
                            ActionsWithDebts(debtMenuSelect);
                            if (debtMenuSelect == 'q')
                            {
                                selectActive = false;
                            }
                        }
                        break;
                    case '3':
                        break;
                    case '4':
                        log.Info("Actions with Persons, Debts and Payments menu.");
                        ActionMenuPersonsDebtsPAyments();
                        break;
                    case 'q':
                        mainMenuActive = false;
                        break;
                    default:
                        log.Warn("Wrong selection.");
                        break;
                }
            }
        }

        private char MainMenuSelect()
        {
                Console.Clear();
                Console.WriteLine("1 - Actions with Persons");
                Console.WriteLine("2 - Actions with Debts");
                Console.WriteLine("3 - Actions with Payments");
                Console.WriteLine("4 - Actions with Persons, Debts and Payments");
                Console.WriteLine("q - Exit");
         
                return Console.ReadKey().KeyChar;
        }

        private char PersonMenuSelect()
        {
            Console.Clear();
            Console.WriteLine("#Actions with Persons#");
            Console.WriteLine("1 - Write all data to console");
            Console.WriteLine("2 - Write all data to json file");
            Console.WriteLine("3 - Add");
            Console.WriteLine("4 - Search");
            Console.WriteLine("5 - Search by id");
            Console.WriteLine("6 - Update");
            Console.WriteLine("7 - Delete");
            Console.WriteLine("q - Exit");

            return Console.ReadKey().KeyChar;
        }

        private void ActionsWithPersons(char select)
        {
            switch(select)
            {
                case '1':
                    var persons = personDAL.GetList();

                    foreach (var person1 in persons)
                    {
                        Console.WriteLine($"{person1.Id} {person1.Name} {person1.SurName} {person1.PhoneNumber} ");
                    }
                    log.Info("Persons from DB was writed to console.");
                    break;
                case '2':
                    var persons2 = personDAL.GetList();
                    List<string> personsData = new List<string>();

                    foreach (var person2 in persons2)
                    {
                        personsData.Add($"{person2.Id} {person2.Name} {person2.SurName} {person2.PhoneNumber}");
                    }
                    string json = JsonConvert.SerializeObject(personsData.ToArray(), Formatting.Indented);
                    File.WriteAllText(@"C:\Users\Andzej\Desktop\IT Tasks\HomeWork\Task5\Task5\Persons.json", json);
                    log.Info("Persons data was writed to json file.");
                    break;
                case '3':
                    Person newPerson = CreateNewPerson();
                    int addedId = personDAL.Add(newPerson);
                    log.Info("Person added to DB.");
                    break;
                case '4':
                    Console.WriteLine("Make a search:");
                    string search = Console.ReadLine();
                    var data = personDAL.GetSearchList(search);
                    foreach (var person4 in data)
                    {
                        Console.WriteLine($"{person4.Id} {person4.Name} {person4.SurName} {person4.PhoneNumber}");
                    }
                    log.Info("Search results writed to console.");
                    break;
                case '5':
                    Console.WriteLine("Make a search by id:");
                    int search1 = Convert.ToInt32(Console.ReadLine());
                    Person person = personDAL.GetSearchByID(search1);
                    Console.Clear();
                    Console.WriteLine($"{person.Id} {person.Name} {person.SurName} {person.PhoneNumber}");
                    Console.ReadKey();
                    log.Info("Search results writed to console.");
                    break;
                case '6':
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
                                log.Warn("Wrong select was maded.");
                                break;
                        }
                    } while (updateIsActive);

                    if (selectOption == '1' || selectOption == '2' || selectOption == '3' || selectOption == '4')
                    {
                        personDAL.Update(personId, selectOption, name, surName, phoneNumber);
                    }
                    break;
                case '7':
                    int idOfPersonToDelete = variableEntries.EnterPersonID();
                    personDAL.Delete(idOfPersonToDelete);
                    break;
                case 'q':
                    log.Info("Returned to the main menu.");
                    break;
                default:
                    log.Warn("Wrong selection.");
                    break;
            }
            Console.WriteLine("Press button...");
            Console.ReadKey();
        }
      
        public Person CreateNewPerson()
        {
            Console.Clear();
            Person person = new Person();
            person.Name = variableEntries.WriteNewName();
            person.SurName = variableEntries.WriteNewSurName();
            person.PhoneNumber = variableEntries.WriteNewPhoneNumber();
            log.Info("Person was created.");
            return person;
        }

        private void ActionMenuPersonsDebtsPAyments()
        {
            bool selectActive = true;
            do
            {
                Console.Clear();
                Console.WriteLine("#Actions with Persons, Debts and Payments#");
                Console.WriteLine("1 - Write all Persons with Debt sum to console.");
                Console.WriteLine("2 - Write all Persons with DebtSum, PaymentSum, Balance.");
                Console.WriteLine("q - Exit");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var persons = extraDAL.GetPersonListWithSum();

                        foreach (var person in persons)
                        {
                            Console.WriteLine($"{person.Id} {person.Name} {person.SurName} {person.DebtSumAmount}");
                        }
                        Console.ReadKey();
                        log.Info("Persons with debt sum writed to console.");
                        break;
                    case '2':
                        var persons1 = extraDAL.GetPersonListWithDebtSumPaymentSumBalance();

                        foreach (var person in persons1)
                        {
                            Console.WriteLine($"{person.Id} {person.Name} {person.SurName} {person.DebtSumAmount} {person.PaymentSumAmount} {person.Balance}");
                        }
                        Console.ReadKey();
                        log.Info("Persons with debt sum, payment sum and balance writed to console.");
                        break;
                    case 'q':
                        selectActive = false;
                        log.Info("Returned to the main menu.");
                        break;
                    default:
                        log.Warn("Wrong select was maded.");
                        break;
                }

            } while (selectActive);
            
        }

        private char DebtMenuSelect()
        {
            Console.Clear();
            Console.WriteLine("#Actions with Persons#");
            Console.WriteLine("1 - Write all data to console");          
            Console.WriteLine("2 - Add");
            Console.WriteLine("3 - Search");
            Console.WriteLine("4 - Update");
            Console.WriteLine("5 - Delete");
            Console.WriteLine("q - Exit");

            return Console.ReadKey().KeyChar;
        }

        public void ActionsWithDebts(char select)
        {
            Console.Clear();
            switch (select)
            {
                case '1':
                    List<Debt> debts = debtDAL.GetList();
                    foreach (var debt in debts)
                    {
                        debt.Render(debt);
                    }
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("Write person id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Person person = personDAL.GetSearchByID(id);
                    if (person.Name != null)
                    {
                        double debtAmount = 125;
                        debtDAL.Add(CreateDebt(id, debtAmount));
                        Console.WriteLine("Debt added.");
                    }
                    else
                    {
                        Console.WriteLine("There is no Person with id: {0}", id);
                    }
                    Console.WriteLine("Press key to contonue...");
                    Console.ReadKey();
                    break;
                case '3':
                    Console.Clear();
                    Console.WriteLine("Write person id:");
                    int personId = Convert.ToInt32(Console.ReadLine());
                    List<Debt> debts3 = debtDAL.GetSearchList(personId);
                    if (debts3 != null)
                    {
                        foreach (var debt in debts3)
                        {
                            debt.Render(debt);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Person with id: {0} do not have any debts", personId);
                    }

                    Console.WriteLine("Press key to contonue...");
                    Console.ReadKey();
                    break;
                case '4':
                    Console.Clear();
                    select = Console.ReadKey().KeyChar;

                    int personID = 0;
                    double debtAmount4 = 0;

                    Console.WriteLine("Write debt id:");
                    int id4 = Convert.ToInt32(Console.ReadLine());

                    switch (select)
                    {
                        case '1':
                            Console.WriteLine("Write person id:");
                            personID = Convert.ToInt32(Console.ReadLine());
                            break;
                        case '2':

                            break;
                        case '3':
                            Console.WriteLine("Write debt amount:");
                            debtAmount4 = Convert.ToDouble(Console.ReadLine());
                            break;
                        case '4':
                            Console.WriteLine("Write person id:");
                            personID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Write debt amount:");
                            debtAmount4 = Convert.ToDouble(Console.ReadLine());
                            break;
                    }
                    debtDAL.Update(select, new Debt(id4, personID, DateTime.Now, debtAmount4));

                    Console.WriteLine("Press key to contonue...");
                    Console.ReadKey();
                    break;
                case '5':
                    Console.WriteLine("Write debt id:");
                    int id5 = Convert.ToInt32(Console.ReadLine());
                    debtDAL.Delete(id5);
                    break;
                case 'q':
                    break;
                default:
                    log.Warn("Wrong selection.");
                    break;
            }
            
        }

        private Debt CreateDebt(int id, double debtAmont)
        {
            Debt debt = new Debt(id, DateTime.Now, debtAmont);
            return debt;
        }
    }
}
