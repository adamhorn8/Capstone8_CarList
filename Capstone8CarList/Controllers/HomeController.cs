using Capstone8CarList.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone8CarList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult GetAPI()
        {

            HttpWebRequest WR = WebRequest.CreateHttp($"http://localhost:51186/api/Values/GetCarList");
            WR.UserAgent = ".NET Framework Test Client";


            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CardInfo = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(CardInfo);
                ViewBag.CarID = JsonData["CarID"];
                ViewBag.Make = JsonData["Make"];
                ViewBag.Model = JsonData["Model"];
                ViewBag.Year = JsonData["Year"];

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }


            return View();
        }

        public ActionResult GetCarsByMake(string make)
        {
            CarsEntities db = new CarsEntities();

            List<CarList> make1 = (from m in db.CarLists
                                   where m.Make.Contains(make)
                                   select m).ToList();

            ViewBag.Makelist = make1;
            return View();
        }

        public ActionResult GetCarsByModel(string model)
        {
            CarsEntities db = new CarsEntities();

            List<CarList> make1 = (from m in db.CarLists
                                   where m.Make.Contains(model)
                                   select m).ToList();

            ViewBag.Makelist = make1;
            return View("GetCarsByMake");
        }

        public ActionResult GetCarsByYear(int year)
        {
            CarsEntities db = new CarsEntities();

            List<CarList> make1 = (from m in db.CarLists
                                   where m.Year == (year)
                                   select m).ToList();

            ViewBag.Makelist = make1;
            return View("GetCarsByMake");
        }

        public ActionResult GetCarsByColor(string color)
        {
            CarsEntities db = new CarsEntities();

            List<CarList> make1 = (from m in db.CarLists
                                   where m.Make.Contains(color)
                                   select m).ToList();

            ViewBag.Makelist = make1;
            return View("GetCarsByMake");
        }



    }
}