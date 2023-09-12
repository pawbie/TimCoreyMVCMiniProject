using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonRegistryLibrary.Interfaces;
using PersonRegistryMVC.Models;
using PersonRegistryLibrary.Services;
using PersonRegistryLibrary.Models;
using System.Linq;

namespace PersonRegistryMVC.Controllers
{
    public class PersonRegistryController : Controller
    {
        private readonly IDataAccess<PersonModel> _personSessionStorage;
        private readonly IDataAccess<AddressModel> _addressSessionStorage;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public PersonRegistryController(IDataAccess<PersonModel> personSessionStorage, IDataAccess<AddressModel> addressSessionStorage, IHttpContextAccessor httpContextAccessor)
        {
            _personSessionStorage = personSessionStorage;
            _addressSessionStorage = addressSessionStorage;
            _httpContextAccessor = httpContextAccessor;

            _session = _httpContextAccessor.HttpContext.Session;
        }

        // GET: PersonRegistryController
        public ActionResult Index() 
        {   
            var personList = _personSessionStorage.GetAll();
            return View(personList.ToList());
        }

        // GET: PersonRegistryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonRegistryController/Create
        public ActionResult CreatePerson()
        {
            return View();
        }

        public ActionResult CreateAddress()
        {
            return View(new AddressUIModel { PersonId = (int)_session.GetInt32("personId") });
        }

        // POST: PersonRegistryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddress(IFormCollection collection)
        {
            try
            {
                var address = new AddressModel{ City = collection["City"], Street = collection["Street"], PostalCode = collection["PostalCode"], PersonId = int.Parse(collection["PersonId"]) };
                _addressSessionStorage.Add(address);

                return RedirectToAction(nameof(EditPerson), new {id = address.PersonId});
            }
            catch
            {
                return View();
            }
        }

        // POST: PersonRegistryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePerson(IFormCollection collection)
        {
            try
            {
                var person = new PersonModel { FirstName = collection["FirstName"], LastName = collection["LastName"], Age = int.Parse(collection["Age"]) };
                _personSessionStorage.Add(person);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonRegistryController/Edit/5
        public ActionResult EditPerson(int id)
        {
            _session.SetInt32("personId", id);

            var personDetails = _personSessionStorage.GetAll().Where(x => x.Id == id).First();
            var addresses = _addressSessionStorage.GetAll().Where(x => x.PersonId == id);

            return View(
                new PersonUIModel { 
                    FirstName = personDetails.FirstName, 
                    LastName = personDetails.LastName, Age = personDetails.Age, 
                    Addresses = addresses.ToList()
                }
            );
        }

        // POST: PersonRegistryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerson(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
