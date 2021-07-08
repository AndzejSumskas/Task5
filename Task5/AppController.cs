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
                Console.WriteLine("a - Print all persons ");
                Console.WriteLine("b - Write all personts to Json file");
                Console.WriteLine("c - Add new person to DB");
                Console.WriteLine("d - Search person in DB");
                Console.WriteLine("e - Update person in DB");
                Console.WriteLine("f - Delete person from DB");
                Console.WriteLine("g - Print all debts ");
                Console.WriteLine("h - Write all debts to Json file");
                Console.WriteLine("i - Add new debt to DB");
                Console.WriteLine("j - Search debts in DB");
                Console.WriteLine("l - Update debt in DB");
                Console.WriteLine("m - Delete debt from DB");

                Console.WriteLine("q - Exit");


                select = Console.ReadKey().KeyChar;
                Console.WriteLine();

                transaction.ExecuteSqlTransaction(connectionString, select);

            }
        }

       

    }
}
