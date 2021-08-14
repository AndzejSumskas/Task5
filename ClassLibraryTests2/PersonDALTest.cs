using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClassLibraryTests
{
    [TestClass]
    public class PersonDALTest
    {
        PersonDAL personDAL = new PersonDAL();

        [TestMethod]
        public void GetPersons_CheckOrCorrectPerson()
        {

            Person person = personDAL.GetSearchByID(25);

            Assert.AreEqual(25, person.Id);
        }

        [TestMethod]

        public void TestAddingPerson_CheckOrExist()
        {
            Person newPerson = new Person("Lola", "Bora", "+37067035428");

            int personId = personDAL.Add(newPerson);

            Person person = personDAL.GetSearchByID(personId);

            Assert.AreEqual(newPerson.Name, person.Name);
            Assert.AreEqual(newPerson.SurName, person.SurName);
            Assert.AreEqual(newPerson.PhoneNumber, person.PhoneNumber);
        }
    }
}
