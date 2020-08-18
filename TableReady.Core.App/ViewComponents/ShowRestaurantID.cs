//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableReady.Core.BLL;

namespace TableReady.Core.App.ViewComponents
{
    //View Component that shows the user login's type
    public class ShowRestaurantID : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
                ViewBag.Restaurants = BasicGetRestaurants();
                return View();
        }
        protected IEnumerable BasicGetRestaurants()
        {
            //call the Asset Type manager and get the collection of Key value objects
            var types = RestaurantsManager.GetAsKeyValuePairs();
            //Create a collection of SelecteListItems
            var styles = new SelectList(types, "Value", "Text");
            return styles;
        }
    }
}
