using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClassLibraryTests2
{
    [TestClass]
    public class DebtDALTest
    {
        DebtDAL debtDAL = new DebtDAL();

        [TestMethod]
        public void TestUpgradeDebtPersonId_CheckOrChange()
        {
            int debtID = 57;

            int newPersonId = 4;

            Debt debt = new Debt(debtID, newPersonId, DateTime.Now, 213);

            debtDAL.Update('1', debt);

            Debt debt2 = debtDAL.GetSearchById(debtID);

            Assert.AreEqual(debt.PersonId, debt2.PersonId);
        }

        [TestMethod]
        public void TestUpgradeDebtAllData_CheckOrChange()
        {
            int debtID = 83;

            int newPersonId = 46;

            Debt debt = new Debt(debtID, newPersonId, DateTime.Now, 213);

            debtDAL.Update('4', debt);

            Debt debt2 = debtDAL.GetSearchById(debtID);

            Assert.AreEqual(debt.PersonId, debt2.PersonId);
            Assert.AreEqual(debt.Date.ToString(), debt2.Date.ToString());
            Assert.AreEqual(debt.Amount, debt2.Amount);
        }
    }
}
        

