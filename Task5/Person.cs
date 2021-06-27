using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Person
    {
        private string name { get; set; }
        private string surName { get; set; }
        private string phoneNumber { get; set; }

        public Person(string name, string surName, string phoneNumber)
        {
            this.name = name;
            this.surName = surName;
            this.phoneNumber = phoneNumber;
        }

        public string GetName()
        {
            return name;
        }
        public string GetSurName()
        {
            return surName;
        }
        public string GetPhoneNumber()
        {
            return phoneNumber;
        }
    }
}
