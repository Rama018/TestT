using Microsoft.AspNetCore.Mvc;
using TestT.Models;

namespace TestT.Controllers
{
    public class NormalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            List<CompanyTest> comp = CompanyTest.GetAll();

            return View(comp);
        }
        public IActionResult Get()
        {
            List<CompanyTest> comp = CompanyTest.GetAll();

            return Json(comp);
        }

        public IActionResult Create()
        {

            return View();
        }
        public IActionResult BtnCreate(CompanyTest Co)
        {
            if (ModelState.IsValid)
            {
                CompanyTest.Create(Co);
                return RedirectToAction(nameof(List));
            }
            else
                return View(nameof(Create));
        }
        public IActionResult Edit(string id)
        {
            CompanyTest com = CompanyTest.getID(Convert.ToInt32(id));
            return View(com);
        }

        public IActionResult BtnEdit(CompanyTest com)
        {
            CompanyTest.Update(com);
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(string id)
        {
            CompanyTest co = CompanyTest.getID(int.Parse(id));
            return View(co);
        }

        public IActionResult BtnDelete(CompanyTest com)
        {
            CompanyTest.Delete(com);
            return RedirectToAction(nameof(List));
        }


        public IActionResult btnSearch(string item)
        {
            List<CompanyTest> store = CompanyTest.GetAll();
            var Name = (from N in store
                        where N.Name.StartsWith(item)
                        select new { N.Name });

            return Json(Name);
        }
    }
}
