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
        public void TestMethod1()
        {
            Debt debt = new Debt(3, 3, DateTime.Now, 213);

            debtDAL.Update('1', debt);

            Debt debt2 = debtDAL.GetSearchById(3);

            //Assert.AreEqual(debt.Id, debt2.Id);
            Assert.AreEqual(debt.PersonId, debt2.PersonId);
            //Assert.AreEqual(debt.Date, debt2.Date);
            //Assert.AreEqual(debt.Amount, debt2.Amount);

        }
    }
}
        

