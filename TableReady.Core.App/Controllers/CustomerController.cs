//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Hosting;
using TableReady.Core.App.Models;
using TableReady.Core.BLL;
using TableReady.Core.Data.Domain;

namespace TableReady.Core.App.Controllers
{
    //Controller to manage Customer
    [Authorize]
    public class CustomerController : Controller
    {
        //Customer's Waitlist 
        public IActionResult WaitlistShow()
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var custId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "CustomerID").Value);
            var waitlist = CustomersManager.GetActiveWaitlist(custId);
            var viewWaitlist = waitlist.Select(r => new WaitCustomerModelView
            {
                RestaurantID = (int)r.RestaurantId,
                CustomerId = (int)r.CustomerId,
                Restaurant = r.Restaurant.RestaurantName,
                WaitlistEntryId = r.WaitlistEntryId,
                PartySizew = r.PartySize,
                WaitlistStatus = r.WaitlistStatus,
                EntryOriginw = r.EntryOrigin,
                WaitlistPosition = "-"
            }).ToList();
            foreach (WaitCustomerModelView viewaux in viewWaitlist)
            {
                if (viewaux.WaitlistStatus == "active")
                {
                    int waitPos = RestaurantsManager.GetWaitlistPosition(viewaux.RestaurantID, viewaux.WaitlistEntryId);
                    viewaux.WaitlistPosition = Convert.ToString(waitPos);
                }
            }
            return View(viewWaitlist);
        }
        //Customer's Reservations 
        public IActionResult ReservationsShow()
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var custId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "CustomerID").Value);
            var reservations = CustomersManager.GetReservations(custId);
            var viewReservations = reservations.Select(r => new ResCustomerModelView
            {
                RestaurantID = (int)r.RestaurantId,
                CustomerId = (int)r.CustomerId,
                Restaurant = r.Restaurant.RestaurantName,
                ReservationEntryId = r.ReservationId,
                PartySize = r.PartySize,
                ReservationStatus = r.ReservationStatus,
                EntryOrigin = r.EntryOrigin,
                ReservationDate = r.ReservationDateTime,
                Message=r.CustomerMessage
            }).ToList();
            return View(viewReservations);
        }
        //Customer's Waitlist and Reservations
        public IActionResult RecordsShow()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var custId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "CustomerID").Value);
            var waitlist = CustomersManager.GetWaitlistByCustomerID(custId);
            var viewWaitlist = waitlist.Select(r => new RecordModelView
            {
                CustomerId = (int)r.CustomerId,
                Restaurant = r.Restaurant.RestaurantName,
                Id = r.WaitlistEntryId,
                Type = "Waitlist",
                EntryOrigin = r.EntryOrigin,
                Status = r.WaitlistStatus,
                PartySize = r.PartySize,
                RecordDate = r.CheckinDate,
                Message=""
            }).ToList();
            var viewRecords = viewWaitlist.OrderByDescending(p => p.RecordDate).ThenBy(p => p.Type).ThenBy(p => p.Status).ThenBy(p => p.EntryOrigin).ThenByDescending(p => p.PartySize);
            ViewBag.CustomerId = custId;
            return View(viewRecords);
        }
        //Customer's Waitlist entry details from Waitlist
        public IActionResult WaitlistDetails(int id)
        {
            LogRestaurant();
            var waitlist = RestaurantsManager.GetWaitlistById(id);
            var viewWait = new WaitCustomerModelView
            {
                CustomerId=(int)waitlist.CustomerId,
                Restaurant=waitlist.Restaurant.RestaurantName,
                WaitlistEntryId=waitlist.WaitlistEntryId,
                PartySizew=waitlist.PartySize,
                WaitlistStatus=waitlist.WaitlistStatus,
                EntryOriginw=waitlist.EntryOrigin,
                WaitlistPosition="-"
            };
            if (waitlist.WaitlistStatus == "active")
            {
                int waitPos = RestaurantsManager.GetWaitlistPosition((int)waitlist.RestaurantId, waitlist.WaitlistEntryId);
                viewWait.WaitlistPosition = Convert.ToString(waitPos);
            }
            return View(viewWait);
        }
        //Customer's Waitlist entry details from History
        public IActionResult WaitlistHistoryDetails(int id)
        {
            LogRestaurant();
            var waitlist = RestaurantsManager.GetWaitlistById(id);

            var viewWait = new WaitCustomerModelView
            {
                CustomerId = (int)waitlist.CustomerId,
                Restaurant = waitlist.Restaurant.RestaurantName,
                WaitlistEntryId = waitlist.WaitlistEntryId,
                PartySizew = waitlist.PartySize,
                WaitlistStatus = waitlist.WaitlistStatus,
                EntryOriginw = waitlist.EntryOrigin,
                WaitlistPosition = "-"
            };
            if (waitlist.WaitlistStatus == "active")
            {
                int waitPos = RestaurantsManager.GetWaitlistPosition((int)waitlist.RestaurantId, waitlist.WaitlistEntryId);
                viewWait.WaitlistPosition = Convert.ToString(waitPos);
            }
            return View(viewWait);
        }
        //Customer's Reservation details from Reservations
        public IActionResult ReservationDetails(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);

            var viewReservation = new ResCustomerModelView
            {
                RestaurantID = (int)reservation.RestaurantId,
                CustomerId = (int)reservation.CustomerId,
                Restaurant = reservation.Restaurant.RestaurantName,
                ReservationEntryId = reservation.ReservationId,
                PartySize = reservation.PartySize,
                ReservationStatus = reservation.ReservationStatus,
                EntryOrigin = reservation.EntryOrigin,
                ReservationDate = reservation.ReservationDateTime,
                Message=reservation.CustomerMessage
            };
            return View(viewReservation);
        }
        //Customer's Reservation details from History
        public IActionResult ReservationHistoryDetails(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            var viewReservation = new ResCustomerModelView
            {
                RestaurantID = (int)reservation.RestaurantId,
                CustomerId = (int)reservation.CustomerId,
                Restaurant = reservation.Restaurant.RestaurantName,
                ReservationEntryId = reservation.ReservationId,
                PartySize = reservation.PartySize,
                ReservationStatus = reservation.ReservationStatus,
                EntryOrigin = reservation.EntryOrigin,
                ReservationDate = reservation.ReservationDateTime,
                Message=reservation.CustomerMessage
            };
            return View(viewReservation);
        }
        //Edit Waitlist Party
        public IActionResult WaitlistEditParty(int id)
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            var waitlist = RestaurantsManager.GetWaitlistById(id);
            var viewWait = new WaitCustomerModelView
            {
                CustomerId = (int)waitlist.CustomerId,
                Restaurant = waitlist.Restaurant.RestaurantName,
                WaitlistEntryId = waitlist.WaitlistEntryId,
                PartySizew = waitlist.PartySize,
                WaitlistStatus = waitlist.WaitlistStatus,
                EntryOriginw = waitlist.EntryOrigin,
                WaitlistPosition = "-"
            };
            if (waitlist.WaitlistStatus == "active")
            {
                int waitPos = RestaurantsManager.GetWaitlistPosition((int)waitlist.RestaurantId, waitlist.WaitlistEntryId);
                viewWait.WaitlistPosition = Convert.ToString(waitPos);
            }
            return View(viewWait);
        }

        //[Post]Edit Waitlist Party
        [HttpPost]
        public ActionResult WaitlistEditParty (WaitCustomerModelView viewWaitlist)
        {
            LogRestaurant();
            if (viewWaitlist.PartySizew <= 0)
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!!! Your Waitlist entry couldn't be updated. PartySize must be a non-zero positive integer";
                var waitlist = RestaurantsManager.GetWaitlistById(viewWaitlist.WaitlistEntryId);
                viewWaitlist.PartySizew = waitlist.PartySize;
                return RedirectToAction("WaitlistEditParty", waitlist.WaitlistEntryTable);
            }
            else
            {
                if (viewWaitlist.PartySizew >= 100)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Sorry!!! Your Waitlist entry couldn't be updated. Party size not supported by the Restaurant.";
                    var waitlist = RestaurantsManager.GetWaitlistById(viewWaitlist.WaitlistEntryId);
                    viewWaitlist.PartySizew = waitlist.PartySize;
                    return RedirectToAction("WaitlistEditParty", waitlist.WaitlistEntryTable);
                }
                else
                {
                    var waitlist = new WaitlistEntry
                    {
                        WaitlistEntryId = viewWaitlist.WaitlistEntryId,
                        PartySize = viewWaitlist.PartySizew
                    };
                    try
                    {
                        RestaurantsManager.UpdatePartySizeWaitlist(waitlist);
                        TempData["Message"] = "Your Waitlist entry was updated.";
                        TempData["ErrorMessage"] = null;
                        return RedirectToAction(nameof(WaitlistShow));
                    }
                    catch
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "Sorry!! It was not possible to Update your party size. Try again later!!!";
                        waitlist = RestaurantsManager.GetWaitlistById(viewWaitlist.WaitlistEntryId);
                        viewWaitlist.PartySizew = waitlist.PartySize;
                        return RedirectToAction("WaitlistEditParty", waitlist.WaitlistEntryTable);
                    }
                }
            }
        }
        //Edit Reservation
        public IActionResult ReservationEdit(int id)
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            var reservation = RestaurantsManager.GetReservationById(id);
            var viewReservation = new ResCustomerModelView
            {
                RestaurantID = (int)reservation.RestaurantId,
                CustomerId = (int)reservation.CustomerId,
                Restaurant = reservation.Restaurant.RestaurantName,
                ReservationEntryId = reservation.ReservationId,
                PartySize = reservation.PartySize,
                ReservationStatus = reservation.ReservationStatus,
                EntryOrigin = reservation.EntryOrigin,
                ReservationDate = reservation.ReservationDateTime
            };
            return View(viewReservation);
        }
        //[Post]Edit Reservation
        [HttpPost]
        public ActionResult ReservationEdit(ResCustomerModelView viewReservation)
        {
            LogRestaurant();
            if (viewReservation.PartySize <= 0)
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be updated. PartySize must be a non-zero positive number";

                return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
            }
            else
            {
                if (viewReservation.PartySize >= 100)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be updated. PartySize not supported by the Restaurant.";
                    return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
                }
                else
                {
                    var reservation = RestaurantsManager.GetReservationById(viewReservation.ReservationEntryId);
                    DateTime eDatetime = Convert.ToDateTime("1/1/0001 12:00:00 AM");
                    if (viewReservation.ReservationDate == eDatetime)
                    {
                        viewReservation.ReservationDate = reservation.ReservationDateTime;
                    }
                    if (viewReservation.ReservationDate.Date < DateTime.Today.Date)
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be updated. Reservations are only for future dates.";
                        return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
                    }
                    else
                    {
                        if (!(reservation.PartySize <= viewReservation.PartySize && reservation.ReservationDateTime == viewReservation.ReservationDate))
                        {
                            if (reservation.ReservationStatus == "confirmed")
                            {
                                TempData["Message"] = "Your reservation will be processed again due to the new date or party size request. The restaurant's manager will evaluate the restaurant availability and will reply as soon as possible.";
                                TempData["ErrorMessage"] = null;
                            }
                            else
                            {
                                TempData["Message"] = "Your Reservation was updated.  The restaurant's  manager will evaluate the restaurant availability and will reply as soon as possible.";
                                TempData["ErrorMessage"] = null;
                            }
                            reservation.ReservationStatus = "on Hold";
                        }
                        else
                        {
                            if (reservation.ReservationStatus == "confirmed")
                            {
                                TempData["Message"] = "Your Reservation was updated and confirmed.";
                                TempData["ErrorMessage"] = null;
                            }
                            else
                            {
                                TempData["Message"] = "Your Reservation was updated. The restaurant's manager will evaluate the restaurant availability and will reply as soon as possible";
                                TempData["ErrorMessage"] = null;
                            }
                        }
                        reservation.ReservationId = viewReservation.ReservationEntryId;
                        reservation.PartySize = viewReservation.PartySize;
                        reservation.ReservationDateTime = viewReservation.ReservationDate;
                        reservation.EntryOrigin = "app";
                        try
                        {
                            RestaurantsManager.UpdateReservation(reservation);
                            return RedirectToAction(nameof(ReservationsShow));
                        }
                        catch
                        {
                            return View();
                        }
                    }
                }
            }
        }
        //Cancel Waitlist Entry
        public IActionResult WaitlistCancel(int id)
        {
            LogRestaurant();
            RestaurantsManager.CancelPartySizeWaitlist(id);
            TempData["Message"] = "Your Waitlist entry was cancelled!!!!";
            TempData["ErrorMessage"] = null;
            return RedirectToAction(nameof(WaitlistShow));
        }
        //Cancel Reservation
        public IActionResult ReservationCancel(int id)
        {
            LogRestaurant();
            RestaurantsManager.CancelReservation(id);
            TempData["Message"] = "Your Reservation was cancelled!!!!";
            TempData["ErrorMessage"] = null;
            return RedirectToAction(nameof(ReservationsShow));
        }
        //Add Waitlist Entry
        public IActionResult WaitlistCreate()
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.Restaurants = BasicGetRestaurants();

            var viewWaitlist = new WaitCustomerModelView
            {
                  PartySizew = 1
            };
            return View(viewWaitlist);
        }
        //[Post]Add Waitlist Entry
        [HttpPost]
        public IActionResult WaitlistCreate(WaitCustomerModelView viewWaitlist)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var custId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "CustomerID").Value);
            if (viewWaitlist.PartySizew <= 0)
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!!! Your Waitlist entry couldn't be created. Party size must be a non-zero positive number";

                return RedirectToAction("WaitlistCreate");
            }
            else
            {
                if (viewWaitlist.PartySizew >= 100)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Sorry!!! Your Waitlist entry couldn't be created. Party size not supported by the Restaurant.";
                    return RedirectToAction("WaitlistCreate");
                }
                else
                {
                    if (RestaurantsManager.RestaurantActiveWaitlistByCustomer(viewWaitlist.RestaurantID, custId) == false)
                    {
                        var waitlist = new WaitlistEntry
                        {
                            EntryOrigin = "app",
                            PartySize = viewWaitlist.PartySizew,
                            CustomerId = custId,
                            RestaurantId = viewWaitlist.RestaurantID,
                            WaitlistStatus = "active",
                        };
                        try
                        {
                            RestaurantsManager.CreateWaitlist(waitlist);
                            TempData["Message"] = "Your NEW Waitlist entry is Active!!!";
                            TempData["ErrorMessage"] = null;
                            return RedirectToAction(nameof(WaitlistShow));
                        }
                        catch
                        {
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "Sorry!!! Each customer can have only one active Waitlist entry per Restaurant.";
                        ViewBag.Restaurants = BasicGetRestaurants();
                        return RedirectToAction("WaitlistCreate");
                    }
                }
            }
        }
        //Add Reservation
        public IActionResult ReservationCreate()
        {
            LogRestaurant();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.Restaurants = BasicGetRestaurants();
            var viewReservation = new ResCustomerModelView
            {
                ReservationDate = DateTime.Now,
                PartySize=1
            };
            return View(viewReservation);
        }
        //[Post]Add Reservation
        [HttpPost]
        public IActionResult ReservationCreate(ResCustomerModelView viewReservation)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var custId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "CustomerID").Value);
            if (viewReservation.PartySize <= 0)
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be created. PartySize must be a non-zero positive number";
                return RedirectToAction("ReservationCreate");
            }
            else
            {
                if(viewReservation.PartySize >= 100)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be created. PartySize not supported by the Restaurant.";
                    return RedirectToAction("ReservationCreate");
                }
                else
                {
                    DateTime eDatetime = Convert.ToDateTime("1/1/0001 12:00:00 AM");
                    if (viewReservation.ReservationDate == eDatetime)
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "You must pick a date and time to complete your reservation.";
                        return RedirectToAction("ReservationCreate");
                    }
                    else
                    {
                        if (viewReservation.ReservationDate.Date <= DateTime.Today.Date)
                        {
                            if (viewReservation.ReservationDate.Date == DateTime.Today.Date)
                            {
                                TempData["Message"] = null;
                                TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be created. Reservations for today are closed. Try the waitlist!";
                            }
                            else
                            {
                                TempData["Message"] = null;
                                TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be created. Reservations are only for future dates.";
                            }
                            return RedirectToAction("ReservationCreate");
                        }
                        else
                        {
                            var reservation = new ReservationEntry
                            {
                                EntryOrigin = "app",
                                PartySize = viewReservation.PartySize,
                                CustomerId = custId,
                                RestaurantId = viewReservation.RestaurantID,
                                ReservationStatus = "on Hold",
                                ReservationDateTime = viewReservation.ReservationDate,
                            };
                            try
                            {
                                TempData["Message"] = "Your NEW Reservation was created. The restaurant's manager will evaluate the restaurant availability and will reply as soon as possible";
                                TempData["ErrorMessage"] = null;
                                RestaurantsManager.CreateReservation(reservation);
                                return RedirectToAction(nameof(ReservationsShow));
                            }
                            catch
                            {
                                return View();
                            }
                        }
                    }
                }
            }
        }
        //List of Restaurants in the database
        protected IEnumerable BasicGetRestaurants()
        {
            //call the Asset Type manager and get the collection of Key value objects
            var types = RestaurantsManager.GetAsKeyValuePairs();
            //Create a collection of SelecteListItems
            var styles = new SelectList(types, "Value", "Text");

            return styles;
        }
        //Verify with User is logged in as restaurant staff
        protected void LogRestaurant()
        {
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            if (claims.Count() != 0)
            {
                if (claims.Where(p => p.Type == "RestaurantID").Count() != 0)
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