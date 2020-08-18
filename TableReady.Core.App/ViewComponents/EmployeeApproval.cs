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
    //View Component that shows employee's application for a restaurant
    public class EmployeeApproval : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var restaurantEmployees = RestaurantsManager.GetRestauranEmployeesByID(id);
            List<EmployeeModelView> viewRestaurantEmployees = new List<EmployeeModelView>();
            foreach (RestaurantEmployees remployee in restaurantEmployees)
            {
                if (remployee.NewRequestFlag == true)
                {
                    var employeeUser = UsersManager.GetUserByUserId(remployee.Employee.UserId);
                    string fullName = $"{employeeUser.FirstName} {employeeUser.LastName}";
                    var viewEmployee = new EmployeeModelView
                    {
                        RestaurantId = remployee.RestaurantId,
                        EmployeeId = remployee.EmployeeId,
                        UserId = employeeUser.UserId,
                        EmployeeFullName = fullName,
                        Active = remployee.Active,
                        Status = remployee.Status,
                        StartDate = (DateTime)remployee.StartDate,
                        EndDate = remployee.EndDate,
                        RequestFlag = remployee.NewRequestFlag,
                        RequestStatus = remployee.RequestStatus
                    };
                    viewRestaurantEmployees.Add(viewEmployee);
                }
            }
            return View(viewRestaurantEmployees);
        }
    }
}
