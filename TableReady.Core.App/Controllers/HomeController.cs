//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TableReady.Core.App.Models;
using TableReady.Core.BLL;

namespace TableReady.Core.App.Controllers
{
    //Controller to manage Home
    public class HomeController : Controller
    {
        //Home page of portal
        public IActionResult Index()
        {
            LogRestaurant();
            return View();
        }
        //Privacy page of portal
        public IActionResult Privacy()
        {
            LogRestaurant();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Verify with User is logged in as restaurant staff
        protected void LogRestaurant()
        {
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            if (claims.Count() != 0)
            {
                if(claims.Where(p => p.Type == "RestaurantID").Count() != 0)
                {
                    var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
                    var restLog = RestaurantsManager.GetRestaurantIdByNameByRestauranID(restId);
                    if (restLog == "")
                    {
                        TempData["RestaurantName"] = null;
                    }
                    else
                    {
                        TempData["RestaurantName"] = restLog;
                    }
                }
            }
        }
    }
}
