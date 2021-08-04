using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Payment
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }
        public double PaymentAmount { get; set; }

        public Payment(int id, int personId, DateTime date, double paymentAmount)
        {
            Id = id;
            PersonId = personId;
            Date = date;
            PaymentAmount = paymentAmount;
        }
    }
}
