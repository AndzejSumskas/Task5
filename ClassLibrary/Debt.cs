using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Debt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
        public double Amount { get; set; }

        public Debt()
        {
        }

        public Debt(int personId, DateTime date, double deptAmount)
        {
            PersonId = personId;
            Date = date;
            Amount = deptAmount;
        }

        public Debt(int id, int personId, DateTime date, double deptAmount)
        {
            Id = id;
            PersonId = personId;
            Date = date;
            Amount = deptAmount;
        }

        public void Render(Debt debt)
        {
            Console.WriteLine($"Id: {debt.Id}, Date: {debt.Date}, Person ID: {debt.PersonId}, Amount: {debt.Amount}.");
        }


    }
}
