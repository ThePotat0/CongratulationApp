using CongratulationApp.Domains;
using CongratulationApp.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CongratulationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            _dataManager = dataManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> ContactsEdit(int id)
        {
            ContactEntity? entity = id == default
                ? new ContactEntity()
                : await _dataManager.Contacts.GetContactByIdAsync(id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> ContactsEdit(ContactEntity entity, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View(entity);
            if (image != null) 
            {
                entity.Photo = image.FileName;
                await SaveImage(image);
            }
            await _dataManager.Contacts.SaveContactAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ContactsDelete(int id)
        {
            await _dataManager.Contacts.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<string> SaveImage(IFormFile img) 
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "img/", img.FileName);
            await using FileStream stream = new FileStream(path, FileMode.Create);
            await img.CopyToAsync(stream);
            return path;
        }

        /// <summary>
        /// ^^^Распихать по файлам при пейвой возможности^^^
        /// </summary>

       
        public async Task<IActionResult> Index()
        {
            ViewBag.Contacts = await _dataManager.Contacts.GetContactEntitiesAsync();

            return View();
        }
    }
}
