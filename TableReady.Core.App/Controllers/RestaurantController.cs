//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TableReady.Core.App.Models;
using TableReady.Core.App.ViewComponents;
using TableReady.Core.BLL;
using TableReady.Core.Data.Domain;

namespace TableReady.Core.App.Controllers
{
    //Controller to manage Restaurant
    [Authorize(Roles = "Owner,Employee,Manager")]
    public class RestaurantController : Controller
    {
        //Restaurant Profile
        public IActionResult RestaurantShow()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurant = RestaurantsManager.GetRestauranByID(restId);
            var viewRestaurant = new RestaurantCreateModelView
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName,
            };
            return View(viewRestaurant);
        }
        //Edit Restaurant Profile
        [Authorize(Roles = "Owner,Manager")]
        public IActionResult RestaurantEdit(int id)
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
            var restaurant = RestaurantsManager.GetRestauranByID(id);
            var viewRestaurant = new RestaurantCreateModelView
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName,
            };
            return View(viewRestaurant);
        }
        //[Post]Edit Restaurant Profile
        [HttpPost]
        public IActionResult RestaurantEdit(RestaurantCreateModelView viewRestaurant)
        {
            LogRestaurant();
            var newRestaurant = RestaurantsManager.GetRestaurantIdByName(viewRestaurant.RestaurantName);
            if (newRestaurant == 0)
            {
                var restaurant = new Restaurants
                {
                    RestaurantId = viewRestaurant.RestaurantId,
                    RestaurantName = viewRestaurant.RestaurantName
                };
                RestaurantsManager.EditRestaurant(restaurant);
                return RedirectToAction("RestaurantShow");
            }
            else
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!! The Restaurant's name is already registered. Choose another Restaurant's Name.";
                return RedirectToAction("RestaurantEdit", viewRestaurant.RestaurantId);
            }
        }
        //Restaurant's Owners
        [Authorize(Roles = "Owner")]
        public IActionResult RestaurantOwnersShow()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantOwners = RestaurantsManager.GetRestauranOwnersByID(restId);
            List<OwnerModelView> viewRestaurantOwners = new List<OwnerModelView>();
            foreach (RestaurantOwners rowner in restaurantOwners)
            {
                if (rowner.Request == false)
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
            ViewBag.RestaurantID = restId;
            return View(viewRestaurantOwners);
        }
        //Restaurant's Employee
        [Authorize(Roles = "Owner,Manager")]
        public IActionResult RestaurantEmployeesShow()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantEmployees = RestaurantsManager.GetRestauranEmployeesByID(restId);
            List<EmployeeModelView> viewRestaurantEmployees = new List<EmployeeModelView>();
            foreach (RestaurantEmployees remployee in restaurantEmployees)
            {
                if (remployee.NewRequestFlag != true)
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
            ViewBag.RestaurantID = restId;
            return View(viewRestaurantEmployees);
        }
        //Manage Resrvations
        [Authorize(Roles = "Owner,Manager")]
        public IActionResult ReservationManage()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var reservations = RestaurantsManager.GetManageReservations(restId);
            List<ResCustomerModelView> viewReservations = new List<ResCustomerModelView>();
            foreach (ReservationEntry resAux in reservations)
            {
                if (resAux.ReservationStatus != "on Hold")
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
            ViewBag.RestaurantID = restId;
            return View(viewReservations);
        }
        //Host Management 
        [Authorize(Roles = "Owner,Employee,Manager")]
        public IActionResult HostManage()
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var waitlist = RestaurantsManager.GetWaitlistByRestaurantID(restId);
            List<WaitCustomerModelView> viewWaitlist = new List<WaitCustomerModelView>();
            foreach (WaitlistEntry waitAux in waitlist)
            {
                var customer = CustomersManager.GetCustomerByCustomerId(waitAux.CustomerId);
                var viewWaitlistEntry = new WaitCustomerModelView
                {
                    RestaurantID = (int)waitAux.RestaurantId,
                    CustomerId = (int)waitAux.CustomerId,
                    CustomerName = $"{customer.User.FirstName} {customer.User.LastName}",
                    Restaurant = waitAux.Restaurant.RestaurantName,
                    WaitlistEntryId = waitAux.WaitlistEntryId,
                    PartySizew = waitAux.PartySize,
                    WaitlistStatus = waitAux.WaitlistStatus,
                    EntryOriginw = waitAux.EntryOrigin,
                    WaitlistPosition = Convert.ToString(RestaurantsManager.GetWaitlistPosition(restId, waitAux.WaitlistEntryId))
                };
                viewWaitlist.Add(viewWaitlistEntry);
            }
            var orderViewWaitlist = viewWaitlist.OrderBy(w => w.WaitlistPosition);
            ViewBag.RestaurantID = restId;
            return View(orderViewWaitlist);
        }
        //Owners's Details
        public IActionResult OwnerDetails(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantOwner = RestaurantsManager.RestaurantByOwnerId(restId, id);
            var ownerUser = UsersManager.GetUserByUserId(restaurantOwner.Owner.UserId);
            string fullName = $"{ownerUser.FirstName} {ownerUser.LastName}";
            var viewOwner = new OwnerModelView
            {
                RestaurantId = restaurantOwner.RestaurantId,
                OwnerId = restaurantOwner.OwnerId,
                UserId = ownerUser.UserId,
                OwnerFullName = fullName,
                Active = restaurantOwner.Active,
                Status = restaurantOwner.Status,
                StartDate = (DateTime)restaurantOwner.StartDate,
                EndDate = restaurantOwner.EndDate,
                RequestFlag = restaurantOwner.Request,
                RequestStatus = restaurantOwner.RequestStatus
            };
            return View(viewOwner);
        }
        //Employee's Details
        public IActionResult EmployeeDetails(int id)
        {
            LogRestaurant();
            string role;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var userId = EmployeesManager.GetUserIdByEmployeeId(id);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var authRestaurant = UsersManager.GetAuthenticationMatrixByIDs(restId, authId);
            if (authRestaurant == null)
            {
                role = "";
            }
            else
            {
                role = authRestaurant.Role;
            }
            var restaurantEmployee = RestaurantsManager.RestaurantByEmployeeId(restId, id);
            var employeeUser = UsersManager.GetUserByUserId(userId);
            string fullName = $"{employeeUser.FirstName} {employeeUser.LastName}";
            var viewEmployee = new EmployeeModelView
            {
                RestaurantId = restaurantEmployee.RestaurantId,
                EmployeeId = restaurantEmployee.EmployeeId,
                UserId = userId,
                EmployeeFullName = fullName,
                Active = restaurantEmployee.Active,
                Status = restaurantEmployee.Status,
                StartDate = (DateTime)restaurantEmployee.StartDate,
                EndDate = restaurantEmployee.EndDate,
                RequestFlag = restaurantEmployee.NewRequestFlag,
                RequestStatus = restaurantEmployee.RequestStatus,
                Role = role
            };
            return View(viewEmployee);
        }
        //Approve Owner's Application
        public IActionResult OwnerApprove(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var userId = OwnersManager.GetUserIdByOwnerId(id);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var restaurantOwner = RestaurantsManager.RestaurantByOwnerId(restId, id);
            restaurantOwner.Request = false;
            restaurantOwner.Status = "Owner";
            restaurantOwner.RequestStatus = "Approved";
            restaurantOwner.Active = true;
            RestaurantsManager.UpdateRestaurantOwner(restaurantOwner);
            var authRestaurant = UsersManager.GetAuthenticationMatrixByIDs(restId, authId);
            if (authRestaurant == null)
            {
                var authRest = new AuthenticationMatrix
                {
                    RestaurantId = restId,
                    AuthenticationId = authId,
                    Role = "Ownwer"
                };
                UsersManager.AddOwnerToAuthetication(authRest);
            }
            else
            {
                authRestaurant.Role = "Ownwer";
                UsersManager.UpdateAuthenticationMatrixByIDs(authRestaurant);
            }
            return RedirectToAction("RestaurantOwnersShow");
        }
        //Deny Owner's Application
        public IActionResult OwnerDeny(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantOwner = RestaurantsManager.RestaurantByOwnerId(restId, id);
            restaurantOwner.Request = false;
            restaurantOwner.Status = "Not Accepted";
            restaurantOwner.RequestStatus = "Denied";
            restaurantOwner.Active = false;
            RestaurantsManager.UpdateRestaurantOwner(restaurantOwner);
            return RedirectToAction("RestaurantOwnersShow");
        }
        //Approve Employee's Application
        public IActionResult EmployeeApprove(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var userId = EmployeesManager.GetUserIdByEmployeeId(id);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var restaurantEmployee = RestaurantsManager.RestaurantByEmployeeId(restId, id);
            restaurantEmployee.NewRequestFlag = false;
            restaurantEmployee.Status = "Employee";
            restaurantEmployee.RequestStatus = "Approved";
            restaurantEmployee.Active = true;
            RestaurantsManager.UpdateRestaurantEmployee(restaurantEmployee);
            var authRestaurant = UsersManager.GetAuthenticationMatrixByIDs(restId, authId);
            if (authRestaurant == null)
            {
                var authRest = new AuthenticationMatrix
                {
                    RestaurantId = restId,
                    AuthenticationId = authId,
                    Role = "Employee"
                };
                UsersManager.AddOwnerToAuthetication(authRest);
            }
            else
            {
                authRestaurant.Role = "Employee";
                UsersManager.UpdateAuthenticationMatrixByIDs(authRestaurant);
            }
            return RedirectToAction("RestaurantEmployeesShow");
        }
        //Deny Employee's Application
        public IActionResult EmployeeDeny(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantEmployee = RestaurantsManager.RestaurantByEmployeeId(restId, id);
            restaurantEmployee.NewRequestFlag = false;
            restaurantEmployee.Status = "Not Accepted";
            restaurantEmployee.RequestStatus = "Denied";
            restaurantEmployee.Active = false;
            RestaurantsManager.UpdateRestaurantEmployee(restaurantEmployee);
            return RedirectToAction("RestaurantEmployeesShow");
        }

        //Cancel Reservation
        public IActionResult ReservationCancel(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            reservation.ReservationStatus = "Cancelled";
            RestaurantsManager.UpdateReservation(reservation);
            return RedirectToAction("HostManage");
        }
        //Set NoShow to Reservation
        public IActionResult ReservationNoShow(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            reservation.ReservationStatus = "NoShow";
            RestaurantsManager.UpdateReservation(reservation);
            return RedirectToAction("HostManage");
        }
        //Check-in Reservation
        public IActionResult ReservationCheckin(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            reservation.CheckinDateTime = DateTime.Now;
            RestaurantsManager.UpdateReservation(reservation);
            return RedirectToAction("HostManage");
        }
        //Cancel Waitlist Entry
        public IActionResult WaitlistCancel(int id)
        {
            LogRestaurant();
            var waitlistEntry = RestaurantsManager.GetWaitlistById(id);
            waitlistEntry.WaitlistStatus = "Cancelled";
            RestaurantsManager.UpdateWaitlist(waitlistEntry);
            return RedirectToAction("HostManage");
        }
        //Set NoShow to Waitlist Entry
        public IActionResult WaitlistNoShow(int id)
        {
            LogRestaurant();
            var waitlistEntry = RestaurantsManager.GetWaitlistById(id);
            waitlistEntry.WaitlistStatus = "NoShow";
            RestaurantsManager.UpdateWaitlist(waitlistEntry);
            return RedirectToAction("HostManage");
        }
        //Approve Reservation
        public IActionResult ReservationApprove(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            reservation.ReservationStatus = "confirmed";
            RestaurantsManager.UpdateReservation(reservation);
            return RedirectToAction("ReservationManage");
        }
        //Reservation's Details
        public IActionResult ReservationDetails(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            var userId = CustomersManager.GetUserIdByCustomerId(reservation.CustomerId);
            var customerUser = UsersManager.GetUserByUserId(userId);
            string fullName = $"{customerUser.FirstName} {customerUser.LastName}";
            var viewreservation = new ResCustomerModelView
            {
                CustomerName = fullName,
                EntryOrigin = reservation.EntryOrigin,
                PartySize = reservation.PartySize,
                ReservationStatus = reservation.ReservationStatus,
                ReservationDate = reservation.ReservationDateTime,
                ReservationEntryId = reservation.ReservationId,
                RestaurantID = reservation.RestaurantId
            };
            return View(viewreservation);
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
            var userId = CustomersManager.GetUserIdByCustomerId(reservation.CustomerId);
            var customerUser = UsersManager.GetUserByUserId(userId);
            string fullName = $"{customerUser.FirstName} {customerUser.LastName}";
            var viewreservation = new ResCustomerModelView
            {
                CustomerName = fullName,
                EntryOrigin = reservation.EntryOrigin,
                PartySize = reservation.PartySize,
                ReservationStatus = reservation.ReservationStatus,
                ReservationDate = reservation.ReservationDateTime,
                ReservationEntryId = reservation.ReservationId,
                RestaurantID = reservation.RestaurantId
            };
            ViewBag.ReservationStatus = GetReservationStatus();
            return View(viewreservation);
        }
        //[Post] Edit Reservation
        [HttpPost]
        public IActionResult ReservationEdit(ResCustomerModelView viewReservation)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(viewReservation.ReservationEntryId);
            DateTime eDatetime = Convert.ToDateTime("1/1/0001 12:00:00 AM");
            if (viewReservation.ReservationDate == eDatetime)
            {
                viewReservation.ReservationDate = reservation.ReservationDateTime;
            }
            if (viewReservation.ReservationDate.Date < DateTime.Today.Date && viewReservation.ReservationStatus=="confirmed")
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be updated. Reservations are only for future dates.";
                return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
            }
            else
            {
                if (viewReservation.PartySize <= 0)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be Updated. PartySize must be a non-zero positive number";
                    return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
                }
                else
                {
                    if (viewReservation.PartySize >= 100)
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "Sorry!!! Your Reservation couldn't be Updated. PartySize not supported by the Restaurant.";
                        return RedirectToAction("ReservationEdit", viewReservation.ReservationEntryId);
                    }
                    else
                    {
                        reservation.ReservationStatus = GetReservationStatusValue(viewReservation.ReservationStatus);
                        reservation.ReservationDateTime = viewReservation.ReservationDate;
                        reservation.PartySize = viewReservation.PartySize;
                        reservation.EntryOrigin = viewReservation.EntryOrigin;
                        RestaurantsManager.UpdateReservation(reservation);
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = null;
                        return RedirectToAction("ReservationManage");
                    }
                }
            }
        }
        //Deny Reservation
        public IActionResult ReservationDeny(int id)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(id);
            var userId = CustomersManager.GetUserIdByCustomerId(reservation.CustomerId);
            var customerUser = UsersManager.GetUserByUserId(userId);
            string fullName = $"{customerUser.FirstName} {customerUser.LastName}";
            var viewreservation = new ResCustomerModelView
            {
                CustomerName = fullName,
                EntryOrigin = reservation.EntryOrigin,
                PartySize = reservation.PartySize,
                ReservationStatus = "denied",
                ReservationDate = reservation.ReservationDateTime,
                ReservationEntryId = reservation.ReservationId,
                RestaurantID = reservation.RestaurantId,
            };
            return View(viewreservation);
        }
        //[Post]Deny Reservation
        [HttpPost]
        public IActionResult ReservationDeny(ResCustomerModelView viewreservation)
        {
            LogRestaurant();
            var reservation = RestaurantsManager.GetReservationById(viewreservation.ReservationEntryId);
            reservation.CustomerMessage = viewreservation.Message;
            reservation.ReservationStatus = "denied";
            RestaurantsManager.UpdateReservation(reservation);
            return RedirectToAction("ReservationManage");
        }
        //Edit Owner's details
        public IActionResult OwnerEdit(int id)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantOwner = RestaurantsManager.RestaurantByOwnerId(restId, id);
            var ownerUser = UsersManager.GetUserByUserId(restaurantOwner.Owner.UserId);
            string fullName = $"{ownerUser.FirstName} {ownerUser.LastName}";
            var viewOwner = new OwnerModelView
            {
                RestaurantId = restaurantOwner.RestaurantId,
                OwnerId = restaurantOwner.OwnerId,
                UserId = ownerUser.UserId,
                OwnerFullName = fullName,
                Active = restaurantOwner.Active,
                Status = restaurantOwner.Status,
                StartDate = (DateTime)restaurantOwner.StartDate,
                EndDate = restaurantOwner.EndDate,
                RequestFlag = restaurantOwner.Request,
                RequestStatus = restaurantOwner.RequestStatus
            };
            ViewBag.OwnerStatus = GetOwnerStatus();
            return View(viewOwner);
        }
        //[Post]Edit Owner's details
        [HttpPost]
        public IActionResult OwnerEdit(OwnerModelView viewOwner)
        {
            LogRestaurant();
            var userId = OwnersManager.GetUserIdByOwnerId(viewOwner.OwnerId);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var restaurantOwner = RestaurantsManager.RestaurantByOwnerId(viewOwner.RestaurantId, viewOwner.OwnerId);
            var authMatrix = UsersManager.GetAuthenticationMatrixByIDs(viewOwner.RestaurantId, authId);
            viewOwner.Status=GetOwnerStatusValue(viewOwner.Status);
            if (restaurantOwner.Active == false && viewOwner.Active == true)
            {
                if (viewOwner.Status == "Owner" || viewOwner.Status == "Primary Owner")
                {
                    restaurantOwner.RequestStatus = "Approved";
                    if (authMatrix != null)
                    {
                        authMatrix.Role = "Owner";
                        UsersManager.UpdateAuthenticationMatrixByIDs(authMatrix);
                    }
                    else
                    {
                        var newAuthMatrix = new AuthenticationMatrix
                        {
                            AuthenticationId = authId,
                            RestaurantId = viewOwner.RestaurantId,
                            Role = "Owner"
                        };
                        UsersManager.AddOwnerToAuthetication(newAuthMatrix);
                    }
                }
            }
            else
            {
                if (restaurantOwner.Active == true && viewOwner.Active == false)
                {
                    if (viewOwner.Status == "Owner" || viewOwner.Status == "Primary Owner")
                    {
                        authMatrix.Role = "OwnerLeave";
                        UsersManager.UpdateAuthenticationMatrixByIDs(authMatrix);
                    }
                    else
                    {
                        UsersManager.DeleteAuthMatrixByIds(viewOwner.RestaurantId, authId);
                    }
                }
            }
            restaurantOwner.Status = viewOwner.Status;
            restaurantOwner.EndDate = viewOwner.EndDate;
            restaurantOwner.Active = viewOwner.Active;
            RestaurantsManager.UpdateRestaurantOwner(restaurantOwner);
            return RedirectToAction("RestaurantOwnersShow");
            
        }
        //Edit Employee's Edit
        public IActionResult EmployeeEdit(int id)
        {
            LogRestaurant();
            string role;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var restId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "RestaurantID").Value);
            var restaurantEmployee = RestaurantsManager.RestaurantByEmployeeId(restId, id);
            var userId = EmployeesManager.GetUserIdByEmployeeId(restaurantEmployee.EmployeeId);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var authMatrix = UsersManager.GetAuthenticationMatrixByIDs(restId, authId);
            if (authMatrix != null) 
            {
                role = authMatrix.Role;
            }
            else
            {
                role = "";
            }
            var employeeUser = UsersManager.GetUserByUserId(userId);
            string fullName = $"{employeeUser.FirstName} {employeeUser.LastName}";
            var viewEmployee = new EmployeeModelView
            {
                RestaurantId = restaurantEmployee.RestaurantId,
                EmployeeId = restaurantEmployee.EmployeeId,
                UserId = employeeUser.UserId,
                EmployeeFullName = fullName,
                Active = restaurantEmployee.Active,
                Status = restaurantEmployee.Status,
                StartDate = (DateTime)restaurantEmployee.StartDate,
                EndDate = restaurantEmployee.EndDate,
                RequestFlag = restaurantEmployee.NewRequestFlag,
                RequestStatus = restaurantEmployee.RequestStatus,
                Role=role
            };
            ViewBag.EmployeeStatus = GetEmployeeStatus();
            ViewBag.EmployeeRole = GetRestaurantRoles();
            ViewBag.EmployeeStatusV = GetEmployeeStatusText(restaurantEmployee.Status);
            if (GetRestaurantRolesValue(role)=="")
                ViewBag.EmplyeeRoleV = "0";
            else ViewBag.EmplyeeRoleV = GetRestaurantRolesText(role);
            return View(viewEmployee);
        }
        //[Post]Edit Employee's Edit
        [HttpPost]
        public IActionResult EmployeeEdit(EmployeeModelView viewEmployee)
        {
            LogRestaurant();
            string role;
            var userId = EmployeesManager.GetUserIdByEmployeeId(viewEmployee.EmployeeId);
            var authId = UsersManager.GetAuthIdByUserId(userId);
            var restaurantEmployee = RestaurantsManager.RestaurantByEmployeeId(viewEmployee.RestaurantId, viewEmployee.EmployeeId);
            var authMatrix = UsersManager.GetAuthenticationMatrixByIDs(viewEmployee.RestaurantId, authId);
            viewEmployee.Status = GetEmployeeStatusValue(viewEmployee.Status);
            viewEmployee.Role = GetRestaurantRolesValue(viewEmployee.Role);
            if (restaurantEmployee.Active == false && viewEmployee.Active == true)
            {
                if (viewEmployee.Status == "Employee" )
                {
                    restaurantEmployee.RequestStatus = "Approved";
                    viewEmployee.EndDate = null;
                    if (authMatrix != null)
                    {
                        if(viewEmployee.Role==null)
                        {
                            authMatrix.Role = "Employee";
                        }
                        else
                        {
                            authMatrix.Role = viewEmployee.Role;
                        }
                        UsersManager.UpdateAuthenticationMatrixByIDs(authMatrix);
                    }
                    else
                    {
                        if (viewEmployee.Role == null)
                        {
                            role = "Employee";
                        }
                        else
                        {
                            role = viewEmployee.Role;
                        }
                        var newAuthMatrix = new AuthenticationMatrix
                        {
                            AuthenticationId = authId,
                            RestaurantId = viewEmployee.RestaurantId,
                            Role = role
                        };
                        UsersManager.AddEmployeeToAutheticationMatrix(newAuthMatrix);
                    }
                }
            }
            else
            {

                if (restaurantEmployee.Active == true && viewEmployee.Active == false)
                {
                    if (viewEmployee.Status == "Employee"|| viewEmployee.Status == "Employee on Leave")
                    {
                        if (authMatrix == null)
                        {
                            var newAuthMatrix = new AuthenticationMatrix
                            {
                                AuthenticationId = authId,
                                RestaurantId = viewEmployee.RestaurantId,
                            };
                        }
                            authMatrix.Role = "Employee on Leave";
                        UsersManager.UpdateAuthenticationMatrixByIDs(authMatrix);
                    }
                    else
                    {
                        if (authMatrix != null)
                        {
                            UsersManager.DeleteAuthMatrixByIds(viewEmployee.RestaurantId, authId);
                        }
                    }
                }
                else
                {
                    if (viewEmployee.Status != "Employee")
                    {
                        if (authMatrix != null)
                        {

                            UsersManager.DeleteAuthMatrixByIds(viewEmployee.RestaurantId, authId);
                        }
                    }
                    else
                    {
                        if (authMatrix == null)
                        {
                            var newAuthMatrix = new AuthenticationMatrix
                            {
                                AuthenticationId = authId,
                                RestaurantId = viewEmployee.RestaurantId,
                            };
                        }
                        authMatrix.Role = viewEmployee.Role;
                        UsersManager.UpdateAuthenticationMatrixByIDs(authMatrix);
                    }
                }
            }
            restaurantEmployee.Status = viewEmployee.Status;
            restaurantEmployee.EndDate = viewEmployee.EndDate;
            restaurantEmployee.Active = viewEmployee.Active;
            RestaurantsManager.UpdateRestaurantEmployee(restaurantEmployee);
            return RedirectToAction("RestaurantEmployeesShow");
        }

        //List of Restaurant's Roles
        protected IEnumerable GetRestaurantRoles()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "Employee",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Manager",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Employee on Leave",
                Value = "2"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }
        //List of Owner's Status
        protected IEnumerable GetOwnerStatus()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "Primary Owner",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Owner",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Former Owner",
                Value = "2"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Owner on Leave",
                Value = "3"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }
        //List of Request's Status
        protected IEnumerable GetRequestStatus()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "Approved",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Denied",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "on Hold",
                Value = "2"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }
        //List of Reservation's Status
        protected IEnumerable GetReservationStatus()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "confirmed",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "on Hold",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "cancelled",
                Value = "2"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }

       
        //List of Employees's Status
        protected IEnumerable GetEmployeeStatus()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "Employee",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Former Employee",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Employee on Leave",
                Value = "2"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }
        //List of Origin Entry
        protected IEnumerable GetEntryOrigin()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "app",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "phone",
                Value = "1"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "entrance",
                Value = "2"
            });
            userTypes.Add(new SelectListItem
            {
               Text = "email",
                Value = "3"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
            return styles;
        }
        protected string GetReservationStatusValue(string value)
        {
            string text;
            text = "";
            var status = GetReservationStatus();
            foreach (SelectListItem aux in status)
            {
                if (aux.Value == value)
                    text = aux.Text;
            }
            return text;
        }

        protected string GetEmployeeStatusValue(string value)
        {
            string text;
            text = "";
            var status = GetEmployeeStatus();
            foreach (SelectListItem aux in status)
            {
                if (aux.Value == value)
                    text = aux.Text;
            }
            return text;
        }

        protected string GetEmployeeStatusText(string text)
        {
            string value;
            value = "";
            var status = GetEmployeeStatus();
            foreach (SelectListItem aux in status)
            {
                if (aux.Text == text)
                    value = aux.Value;
            }
            return value;
        }
        protected string GetRestaurantRolesText(string text)
        {
            string value;
            value = "";
            var status = GetRestaurantRoles();
            foreach (SelectListItem aux in status)
            {
                if (aux.Text == text)
                    value = aux.Value;
            }
            return value;
        }
        protected string GetRestaurantRolesValue(string value)
        {
            string text;
            text = "";
            var status = GetRestaurantRoles();
            foreach (SelectListItem aux in status)
            {
                if (aux.Value == value)
                    text = aux.Text;
            }
            return text;
        }
        protected string GetOwnerStatusValue(string value)
        {
            string text;
            text = "";
            var status = GetOwnerStatus();
            foreach (SelectListItem aux in status)
            {
                if (aux.Value == value)
                    text = aux.Text;
            }
            return text;
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


