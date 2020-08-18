//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TableReady.Core.Data.Domain;

namespace TableReady.Core.BLL
{
    //Business Layer to Manage Restaurant
    public class RestaurantsManager
    {
        public static string GetRestaurantIdByNameByRestauranID(int restId)
        {
            var context = new TableReadyContext();
            var restaurant = context.Restaurants.SingleOrDefault(r => r.RestaurantId == restId);
            if (restaurant == null)
                return "";
            else return restaurant.RestaurantName;
        }
        public static int GetRestaurantIdByName(string restName)
        {
            var context = new TableReadyContext();
            var restaurant = context.Restaurants.SingleOrDefault(r => r.RestaurantName == restName);
            if (restaurant == null)
                return 0;
            else return restaurant.RestaurantId;
        }
        public static List<ReservationEntry> GetTodayReservationsByRestaurantID(int restId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.RestaurantId == restId && p.ReservationStatus == "confirmed"&& p.ReservationDateTime.Date==DateTime.Today.Date && p.CheckinDateTime==null).ToList();
            return reservations;
        }
        public static List<ReservationEntry> GetCheckinTodayReservationsByRestaurantID(int restId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.RestaurantId == restId && p.ReservationStatus == "confirmed" && p.ReservationDateTime.Date == DateTime.Today.Date && p.CheckinDateTime!=null).ToList();
            return reservations;
        }
        public static List<ReservationEntry> GetReservationsByCustomerID(int custId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == custId ).ToList();
            return reservations;
        }
        public static List<WaitlistEntry> GetWaitlistByRestaurantID(int restId)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant).Where(p => p.RestaurantId == restId && (p.WaitlistStatus == "active")).ToList();
            return waitlist;
        }
        public static List<ReservationEntry> GetManageReservations(int restId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.RestaurantId == restId && (p.ReservationStatus == "confirmed" || p.ReservationStatus == "on Hold")).ToList();
            return reservations;
        }
        public static IList GetAsKeyValuePairs()
        {
            var context = new TableReadyContext();
            var restaurants = context.Restaurants.Select(at => new
            {
                Value = at.RestaurantId,
                Text = at.RestaurantName
            }).ToList();
            return restaurants;
        }
        public static List<Users> GetAll()
        {
            var context = new TableReadyContext();
            var users = context.Users.Include(p => p.Authentication).Include(p => p.Authentication.AuthenticationMatrix).ToList();

            return users;
        }
        public static Users AuthenticateRestaurant(string username, string password, int restaurantId)
        {
            Users user = new Users();
            var usr = GetAll().SingleOrDefault(usr => usr.Authentication.Username == username && usr.Authentication.Password == password);

            if(usr!=null)
            { 
                if (usr.Authentication.AuthenticationMatrix.Count() > 0)
                {
                    foreach (AuthenticationMatrix a in usr.Authentication.AuthenticationMatrix)
                    {
                        if (a.RestaurantId == restaurantId) user = usr;
                    }
                }
            }
            return user;//this will be eithe null or an object
        }
        public static List<ReservationEntry> GetReservations(int restaurantId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Where(p => p.RestaurantId== restaurantId).ToList();
            return reservations;
        }
        public static int GetWaitlistPosition(int restId, int waitlistId)
        {
            int j = 0;
            var context = new TableReadyContext();
            var waitlist=context.WaitlistEntry.Where(p => p.RestaurantId == restId && p.WaitlistStatus=="active").ToList().OrderBy(p=>p.CheckinDate);
            if (waitlist.Count() > 0)
            {
                int i = 1;
                foreach (WaitlistEntry auxWait in waitlist)
                {
                    if (auxWait.WaitlistEntryId == waitlistId)
                        return i;
                    i++;
                }
            }
            return j;
        }
        public static WaitlistEntry GetWaitlistById(int Id)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant).SingleOrDefault(p => p.WaitlistEntryId == Id);

            return waitlist;
        }
        public static ReservationEntry GetReservationById(int Id)
        {
            var context = new TableReadyContext();
            var reservation = context.ReservationEntry.Include(r => r.Restaurant).SingleOrDefault(p => p.ReservationId == Id);
            return reservation;
        }
        public static bool RestaurantActiveWaitlistByCustomer(int restId, int custId)
        {
            bool active = false;
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Where(p => p.RestaurantId == restId && p.CustomerId==custId );
            if (waitlist.Count()!=0) 
            {
                foreach (WaitlistEntry aux in waitlist)
                {
                    if (aux.WaitlistStatus == "active") active=true;
                }     
            }
            return active;
        }
        public static void UpdatePartySizeWaitlist(WaitlistEntry waitlist)
        {
            var context = new TableReadyContext();
            var originalWaitlist = context.WaitlistEntry.SingleOrDefault(p => p.WaitlistEntryId == waitlist.WaitlistEntryId);
            originalWaitlist.PartySize = waitlist.PartySize;
            context.SaveChanges();
        }
        public static void UpdateReservation(ReservationEntry reservation)
        {
            var context = new TableReadyContext();
            var originalReservation = context.ReservationEntry.SingleOrDefault(p => p.ReservationId == reservation.ReservationId);
            originalReservation.PartySize = reservation.PartySize;
            originalReservation.ReservationDateTime = reservation.ReservationDateTime;
            originalReservation.ReservationStatus = reservation.ReservationStatus;
            originalReservation.CheckinDateTime = reservation.CheckinDateTime;
            originalReservation.CustomerMessage = reservation.CustomerMessage;
            context.SaveChanges();
        }
        public static void UpdateWaitlist(WaitlistEntry waitEntry)
        {
            var context = new TableReadyContext();
            var originalWailisttEntry = context.WaitlistEntry.SingleOrDefault(p => p.WaitlistEntryId == waitEntry.WaitlistEntryId);
            originalWailisttEntry.PartySize = waitEntry.PartySize;
            originalWailisttEntry.WaitlistStatus = waitEntry.WaitlistStatus;
            originalWailisttEntry.EntryOrigin = waitEntry.EntryOrigin;
            originalWailisttEntry.CheckinDate = waitEntry.CheckinDate;
            context.SaveChanges();
        }
        public static void CancelPartySizeWaitlist(int id)
        {
            var context = new TableReadyContext();
            var originalWaitlist = context.WaitlistEntry.SingleOrDefault(p => p.WaitlistEntryId == id);
            originalWaitlist.WaitlistStatus = "cancelled";
            context.SaveChanges();
        }
        public static void CancelReservation(int id)
        {
            var context = new TableReadyContext();
            var originalReservation = context.ReservationEntry.SingleOrDefault(p => p.ReservationId == id);
            originalReservation.ReservationStatus = "cancelled";
            context.SaveChanges();
        }
        public static void CreateWaitlist(WaitlistEntry waitlist)
        {
            var context = new TableReadyContext();
            context.WaitlistEntry.Add(waitlist);
            context.SaveChanges();
        }
        public static void CreateReservation(ReservationEntry reservation)
        {
            var context = new TableReadyContext();
            context.ReservationEntry.Add(reservation);
            context.SaveChanges();
        }
        public static int CreateRestaurant(Restaurants restaurant)
        {
            var context = new TableReadyContext();
            var restGroup = context.Restaurants;
            var maxRestBefore = restGroup.SingleOrDefault(p => p.RestaurantId == restGroup.Max(k => k.RestaurantId));
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            var maxRestAfter = restGroup.SingleOrDefault(p => p.RestaurantId == restGroup.Max(k => k.RestaurantId));
            if (maxRestAfter.RestaurantId == maxRestBefore.RestaurantId + 1)
            {
                return maxRestAfter.RestaurantId;
            }
            else
            {
                return 0;
            }
        }
        public static void AddOwnerToRestaurant(RestaurantOwners restaurantOwner)
        {
            var context = new TableReadyContext();
            context.RestaurantOwners.Add(restaurantOwner);
            context.SaveChanges();
        }
        public static void AddEmployeeToRestaurant(RestaurantEmployees restaurantEmployee)
        {
            var context = new TableReadyContext();
            context.RestaurantEmployees.Add(restaurantEmployee);
            context.SaveChanges();
        }
        public static List<RestaurantOwners> RestaurantsByOwnerId(int ownId)
        {
            var context = new TableReadyContext();
            var restaurants = context.RestaurantOwners.Include(r => r.Restaurant).Where(p => p.OwnerId == ownId).ToList();
            return restaurants;
        }
        public static RestaurantOwners RestaurantByOwnerId(int restId, int ownId)
        {
            var context = new TableReadyContext();
            var restaurant = context.RestaurantOwners.Include(r => r.Restaurant).Include(o=>o.Owner).SingleOrDefault(p => p.OwnerId == ownId && p.RestaurantId==restId);
            return restaurant;
        }
        public static List<RestaurantEmployees> RestaurantsByEmployeeId(int empId)
        {
            var context = new TableReadyContext();
            var restaurants = context.RestaurantEmployees.Include(r => r.Restaurant).Include(e => e.Employee).Where(p => p.EmployeeId == empId).ToList();
            return restaurants;
        }
        public static RestaurantEmployees RestaurantByEmployeeId(int restId, int empId)
        {
            var context = new TableReadyContext();
            var restaurant = context.RestaurantEmployees.Include(r => r.Restaurant).SingleOrDefault(p => p.EmployeeId == empId && p.RestaurantId == restId);
            return restaurant;
        }
        public static Restaurants GetRestauranByID(int restId)
        {
            var context = new TableReadyContext();
            var restaurant = context.Restaurants.Include(r => r.RestaurantOwners).Include(p => p.RestaurantEmployees).SingleOrDefault(p => p.RestaurantId == restId);
            return restaurant;
        }
        public static void EditRestaurant(Restaurants restaurant)
        {
            var context = new TableReadyContext();
            var originalRestaurant = context.Restaurants.SingleOrDefault(p => p.RestaurantId == restaurant.RestaurantId);
            originalRestaurant.RestaurantName = restaurant.RestaurantName;
            context.SaveChanges();
        }
        public static List<RestaurantOwners> GetRestauranOwnersByID(int restId)
        {
            var context = new TableReadyContext();
            var restaurantOwners = context.RestaurantOwners.Include(j => j.Owner).Where(p => p.RestaurantId == restId).ToList();
            return restaurantOwners;
        }
        public static List<RestaurantEmployees> GetRestauranEmployeesByID(int restId)
        {
            var context = new TableReadyContext();
            var restaurantEmployees = context.RestaurantEmployees.Include(j => j.Employee).Where(p => p.RestaurantId == restId).ToList();
            return restaurantEmployees;
        }
        public static void UpdateRestaurantOwner(RestaurantOwners restaurantOwner)
        {
            var context = new TableReadyContext();
            var originalRestaurantOwner = context.RestaurantOwners.SingleOrDefault(p => p.RestaurantId==restaurantOwner.RestaurantId && p.OwnerId== restaurantOwner.OwnerId);
            originalRestaurantOwner.Request = restaurantOwner.Request;
            originalRestaurantOwner.RequestStatus = restaurantOwner.RequestStatus;
            originalRestaurantOwner.StartDate = restaurantOwner.StartDate;
            originalRestaurantOwner.EndDate = restaurantOwner.EndDate;
            originalRestaurantOwner.Active = restaurantOwner.Active;
            originalRestaurantOwner.Status = restaurantOwner.Status;
            context.SaveChanges();
        }
        public static void UpdateRestaurantEmployee(RestaurantEmployees restaurantEmployee)
        {
            var context = new TableReadyContext();
            var originalRestaurantEmployee = context.RestaurantEmployees.SingleOrDefault(p => p.RestaurantId == restaurantEmployee.RestaurantId && p.EmployeeId == restaurantEmployee.EmployeeId);
            originalRestaurantEmployee.NewRequestFlag = restaurantEmployee.NewRequestFlag;
            originalRestaurantEmployee.RequestStatus = restaurantEmployee.RequestStatus;
            originalRestaurantEmployee.StartDate = restaurantEmployee.StartDate;
            originalRestaurantEmployee.EndDate = restaurantEmployee.EndDate;
            originalRestaurantEmployee.Active = restaurantEmployee.Active;
            originalRestaurantEmployee.Status = restaurantEmployee.Status;
            context.SaveChanges();
        }
    }
}
