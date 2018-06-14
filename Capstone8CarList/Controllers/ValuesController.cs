using Capstone8CarList.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Capstone8CarList.Controllers
{
    public class ValuesController : HomeController
    {
        static int hits = 0;
        

        public int Counter()
        {
            hits++;

            return hits;
        }

        public List<CarList> GetCarList()
        {
            CarsEntities db = new CarsEntities();

            List<CarList> cars = db.CarLists.ToList();

            return cars;
        }
    }
}