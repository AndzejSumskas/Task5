using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Person
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string SurName { get; set; }
        internal string PhoneNumber { get; set; }

        VariableEntries textEntries = new VariableEntries();

        internal List<string> persons = new List<string>();

        public Person()
        {
        }

        public Person(int id, string name, string surName, string phoneNumber)
        {
            Id = id;
            Name = name;
            SurName = surName;
            PhoneNumber = phoneNumber;
        }

        public Person CreateNewPerson()
        {
            Console.Clear();
            Person person = new Person();
            person.Name = textEntries.WriteNewName();
            person.SurName = textEntries.WriteNewSurName();
            person.PhoneNumber = textEntries.WriteNewPhoneNumber();

            return person;
        }
    }
}
