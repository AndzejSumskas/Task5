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
        public void GetPerson_CheckOrNotEmpty()
        {

            var persons = personDAL.GetSearchByID(25);

            Person person = (from p in persons
                             where p.Id == 25
                             select p).FirstOrDefault();

            Assert.IsNotEmpty(persons);
        }

        [Test]
        public void GetPersons_CheckOrCorrectPerson()
        {

            var persons = personDAL.GetSearchByID(25);

            Person person = (from p in persons
                           where p.Id == 25
                           select p).FirstOrDefault();

            Assert.AreEqual(25, person.Id); 
        }

        [Test]

        public void AddPerson_CheckORExistsInDB()
        {
            Person newPerson = new Person("Lola", "Bora", "+37067035428");

            int personId = personDAL.Add(newPerson);

            Person person = personDAL.GetSearchByID(personId)[0];

            Assert.Equals(newPerson, person);
        }
    }
}
