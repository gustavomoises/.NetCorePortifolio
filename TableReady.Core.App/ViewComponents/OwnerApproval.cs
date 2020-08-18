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
using TableReady.Core.Data.Domain;

namespace TableReady.Core.App.ViewComponents
{
    //View Component that shows owners of a restaurant
    public class OwnerApproval : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var restaurantOwners = RestaurantsManager.GetRestauranOwnersByID(id);
            List<OwnerModelView> viewRestaurantOwners = new List<OwnerModelView>();
            foreach (RestaurantOwners rowner in restaurantOwners)
            {
                if (rowner.Request==true)
                { 
                    var ownerUser = UsersManager.GetUserByUserId(rowner.Owner.UserId);
                    string fullName = $"{ownerUser.FirstName} {ownerUser.LastName}";
                    var viewOwner = new OwnerModelView
                    {
                        RestaurantId = rowner.RestaurantId,
                        OwnerId = rowner.OwnerId,
                        UserId = ownerUser.UserId,
                        OwnerFullName = fullName,
                        Active = rowner.Active,
                        Status = rowner.Status,
                        StartDate = (DateTime)rowner.StartDate,
                        EndDate = rowner.EndDate,
                        RequestFlag = rowner.Request,
                        RequestStatus = rowner.RequestStatus
                    };
                    viewRestaurantOwners.Add(viewOwner);
                }
            }
            return View(viewRestaurantOwners);
        }
    }
}
