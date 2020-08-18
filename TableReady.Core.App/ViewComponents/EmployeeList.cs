//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TableReady.Core.App.Models;
using TableReady.Core.BLL;

namespace TableReady.Core.App.ViewComponents
{
    //View Component that shows employees of a restaurant
    public class EmployeeList :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var restaurants = EmployeesManager.GetAllRestaurantsByEmpID(id);
            var viewRestaurants = restaurants.Select(r => new EmployeeModelView
            { 
                EmployeeId=r.EmployeeId,
                Active=r.Active,
                Status=r.Status,
                Restaurant=r.Restaurant.RestaurantName,
                RestaurantId=r.RestaurantId,
                UserId=r.Employee.UserId,
                StartDate=r.StartDate,
                EndDate=r.EndDate,
                RequestFlag=r.NewRequestFlag,
                RequestStatus=r.RequestStatus
            });
            return View(viewRestaurants);
        }
    }
}
