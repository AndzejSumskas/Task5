using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClassLibraryTests2
{
    [TestClass]
    public class PaymentDALTest
    {
        PaymentDAL paymentDAL = new PaymentDAL();


        [TestMethod]
        public void TestAddingDebt_CheckOrExist()
        {
            int personId = 49;
            DateTime date = DateTime.Now;
            double paymentAmount = 123.23;

            Payment payment = new Payment(personId,date, paymentAmount);

            int ID = paymentDAL.Add(payment);

            Payment paymentCheck = paymentDAL.GetSearchById(ID);

            Assert.AreEqual(ID, paymentCheck.Id);
            Assert.IsTrue(payment.PersonId == paymentCheck.PersonId);
            Assert.AreEqual(payment.PaymentAmount, paymentCheck.PaymentAmount);
            Assert.AreEqual(payment.Date.ToString(), paymentCheck.Date.ToString());
        }

        [TestMethod]
        public void TestDeletingDebt_CheckOrExist()
        {
            int ID = 53;

            Payment payment = paymentDAL.GetSearchById(ID);
            paymentDAL.Delete(ID);

            Payment paymentDel = paymentDAL.GetSearchById(ID);

            Assert.AreNotEqual(payment.Id, paymentDel.Id);
            Assert.AreNotEqual(payment.PersonId, paymentDel.PersonId);
            Assert.AreNotEqual(payment.Date.ToString(), paymentDel.Date.ToString());
            Assert.AreNotEqual(payment.PaymentAmount, paymentDel.PaymentAmount);
            Assert.AreEqual(paymentDel.Id, 0);
            Assert.AreEqual(paymentDel.Id, paymentDel.PersonId);
            Assert.AreEqual(paymentDel.Id, paymentDel.PaymentAmount);
        }


    }
}
