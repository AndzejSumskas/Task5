using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class AppController
    {
        private char select = ' ';

        Transaction transaction = new Transaction();

        public void StartApplication(string connectionString)
        {
            while (select != 'q')
            {
                Console.Clear();
                Console.WriteLine("Select option:");
                Console.WriteLine("a - Write all persons from db to console");
                Console.WriteLine("b - Write all personts to Json file");
                Console.WriteLine("c - Add new person to db");
                Console.WriteLine("d - Search person in db");
                Console.WriteLine("e - Update person in db");
                Console.WriteLine("f - Delete person from db");
                Console.WriteLine("g - Write all debs from db to console ");
                Console.WriteLine("h - Write all debts to Json file");
                Console.WriteLine("i - Add new debt to DB");
                Console.WriteLine("j - Search debt in DB");
                Console.WriteLine("l - Update debt in DB");
                Console.WriteLine("m - Delete debt from DB");
                Console.WriteLine("n - Write Name, Surname, debtAmountSum to console");

                

                Console.WriteLine("q - Exit");


                select = Console.ReadKey().KeyChar;
                Console.WriteLine();

                transaction.ExecuteSqlTransaction(connectionString, select);

            }
        }

       

    }
}
