using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Web.Models;
using Project_Web.Repository.Base;

namespace Project_Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
      //  public CategoryController(IRepository<Category> repository)
       // {
            //_repository = repository; 
        //}

        //private IRepository<Category> _repository;


        public CategoryController(IUnitOfWork _myUnit ) 
        {
            myUnit = _myUnit;
            
        }


       
        private readonly IUnitOfWork myUnit;



        //public IActionResult Index()
        //{

        //    return View(_repository.FindAll());
        //}

        public async Task<IActionResult> Index()
        {
            var OneCat = myUnit.categories.SelectOne(x => x.Name == "Mobiles");

            var allCat = await myUnit.categories.FindAllAsync("Items");
            
          //  return View(await _repository.FindAllAsync("Items"));

            return View(allCat);
        }

        //Get 
        public IActionResult New() 
        {
            return View();

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {
            if (ModelState.IsValid) 
            {
                
                if (category.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    category.clientFile.CopyTo(stream);
                    category.dbImage = stream.ToArray();
                    
                 
                    
                }


                myUnit.categories.AddOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
           
        }


        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                
                return NotFound();
            }
            var category = myUnit.categories.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                myUnit.categories.UpdateOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }

        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {

                return NotFound();
            }
            var category = myUnit.categories.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            myUnit.categories.DeleteOne(category);

            TempData["successData"] = "Item has been Deleted Successfully";
            return RedirectToAction("Index");
        }



    }
}
