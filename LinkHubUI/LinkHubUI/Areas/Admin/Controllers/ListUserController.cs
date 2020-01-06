using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkHubUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ListUserController : BaseAdminController
    {

        

   ////public class ListUserController : Controller
   ////     {

   ////         private AdminBs objBs;

   ////         public ListUserController()
   ////         {

   ////             objBs = new AdminBs();
   ////         }


            // GET: Admin/ListUser
            public ActionResult Index(string SortOrder, string Sortby, string Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.Sortby = Sortby;
            var users = objBs.userBs.GetALL();

            switch (Sortby)
            {
                case "UserEmail":
                    switch (SortOrder)
                    {
                        case "Asc":                      
                            users = users.OrderBy(x => x.UserEmail).ToList();
                            break;
                        case "Desc":
                            users = users.OrderByDescending(x => x.UserEmail).ToList();
                            break;
                        default:
                            break;
                    }
                    break;

                case "Role":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                users = users.OrderBy(x => x.Role).ToList();
                                break;
                            case "Desc":
                                users = users.OrderByDescending(x => x.Role).ToList();
                                break;
                            default:
                                break;
                        }

                    }
                    break;          
               
                default:
                    users = users.OrderBy(x => x.UserEmail).ToList();
                    break;
            }

            ViewBag.Totalpages = Math.Ceiling(objBs.userBs.GetALL().Count() / 10.0);

            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;

            users = users.Skip((page - 1) * 10).Take(10);

            return View(users);
        }
    }
}