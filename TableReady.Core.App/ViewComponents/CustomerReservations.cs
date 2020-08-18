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
    //View Component that shows reservation of a specific Customer
    public class CustomerReservations : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var reservations = RestaurantsManager.GetReservationsByCustomerID(id);
            List<ResCustomerModelView> viewReservations = new List<ResCustomerModelView>();
            foreach (ReservationEntry resAux in reservations)
            {
                var viewReservation = new ResCustomerModelView
                {
                    RestaurantID = (int)resAux.RestaurantId,
                    CustomerId = (int)resAux.CustomerId,
                    Restaurant = resAux.Restaurant.RestaurantName,
                    ReservationEntryId = resAux.ReservationId,
                    PartySize = resAux.PartySize,
                    ReservationStatus = resAux.ReservationStatus,
                    EntryOrigin = resAux.EntryOrigin,
                    ReservationDate = resAux.ReservationDateTime,
                    Message = resAux.CustomerMessage
                };
                viewReservations.Add(viewReservation);
            }

            return View(viewReservations);
        }
    }
}
