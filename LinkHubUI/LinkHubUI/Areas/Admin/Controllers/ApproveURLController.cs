using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkHubUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ApproveURLController : BaseAdminController
    {
        // GET: Admin/ApproveURL
        public ActionResult Index(string Status)
        {
            ViewBag.Status = (Status == null ? "P" : Status);
            if (Status == null)
            {
                var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == "P").ToList();
                return View(urls);
            }
            else
            {
                var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == Status).ToList();
                return View(urls);
            }


        }


        public ActionResult Approve(int id)
        {
            try
            {
                var myurl = objBs.urlBs.GetbyID(id);
                myurl.IsApproved = "A";
                objBs.urlBs.Update(myurl);

                TempData["Msg"] = "Approved Sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Approval Failed :" + e1.Message;
                return RedirectToAction("Index");
            }


        }

        public ActionResult Reject(int id)
        {
            try
            {
                var myurl = objBs.urlBs.GetbyID(id);
                myurl.IsApproved = "R";
                objBs.urlBs.Update(myurl);

                TempData["Msg"] = "Rejected Sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Reject Failed :" + e1.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]

        public ActionResult ApproveOrRejectALL(List<int> Ids, String Status, string CurrentStatus)
        {
            try
            {
                objBs.ApproveOrReject(Ids, Status);
                TempData["Msg"] = "Operation Successfully";
                var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == CurrentStatus).ToList();
                return PartialView("pv_ApproveURL", urls);
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Operation Failed" + e1.Message;
                var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == CurrentStatus).ToList();
                return PartialView("pv_ApproveURL", urls);
            }

        }

        //public     void ApproveOrRejectALL(List<int> Ids, String Status)
        //{

        //    try
        //    {
        //        objBs.ApproveOrReject(Ids, Status);
        //        TempData["Msg"] = "Operation Successfully";

        //    }
        //    catch (Exception e1)
        //    {
        //        TempData["Msg"] = "Operation failed" + e1.Message;
        //    }

        //}


    }
}