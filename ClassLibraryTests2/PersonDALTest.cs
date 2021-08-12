using ClassLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTests
{
    [TestFixture]
    public class PersonDALTest
    {
        PersonDAL personDAL = new PersonDAL();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetPersons_CheckOrCorrectPerson()
        {

            Person person = personDAL.GetSearchByID(25);

            Assert.AreEqual(25, person.Id);
        }

        [Test]

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
