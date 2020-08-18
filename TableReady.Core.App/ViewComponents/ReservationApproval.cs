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
    //View Component that shows restaurant's reservations to be approved
    public class ReservationApproval : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var reservations = RestaurantsManager.GetManageReservations(id);

            List<ResCustomerModelView> viewReservations = new List<ResCustomerModelView>();
            foreach (ReservationEntry resAux in reservations)
            {
                if (resAux.ReservationStatus == "on Hold")
                {
                    var customer = CustomersManager.GetCustomerByCustomerId(resAux.CustomerId);
                    var viewReservation = new ResCustomerModelView
                    {
                        RestaurantID = (int)resAux.RestaurantId,
                        CustomerId = (int)resAux.CustomerId,
                        CustomerName = $"{customer.User.FirstName} {customer.User.LastName}",
                        Restaurant = resAux.Restaurant.RestaurantName,
                        ReservationEntryId = resAux.ReservationId,
                        PartySize = resAux.PartySize,
                        ReservationStatus = resAux.ReservationStatus,
                        EntryOrigin = resAux.EntryOrigin,
                        ReservationDate = resAux.ReservationDateTime
                    };
                    viewReservations.Add(viewReservation);
                }
            }
            return View(viewReservations);
        }
    }
}
