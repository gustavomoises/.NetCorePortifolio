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
    public class EmployeesManager
    {
        //Business Layer to Manage Employee
        public static int GetUserIdByEmployeeId(int employeeId)
        {
            var context = new TableReadyContext();
            var emp = context.Employees.SingleOrDefault(emp => emp.EmployeeId == employeeId);
            return emp.UserId;
        }
        public static int GetEmployeeIdByUserId(int userId)
        {
            var context = new TableReadyContext();
            var emp = context.Employees.SingleOrDefault(em => em.UserId == userId);
            if (emp == null)
                return 0;
            else return emp.EmployeeId;
        }
        public static List<RestaurantEmployees> GetAllRestaurantsByEmpID(int empId)
        {
            var context = new TableReadyContext();
            var restaurants = context.RestaurantEmployees.Include(r => r.Restaurant).Include(j=>j.Employee).Where(p => p.EmployeeId == empId).ToList();
            return restaurants;
        }
        public static void CreateEmployee(Employees employee)
        {
            var context = new TableReadyContext();
            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}
