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
    //View Component that shows reservations of a restaurant
    public class HostReservationManage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            bool checkin;
            var reservations = RestaurantsManager.GetTodayReservationsByRestaurantID(id);
            List<ResCustomerModelView> viewReservations = new List<ResCustomerModelView>();
            foreach (ReservationEntry resAux in reservations)
            {
                var customer = CustomersManager.GetCustomerByCustomerId(resAux.CustomerId);
                if (resAux.CheckinDateTime == null) checkin = false;
                else checkin = true;

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
                    ReservationDate = resAux.ReservationDateTime,
                    Checkin=checkin
                };
                viewReservations.Add(viewReservation);
            }
            return View(viewReservations);
        }
    }
}
