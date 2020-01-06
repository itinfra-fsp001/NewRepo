using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkHubUI.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class URLController : Controller
    {

        private UserAreaBs objBs;
        //private CategoryBs objCatBs;
        //private UserBs objUserBs;

        public URLController()
        {

            objBs = new UserAreaBs();
            //objCatBs = new CategoryBs();
            //objUserBs = new UserBs();

        }


        // GET: User/URL
        public ActionResult Index()
        {
            //LinkHubDbEntities db = new LinkHubDbEntities();
            //ViewBag.CategoryId = new SelectList(db.tbl_Category, "CategoryId", "CategoryName");           

            ViewBag.CategoryId = new SelectList(objBs.categoryBs.GetALL().ToList(), "CategoryId", "CategoryName");

            ViewBag.UserId = new SelectList(objBs.userBs.GetALL().ToList(), "UserId", "UserEmail");
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Url myUrl)
        {
            try
            {
                myUrl.IsApproved = "P";
                myUrl.UserId = objBs.userBs.GetALL().Where(x => x.UserEmail == User.Identity.Name).FirstOrDefault().UserId;

                if (ModelState.IsValid)
                {
                    objBs.urlBs.Insert(myUrl);
                    TempData["Msg"] = "Created Sucessfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CategoryId = new SelectList(objBs.categoryBs.GetALL().ToList(), "CategoryId", "CategoryName");
                    ViewBag.UserId = new SelectList(objBs.userBs.GetALL().ToList(), "UserId", "UserEmail");
                    return View("Index");
                }

            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Create Failed :" + e1.Message;
                return RedirectToAction("Index");
            }
        }
    }
}