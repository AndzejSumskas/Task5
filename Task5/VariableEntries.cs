using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class VariableEntries
    {
        internal string WriteNewName()
        {
            Console.WriteLine("Enter the name:");
            string Name = Console.ReadLine();
            return Name;
        }

        internal string WriteNewSurName()
        {
            Console.WriteLine("Enter the surname:");
            string surName = Console.ReadLine();
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
            return "+370" + telephonNumber;
        }

        internal int EnterIDOfPerson()
        {
            Console.Clear();
            Console.WriteLine("Enter person id");
            int personId = Convert.ToInt32(Console.ReadLine());
            return personId;
        }


    }
}
