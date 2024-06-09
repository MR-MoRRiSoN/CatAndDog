using GiviQiria.final.Models;
using GiviQiria.final.Models.DTO;
using GiviQiria.final.Service;
using Microsoft.AspNetCore.Mvc;

namespace GiviQiria.final.Controllers
{
    public class MainPageController : Controller
    {
        private readonly IMainPageService? mainPageService;

        public MainPageController(IMainPageService? mainPageService)
        {
            this.mainPageService = mainPageService;
        }

        public IActionResult Index()
        {
            var catList = mainPageService!.GetCatList();
            return View(catList);
        }


        [Route("MainPage/CatToyList/{catID}")]
        public IActionResult CatToyList(Guid catID)
        {
            ViewData["CatID"] = catID; 
            var toyList = mainPageService!.GetCatToyList(catID);

            return View(toyList);
        }

        [Route("MainPage/AddNewToy/{catID}")]
        public IActionResult AddNewToy(Guid catID)
        {
            ViewData["CatID"] = catID;
            return View();
        }  
        [Route("MainPage/UpdateCat/{catID}")]
        public IActionResult UpdateCat(Guid catID)
        {
           var updateCat = mainPageService!.GetCatById(catID);
            return View(updateCat);
        }  

        [Route("MainPage/UpdateCatToy/{toyID}")]
        public IActionResult UpdateCatToy(Guid toyID)
        {
           var updateToy = mainPageService!.GetToyById(toyID);
            return View(updateToy);
        }

        [HttpPost]
        public IActionResult AddNewToy(AddNewCatToy addNewCatToy)
        {
            mainPageService!.SaveCatToy(addNewCatToy);
            ViewData["CatID"] = addNewCatToy.CatId;
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteCatToy(List<Guid> ToyId)
        {
            mainPageService!.DeleteCatToy(ToyId);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCat(List<Guid> CatId)
        {
            mainPageService!.DeleteCat(CatId);
            return Ok();
        }

        public IActionResult AddNewCat()
        {
            ViewBag.Gender = Enum.GetValues(typeof(Gender));
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCat(AddNewCatRequest newCat)
        {
            mainPageService!.SaveCat(newCat);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCatToy(UpdateToyRequest updateCatToy) {

         

        mainPageService!.UpdateToy(updateCatToy);
        return View(updateCatToy);
        }

        [HttpPost]
        public IActionResult UpdateCat(UpdateCatRequest updateCat)
        {
            mainPageService!.UpdateCat(updateCat);
            return View(updateCat);
        }




    }
}
