using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Payment
    {
        internal int Id { get; set; }
        internal int PersonId { get; set; }
        internal DateTime Date { get; set; }
        internal double PaymentAmount { get; set; }
    }
}
