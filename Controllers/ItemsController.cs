using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Web.Data;
using Project_Web.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;



namespace Project_Web.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {

        public ItemsController(AppDbContext db , IHostingEnvironment host)
        {
           _db = db;
            _host = host;
        }


        private readonly IHostingEnvironment _host;
        private readonly AppDbContext _db;





        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.items.Include(c => c.Category).ToList();
            return View(itemsList);
        }


        //Get 
        public IActionResult New()
        {
            createSelectList();
            return View();
        }


        public void createSelectList(int selectId=1)
        {
            //الطريقة اذا بدي احط الداتا في الكنترلور وانقلها لكل الميثود الموجوده داخل الكنترولر 

            //List<Category> categories = new List<Category>
            //{
            //    new Category() {Id = 0 , Name = "Select Category"},
            //    new Category() {Id = 1, Name = "Computers"},
            //    new Category() {Id = 2 , Name = "Mobiles"},
            //    new Category() {Id = 3, Name = "Electrice Machines"},

            //};





            //الطريقة الثانية هي انا تاتي في الداتا من sql server 



            
            List<Category > categories = _db.Categories.ToList();

            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "100") 
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }

            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if(item.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "images");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    item.clientFile.CopyTo(new FileStream(fullPath,FileMode.Create));
                    item.imagePath = fileName;
                }
                _db.items.Add(item);
                _db.SaveChanges();

                TempData["successData"] = "Item has been added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }



        //Get 
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) 
            {
                return NotFound();
            }
            var item = _db.items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }

            createSelectList(item.CategoryId);

            return View(item);
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }


          
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (item.clientFile != null) 
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "images");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    item.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    item.imagePath = fileName;

                }

                _db.items.Update(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been Edit Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }


            createSelectList(item.CategoryId);

            return View(item);
        }


        //Post
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
       
        public IActionResult DeleteItem(int ? Id)
        {




            var item = _db.items.Find(Id);
            if (item == null) 
            {
                return NotFound();
            }
            _db.Remove(item);
            _db.SaveChanges();
            TempData["successData"] = "Item has been Delete Successfully";
            return RedirectToAction("Index");
           
        }


    }
}
