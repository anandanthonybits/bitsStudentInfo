using ExcelExampleData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsUserDirectory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string userName = form["userName"];
            StudentInfo studentInfo = SearchUseEPPlus(userName);
            if (string.IsNullOrEmpty(studentInfo.name))
            {
                return View("notFound");
            }
            else
            {
                return View("UserDetails", studentInfo);
            }
        }

        [HttpPost]
        public JsonResult SearchUseNPOI(string searchName)
        {
            var data = new NPOIProvider(Server.MapPath("~/App_Data/StudentDB.xlsx")).SearchRow(searchName);
            StudentInfo studentInfo = (from d in data
                   select d).FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public StudentInfo SearchUseEPPlus(string searchName)
        {
            StudentInfo studentInfo = new StudentInfo();
            var data = new EPPlusProvier(Server.MapPath("~/App_Data/StudentDB.xlsx")).SearchRow(searchName);
            if (data != null)
            {
                studentInfo = (from d in data
                                           select d).FirstOrDefault();
            }            
            return studentInfo;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}