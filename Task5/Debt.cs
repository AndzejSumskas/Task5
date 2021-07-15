using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Debt
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }
        internal int PersonId { get; set; }
        internal double Amount { get; set; }

        private VariableEntries variableEntries = new VariableEntries();

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

        internal Debt CreateNeDebt()
        {
            Debt debt = new Debt(variableEntries.EnterPersonID(), DateTime.Now, variableEntries.EnterDebtAmount());

            return debt;
        }
    }
}
