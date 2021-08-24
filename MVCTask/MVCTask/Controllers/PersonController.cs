using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTask.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            PersonDAL personDAL = new PersonDAL();

            List<Person> persons = personDAL.GetList();
            return View(persons);
        }

        public ActionResult GetPersons()
        {
            PersonDAL personDAL = new PersonDAL();

            List<Person> persons = personDAL.GetList();

            return View(persons);
        }

        public ActionResult GetPerson(int id)
        {
            PersonDAL personDAL = new PersonDAL();

            List<Person> persons = personDAL.GetSearchListByID(id);

            return View(persons);
        }

        public ActionResult Delete(int id)
        {
            PersonDAL personDAL = new PersonDAL();

            personDAL.Delete(id);
            return RedirectToAction(nameof(GetPersons));
        }


        public ActionResult Edit(int id)
        {
            PersonDAL personDAL = new PersonDAL();
            Person person = personDAL.GetSearchByID(id);

            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            PersonDAL personDAL = new PersonDAL();
            personDAL.Update(person.Id, '4', person.Name, person.SurName, person.PhoneNumber);

            return RedirectToAction("GetPersons");
        }

        public ActionResult Create()
        {
            PersonDAL personDAL = new PersonDAL();
            Person person = new Person();

            return View(person);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            PersonDAL personDAL = new PersonDAL();
            personDAL.Add(person);

            return RedirectToAction("GetPersons");
        }

    }
}