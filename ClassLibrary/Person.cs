using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }

        public double DebtSumAmount { get; set; }
        public double PaymentSumAmount { get; set; }
        public double Balance { get; set; }

        public List<string> persons = new List<string>();

        public Person()
        {
        }

        public Person(string name, string surName, string phoneNumber)
        {
            Name = name;
            SurName = surName;
            PhoneNumber = phoneNumber;
        }

        public Person(int id, string name, string surName, string phoneNumber)
        {
            Id = id;
            Name = name;
            SurName = surName;
            PhoneNumber = phoneNumber;
        }

        public Person(int id, string name, string surName, double debtSumAmount, double paymentSumAmount)
        {
            Id = id;
            Name = name;
            SurName = surName;
            DebtSumAmount = debtSumAmount;
            PaymentSumAmount = paymentSumAmount;
        }
    }
}
