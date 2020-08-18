//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using TableReady.Core.Data.Domain;

namespace TableReady.Core.BLL
{
    public class UsersManager
    {
        //Business Layer to Manage User
        public static List<Users> GetAll()
        {
            var context = new TableReadyContext();
            var users = context.Users.Include(p => p.Authentication).Include(p => p.Authentication.AuthenticationMatrix).ToList();
            return users;
        }
        public static Users Authenticate(string username, string password)
        {
            var user = GetAll().SingleOrDefault(usr => usr.Authentication.Username == username && usr.Authentication.Password == password);
            return user;//this will be either null or an object
        }
        public static int CreateAuthentication(Authentication auth)

        {
            var context = new TableReadyContext();
            var authGroup = context.Authentication;
            var existAuth = authGroup.SingleOrDefault(p => p.Username == auth.Username);
            if (existAuth == null)
            {
                var maxAuthBefore = authGroup.SingleOrDefault(p => p.Id == authGroup.Max(k => k.Id));
                context.Authentication.Add(auth);
                context.SaveChanges();
                var maxAuthAfter = authGroup.SingleOrDefault(p => p.Id == authGroup.Max(k => k.Id));

                if (maxAuthAfter.Id == maxAuthBefore.Id + 1)
                {
                    return maxAuthAfter.Id;
                }
                else
                {
                    return 0;
                }
            }
            else return 0;
        }
        public static bool UpdateAuthentication(Authentication auth)
        {
            var context = new TableReadyContext();
            var authGroup = context.Authentication;
            var existAuth = authGroup.SingleOrDefault(p => p.Username == auth.Username);
            if (existAuth == null || existAuth.Id == auth.Id)
            {
                var originalAuth = authGroup.SingleOrDefault(p => p.Id == auth.Id);
                originalAuth.Password = auth.Password;
                originalAuth.Username = auth.Username;
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public static void CreateUser(Users user)
        {
            var context = new TableReadyContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
        public static void UpdateUser(Users user)
        {
            var context = new TableReadyContext();
            var originalUser = context.Users.SingleOrDefault(p => p.UserId == user.UserId);
            originalUser.LastName = user.LastName;
            originalUser.FirstName = user.FirstName;
            originalUser.Email = user.Email;
            originalUser.Country = user.Country;
            originalUser.City = user.City;
            originalUser.Province = user.Province;
            originalUser.Phone = user.Phone;
            originalUser.Address = user.Address;
            context.SaveChanges();
        }
        public static int GetUserIdByAuthId(int idAuth)
        {
            var context = new TableReadyContext();
            var user = context.Users.Include(p => p.Authentication).SingleOrDefault(p => p.AuthenticationId == idAuth);
            return user.UserId;
        }
        public static Users GetUserByUserId(int userId)
        {
            var context = new TableReadyContext();
            var user = context.Users.Include(p => p.Authentication).SingleOrDefault(p => p.UserId == userId);
            return user;
        }
        public static void AddOwnerToAuthetication(AuthenticationMatrix authMatrix)
        {
            var context = new TableReadyContext();
            context.AuthenticationMatrix.Add(authMatrix);
            context.SaveChanges();
        }
        public static void AddEmployeeToAutheticationMatrix(AuthenticationMatrix authMatrix)
        {
            var context = new TableReadyContext();
            context.AuthenticationMatrix.Add(authMatrix);
            context.SaveChanges();
        }
        public static AuthenticationMatrix GetAuthenticationMatrixByIDs(int restId, int authId)
        {
            var context = new TableReadyContext();
            var authRestaurant = context.AuthenticationMatrix.SingleOrDefault(p => p.AuthenticationId == authId && p.RestaurantId == restId);
            return authRestaurant;
        }
        public static void UpdateAuthenticationMatrixByIDs(AuthenticationMatrix restAuth)
        {
            var context = new TableReadyContext();
            var originalAuthRestaurant = context.AuthenticationMatrix.SingleOrDefault(p => p.AuthenticationId == restAuth.AuthenticationId && p.RestaurantId == restAuth.RestaurantId);
            originalAuthRestaurant.Role = restAuth.Role;
            context.SaveChanges();
        }
        public static int GetAuthIdByUserId(int userId)
        {
            var context = new TableReadyContext();
            var user = context.Users.SingleOrDefault(p => p.UserId == userId);
            return user.AuthenticationId;
        }
        public static void DeleteAuthMatrixByIds(int restId, int authId)
        {
            var context = new TableReadyContext();
            var authorizationMatrix = context.AuthenticationMatrix.SingleOrDefault(p => p.AuthenticationId == authId && p.RestaurantId == restId);
            context.AuthenticationMatrix.Remove(authorizationMatrix);
            context.SaveChanges();
        }
    }
}
