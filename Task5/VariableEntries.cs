using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class VariableEntries
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AppController));

        internal string WriteNewName()
        {
            Console.WriteLine("Enter the name:");
            string Name = Console.ReadLine();
            log.Info("New name was writed.");
            return Name;
        }

        internal string WriteNewSurName()
        {
            Console.WriteLine("Enter the surname:");
            string surName = Console.ReadLine();
            log.Info("New surname was writed.");
            return surName;
        }

        internal string WriteNewPhoneNumber()
        {
            Console.Write("Enter new phone number\n +370");
            long telephonNumber = Convert.ToInt32(Console.ReadLine());
            while (telephonNumber.ToString().Length != 8)
            {
                Console.WriteLine("Incorrect entry.");
                Console.Write("Enter phone number\n +370");
                telephonNumber = Convert.ToInt32(Console.ReadLine());
            }
            log.Info("New phone number was writed.");
            return "+370" + telephonNumber;
        }

        internal double EnterDebtAmount()
        {
            Console.Clear();
            Console.WriteLine("Enter debt amount");
            double debtAmount = Convert.ToDouble(Console.ReadLine());
            log.Info("New debt amount was writed.");
            return debtAmount;
        }

        internal int EnterPersonID()
        {
            Console.Clear();
            Console.WriteLine("Enter person id");
            int personId = Convert.ToInt32(Console.ReadLine());
            log.Info("New payment amount was writed.");
            return personId;
        }
    }
}
