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
    //Business Layer to Manage Owner
    public class OwnersManager
    {
        public static int GetOwnerIdByUserId(int userId)
        {
            var context = new TableReadyContext();
            var own = context.Owners.SingleOrDefault(ow => ow.UserId == userId);
            if (own == null)
                return 0;
            else return own.OwnerId;
        }

        public static int GetUserIdByOwnerId(int Id)
        {
            var context = new TableReadyContext();
            var own = context.Owners.SingleOrDefault(ow => ow.OwnerId == Id);
            return own.UserId;
        }
        public static List<RestaurantOwners> GetAllRestaurantsByOwnerID(int ownId)
        {
            var context = new TableReadyContext();
            var restaurants = context.RestaurantOwners.Include(r => r.Restaurant).Include(j => j.Owner).Where(p => p.OwnerId == ownId).ToList();
            return restaurants;
        }
        public static void CreateOwner(Owners owner)
        {
            var context = new TableReadyContext();
            context.Owners.Add(owner);
            context.SaveChanges();
        }
        public static Owners GetOwnerByID(int ownerId)
        {
            var context = new TableReadyContext();
            var owner = context.Owners.Include(p => p.User).Include(ro=>ro.RestaurantOwners).SingleOrDefault(o => o.OwnerId == ownerId);
            return owner;
        }
    }
}
