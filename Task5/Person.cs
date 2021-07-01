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
