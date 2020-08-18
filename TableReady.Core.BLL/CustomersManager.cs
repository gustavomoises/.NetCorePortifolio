//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableReady.Core.Data.Domain;

namespace TableReady.Core.BLL
{
    //Business Layer to Manage Customer
    public class CustomersManager
    {
        public static int GetUserIdByCustomerId(int Id)
        {
            var context = new TableReadyContext();
            var emp = context.Customers.SingleOrDefault(emp => emp.CustomerId == Id);
            return emp.UserId;
        }
        public static Customers GetCustomerByCustomerId(int custId)
        {
            var context = new TableReadyContext();
            var customer = context.Customers.Include(p => p.User).SingleOrDefault(p => p.CustomerId == custId);
            return customer;
        }
        public static List<WaitlistEntry> GetActiveWaitlist(int custId)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == custId && p.WaitlistStatus=="active").ToList();
            return waitlist;
        }
        public static List<WaitlistEntry> GetWaitlistByCustomerID(int customerId)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == customerId).ToList();

            return waitlist;
        }
        
        public static int GetCustomerIdByUserId(int userId)
        {
            var context = new TableReadyContext();
            var cust = context.Customers.SingleOrDefault(p => p.UserId == userId);
            if (cust == null)
                return 0;
            else return cust.CustomerId;
        }
        public static List<ReservationEntry> GetReservations(int custId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == custId && (p.ReservationStatus == "confirmed" || p.ReservationStatus == "on Hold" )).ToList();
            return reservations;
        }
        public static void CreateCustomer(Customers customer)
        {
            var context = new TableReadyContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        public static List<ReservationEntry> GetAllReservations(int custId)
        {
            var context = new TableReadyContext();
            var reservationlist = context.ReservationEntry.Include(r => r.Restaurant.RestaurantName).Where(p => p.CustomerId == custId).ToList();
            return reservationlist;
        }
        public static List<WaitlistEntry> GetAllWaitlist(int custId)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant.RestaurantName).Where(p => p.CustomerId == custId).ToList();
            return waitlist;
        }
        public static List<WaitlistEntry> GetNotActiveWaitlist(int custId)
        {
            var context = new TableReadyContext();
            var waitlist = context.WaitlistEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == custId && p.WaitlistStatus != "active").ToList();
            return waitlist;
        }
        public static List<ReservationEntry> GetReservationsByCustomerID(int customerId)
        {
            var context = new TableReadyContext();
            var reservations = context.ReservationEntry.Include(r => r.Restaurant).Where(p => p.CustomerId == customerId).ToList();
            return reservations;
        }
        public static void AddWaitlist(WaitlistEntry waitlist)
        {
            var context = new TableReadyContext();
            context.WaitlistEntry.Add(waitlist);
            context.SaveChanges();
        }
        public static void AddReservation(ReservationEntry reservation)
        {
            var context = new TableReadyContext();
            context.ReservationEntry.Add(reservation);
            context.SaveChanges();
        }
        public static void UpdateWaitlist(WaitlistEntry waitlist)
        {
            var context = new TableReadyContext();
            var originalWaitlist = context.WaitlistEntry.SingleOrDefault(we => we.WaitlistEntryId == waitlist.WaitlistEntryId);
            originalWaitlist.EntryOrigin = waitlist.EntryOrigin;
            originalWaitlist.PartySize = waitlist.PartySize;
            originalWaitlist.WaitlistStatus = waitlist.WaitlistStatus;
            context.SaveChanges();
        }
        public static void UpdateReservation(ReservationEntry reservation)
        {
            var context = new TableReadyContext();
            var originalReservation = context.ReservationEntry.SingleOrDefault(re => re.ReservationId == reservation.ReservationId);
            originalReservation.EntryOrigin = reservation.EntryOrigin;
            originalReservation.PartySize = reservation.PartySize;
            originalReservation.ReservationStatus = reservation.ReservationStatus;
            context.SaveChanges();
        }
    }
}

