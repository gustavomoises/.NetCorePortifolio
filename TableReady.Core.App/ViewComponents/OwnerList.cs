//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableReady.Core.App.Models;
using TableReady.Core.BLL;

namespace TableReady.Core.App.ViewComponents
{
    //View Component that shows restaurant's owners.
    public class OwnerList: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var restaurants = OwnersManager.GetAllRestaurantsByOwnerID(id);
            var viewRestaurants = restaurants.Select(r => new OwnerModelView
            {
                OwnerId = r.OwnerId,
                Active = r.Active,
                Status = r.Status,
                Restaurant = r.Restaurant.RestaurantName,
                RestaurantId = r.RestaurantId,
                UserId = r.Owner.UserId,
                StartDate =(DateTime)r.StartDate,
                EndDate = r.EndDate,
                RequestFlag = r.Request,
                RequestStatus = r.RequestStatus
            });


            return View(viewRestaurants);
        }
    }
}
