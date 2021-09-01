using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace MVCTask.Controllers
{

    public class PersonController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PersonController));


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
            log.Info("Getting list of persons.");
            return View(persons);
        }

        public ActionResult GetPerson(int id)
        {
            PersonDAL personDAL = new PersonDAL();

            List<Person> persons = personDAL.GetSearchListByID(id);
            log.Info("Getting person.");
            return View(persons);
        }

        public ActionResult Delete(int id)
        {
            PersonDAL personDAL = new PersonDAL();
            Person person = personDAL.GetSearchByID(id);
            log.Info("Searching person to delete.");
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(int id, string name)
        {
            PersonDAL personDAL = new PersonDAL();
            personDAL.Delete(id);
            log.Info("Deleting person.");
            return RedirectToAction("GetPersons");
        }



        public ActionResult Edit(int id)
        {
            PersonDAL personDAL = new PersonDAL();
            Person person = personDAL.GetSearchByID(id);
            log.Info("Searching person to edit.");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            PersonDAL personDAL = new PersonDAL();
            personDAL.Update(person.Id, '4', person.Name, person.SurName, person.PhoneNumber);
            log.Info("Editing person.");
            return RedirectToAction("GetPersons");
        }

        public ActionResult Create()
        {
            PersonDAL personDAL = new PersonDAL();
            Person person = new Person();
            log.Info("Createing person.");
            return View(person);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if(person.Name == null || person.SurName == null || person.PhoneNumber == null)
            {
                string message = "Some fields are empty!!!";
                MessageBox.Show(message);
                RedirectToAction("GetPersons");
                log.Warn("Some fields are empty!");
                return RedirectToAction("Create");
            }
            else
            {
                PersonDAL personDAL = new PersonDAL();
                personDAL.Add(person);
                log.Info("Person created.");
                return RedirectToAction("GetPersons");
            }  
        }

        public ActionResult DebtView(int id)
        {
            DebtDAL debtDAL = new DebtDAL();
            List<Debt> debts = debtDAL.GetSearchList(id);
            log.Info("Getting list of person debts.");
            return View(debts);
        }


    }
}