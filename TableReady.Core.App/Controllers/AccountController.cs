//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TableReady.Core.Data.Domain;
using TableReady.Core.BLL;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Authentication;
using TableReady.Core.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authorization;

namespace TableReady.Core.App.Controllers
{
    //Controller to manage User Account
    public class AccountController : Controller
    {
        private readonly object Lock = new object();
        //Call Viewcomponent ShowRestaurantID
        public IActionResult ShowRestaurantID(int id)
        {
            if (id == 0)
            {
                return Content("");
            }
            else
            {
                return ViewComponent("ShowRestaurantID");
            }
        }
        //User Registration
        public IActionResult Register ()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage= TempData["ErrorMessage"];
                var viewRegister = new RegisterModelView();
                if (TempData["FName"] != null)
                        viewRegister.FirstName = (string)TempData["FName"];
                if (TempData["LName"] != null)
                    viewRegister.LastName = (string)TempData["LName"];
                if (TempData["Address"] != null)
                    viewRegister.Address= (string)TempData["Address"];
                if (TempData["City"] != null)
                    viewRegister.City = (string)TempData["City"];
                if (TempData["Province"] != null)
                    viewRegister.Province = (string)TempData["Province"];
                if (TempData["Country"] != null)
                    viewRegister.Country = (string)TempData["Country"];
                if (TempData["Email"] != null)
                    viewRegister.Email = (string)TempData["Email"];
                if (TempData["Phone"] != null)
                    viewRegister.Phone = (string)TempData["Phone"];
                if (TempData["UserName"] != null)
                    viewRegister.Username = (string)TempData["UserName"];

                return View(viewRegister);
            }
            else
            {
                return View();
            }
        }
        //Post for User Registration
        [HttpPost]
        public IActionResult Register(RegisterModelView viewRegistration)
        {
            TempData["FName"] = viewRegistration.FirstName;
            TempData["LName"] = viewRegistration.LastName;
            TempData["Email"] = viewRegistration.Email;
            TempData["UserName"] = viewRegistration.Username;
            TempData["Address"] = viewRegistration.Address;
            TempData["City"] = viewRegistration.City;
            TempData["Province"] = viewRegistration.Province;
            TempData["Country"] = viewRegistration.Country;
            TempData["Phone"] = viewRegistration.Phone;
            if (viewRegistration.FirstName=="" || viewRegistration.FirstName == null)
            {
                TempData["ErrorMessage"] = "First Name is required.";
                return RedirectToAction("Register", "Account");
            }
            else
            {
                if (viewRegistration.LastName == ""|| viewRegistration.LastName ==null)
                {
                    TempData["ErrorMessage"] = "Last Name is required.";
                    return RedirectToAction("Register", "Account");
                }
                else
                {
                    if (viewRegistration.Email == ""|| viewRegistration.Email ==null ) 
                    {
                        TempData["ErrorMessage"] = "Email is required.";
                        return RedirectToAction("Register", "Account");
                    }
                    else
                    {
                        if (viewRegistration.Username == ""|| viewRegistration.Username == null)
                        {
                            TempData["ErrorMessage"] = "Usernamel is required.";
                            return RedirectToAction("Register", "Account");
                        }
                        else
                        {
                            if (viewRegistration.Password == ""|| viewRegistration.Password ==null )
                            {
                                TempData["ErrorMessage"] = "Passord is required.";
                                return RedirectToAction("Register", "Account");
                            }
                            else 
                            {
                                int id;
                                var auth = new Authentication
                                {
                                    Username = viewRegistration.Username,
                                    Password = viewRegistration.Password
                                };
                                lock (Lock)
                                {
                                    id = UsersManager.CreateAuthentication(auth);
                                }
                                if (id > 0)
                                {
                                    var user = new Users
                                    {
                                        Address = viewRegistration.Address,
                                        AuthenticationId = id,
                                        City = viewRegistration.City,
                                        Country = viewRegistration.Country,
                                        Email = viewRegistration.Email,
                                        Province = viewRegistration.Province,
                                        Phone = viewRegistration.Phone,
                                        LastName = viewRegistration.LastName,
                                        FirstName = viewRegistration.FirstName
                                    };
                                    UsersManager.CreateUser(user);
                                    int UserId = UsersManager.GetUserIdByAuthId(id);
                                    var customer = new Customers
                                    {
                                        UserId = UserId
                                    };
                                    CustomersManager.CreateCustomer(customer);
                                    TempData["Message"] = "Regitration completed! Login to Access the TableReady portal.";
                                    TempData["ErrorMessage"] = null;
                                    return RedirectToAction("Login", "Account");
                                }

                                else
                                {
                                    TempData["Message"] = null;
                                    TempData["ErrorMessage"] = "USERNAME already exist.Try another one.";
                                    return RedirectToAction(nameof(Register));
                                }
                            }
                        }
                    }
                }
            }
        }
        //User Profile
        [Authorize]
        public IActionResult Profile()
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
            var userId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "UserID").Value);
            var authId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "AuthID").Value);
            var ownId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "OwnerID").Value);
            var empId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "EmployeeID").Value);
            var user = UsersManager.GetUserByUserId(userId);
            var viewUser = new RegisterModelView
            {
                UserId= userId,
                AuthId= authId,
                Address = user.Address,
                City = user.City,
                Province = user.Province,
                Country = user.Country,
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username=user.Authentication.Username
            };
            ViewBag.Employee = empId;
            ViewBag.Owner = ownId;
            return View(viewUser);
        }
        //Edit User Profile
        [Authorize]
        public IActionResult EditProfile()
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
            var userId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "UserID").Value);
            var user = UsersManager.GetUserByUserId(userId);
            var viewUser = new RegisterModelView
            {
                UserId = user.UserId,
                Address = user.Address,
                City = user.City,
                Province = user.Province,
                Country = user.Country,
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Authentication.Username,
                AuthId=user.AuthenticationId,
            };
            return View(viewUser);
        }
        //[Post] Edit Profile
        [Authorize]
        [HttpPost]
        public IActionResult EditProfile(RegisterModelView viewUser)
        {
            LogRestaurant();
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var authId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "AuthID").Value);
            string id = Convert.ToString(viewUser.UserId);

            if (viewUser.FirstName == "" || viewUser.FirstName == null)
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "First Name is required.";
                return RedirectToAction("EditProfile", new { id = viewUser.UserId });
            }
            else
            {
                if (viewUser.LastName == "" || viewUser.LastName == null)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Last Name is required.";
                    return RedirectToAction("EditProfile",new { id = viewUser.UserId });
                }
                else
                {
                    if (viewUser.Email == "" || viewUser.Email == null)
                    {
                        TempData["Message"] = null;
                        TempData["ErrorMessage"] = "Email is required.";
                        return RedirectToAction("EditProfile", new { id = viewUser.UserId });
                    }
                    else
                    {
                        if (viewUser.Username == "" || viewUser.Username == null)
                        {
                            TempData["Message"] = null;
                            TempData["ErrorMessage"] = "Username is required.";
                            return RedirectToAction("EditProfile", new { id = viewUser.UserId });
                        }
                        else
                        {
                            if (viewUser.Username == "" || viewUser.Password == null)
                            {
                                TempData["Message"] = null;
                                TempData["ErrorMessage"] = "Passord is required.";
                                return RedirectToAction("EditProfile", new { id = viewUser.UserId });
                            }
                            else
                            {
                                var user = new Users
                                {
                                    UserId = viewUser.UserId,
                                    Address = viewUser.Address,
                                    City = viewUser.City,
                                    Province = viewUser.Province,
                                    Country = viewUser.Country,
                                    Phone = viewUser.Phone,
                                    Email = viewUser.Email,
                                    FirstName = viewUser.FirstName,
                                    LastName = viewUser.LastName,
                                    AuthenticationId = authId
                                };
                                UsersManager.UpdateUser(user);
                                var auth = new Authentication
                                {
                                    Username = viewUser.Username,
                                    Password = viewUser.Password,
                                    Id = authId
                                };
                                bool username = UsersManager.UpdateAuthentication(auth);
                                if (!username)
                                {
                                    TempData["Message"] = null;
                                    TempData["ErrorMessage"] = "Sorry!!! Username is already taken. Choose another one.";
                                    return RedirectToAction("EditProfile", new { id = viewUser.UserId });
                                }
                                else
                                {
                                    TempData["Message"] = "Your profile was Updated!!!";
                                    TempData["ErrorMessage"] = null;
                                    return RedirectToAction("Profile", "Account");
                                }
                            }
                        }
                    }
                }
            }
        }
        //Owner List ViewComponent
        [Authorize]
        public IActionResult OwnerList(int id)
        {
            LogRestaurant();
            return ViewComponent("OwnerList",id);
        }
        //Owner List ViewComponent
        [Authorize]
        public IActionResult EmployeeList(int id)
        {
            LogRestaurant();
            return ViewComponent("EmployeeList",id);
        }

        //User Login
        public IActionResult Login(string returnUrl = null)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            if (returnUrl != null)
                TempData["returnUrl"] = returnUrl;
            ViewBag.UserTypes = BasicGetUsers();
            ViewBag.Restaurants = BasicGetRestaurants();
            return View();
        }
        //[Post]User Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(RestaurantLoginModelView authView)
        {
            TempData["RestaurantName"] = null;
            if (authView.Type == 0)
            {
                var user = UsersManager.Authenticate(authView.Username, authView.Password);
                if (user == null)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Login Error!!!.Try Again. Don't forget to Register first!!";
                    return RedirectToAction("Login", "Account");
                }
                var custId = CustomersManager.GetCustomerIdByUserId(user.UserId);
                var empId = EmployeesManager.GetEmployeeIdByUserId(user.UserId);
                var ownId = OwnersManager.GetOwnerIdByUserId(user.UserId);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Authentication.Username),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim("AuthID", user.AuthenticationId.ToString()),
                    new Claim("UserID", user.UserId.ToString()),
                    new Claim("CustomerID", custId.ToString()),
                    new Claim("EmployeeID", empId.ToString()),
                    new Claim("OwnerID", ownId.ToString()),
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
            }
            else
            {
                var user = RestaurantsManager.AuthenticateRestaurant(authView.Username, authView.Password, authView.RestaurantId);
                if (user.UserId <= 0)
                {
                    TempData["Message"] = null;
                    TempData["ErrorMessage"] = "Login Error!!!.Try Again.Don't forget to Register first!!";
                    return RedirectToAction("Login", "Account");
                }
                var custId = CustomersManager.GetCustomerIdByUserId(user.UserId);
                var empId = EmployeesManager.GetEmployeeIdByUserId(user.UserId);
                var ownId = OwnersManager.GetOwnerIdByUserId(user.UserId);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Authentication.Username),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim("AuthID", user.AuthenticationId.ToString()),
                    new Claim("UserID", user.UserId.ToString()),
                    new Claim("CustomerID", custId.ToString()),
                    new Claim("EmployeeID", empId.ToString()),
                    new Claim("OwnerID", ownId.ToString()),
                };
                if (user.Authentication.AuthenticationMatrix.Count() > 0)
                {
                    foreach (AuthenticationMatrix a in user.Authentication.AuthenticationMatrix)
                    {
                        if (a.RestaurantId == authView.RestaurantId)
                        {
                            claims.Add(new Claim("RestaurantID", a.RestaurantId.ToString()));
                            claims.Add(new Claim(ClaimTypes.Role, a.Role));
                            TempData["RestaurantName"] = RestaurantsManager.GetRestaurantIdByNameByRestauranID(a.RestaurantId);
                        }
                    }
                }
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
            }
            if (TempData["returnUrl"] == null)
            {
                return Redirect("~/Account/Profile");
            }
            else
            {
                return Redirect(TempData["returnUrl"].ToString());
            }
        }
        //User Logout
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            if(TempData["Message"]!=null)
                return RedirectToAction("Login", "Account");
            else return RedirectToAction("Index", "Home");
        }
        //Acess Denied Page
        public IActionResult AccessDenied()
        {
            LogRestaurant();
            return View();
        }
        //Create a new Restaurant
        [Authorize]
        public IActionResult RestaurantCreate ()
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
            return View();
        }
        //[Post]Create a new Restaurant
        [Authorize]
        [HttpPost]
        public IActionResult RestaurantCreate(RestaurantCreateModelView viewRestaurant)
        {
            LogRestaurant();
            bool newOwner = false;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var userId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "UserID").Value);
            var ownId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "OwnerID").Value);
            var authId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "AuthID").Value);
            var newRestaurant = RestaurantsManager.GetRestaurantIdByName(viewRestaurant.RestaurantName);
            if (newRestaurant==0)
            {
                var restaurant = new Restaurants
                {
                    RestaurantName = viewRestaurant.RestaurantName,
                };
                if (ownId == 0)
                {
                    var owner = new Owners { UserId = userId };
                    OwnersManager.CreateOwner(owner);
                    ownId = OwnersManager.GetOwnerIdByUserId(userId);
                    newOwner = true;
                }
                int restId = RestaurantsManager.CreateRestaurant(restaurant);
                var restaurantOwner = new RestaurantOwners
                {
                    RestaurantId = restId,
                    OwnerId = ownId,
                    Status = "Primary Owner",
                    Active = true,
                    Request = false,
                    RequestStatus = "Accepted",
                };
                RestaurantsManager.AddOwnerToRestaurant(restaurantOwner);
                var authMatrix = new AuthenticationMatrix
                {
                    AuthenticationId = authId,
                    RestaurantId = restId,
                    Role = "Owner"
                };
                UsersManager.AddOwnerToAuthetication(authMatrix);
                if (newOwner)
                {
                    TempData["Message"] = "You successfully inserted a restaurant in the system. You need to Login again to upgrade your new credential!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Logout", "Account");
                }
                else
                {
                    TempData["Message"] = "You successfully inserted a restaurant!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Profile", "Account");
                }
            }
            else
            {
                TempData["Message"] =null;
                TempData["ErrorMessage"] = "Sorry!! The Restaurant's name is already registered. Choose another Restaurant's Name.";
                return RedirectToAction("RestaurantCreate", "Account");
            }
        }
        // Ownership Application
        [Authorize]
        public IActionResult OwnerApply()
        {
            LogRestaurant();
            ViewBag.Restaurants = BasicGetRestaurants();
            return View();
        }
        // [Post]Ownership Application
        [Authorize]
        [HttpPost]
        public IActionResult OwnerApply(OwnerModelView viewOwner)
        {
            LogRestaurant();
            bool newOwner = false;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var userId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "UserID").Value);
            var ownId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "OwnerID").Value);
            if (ownId == 0)
            {
                var owner = new Owners { UserId = userId };
                OwnersManager.CreateOwner(owner);
                ownId = OwnersManager.GetOwnerIdByUserId(userId);
                newOwner = true;
            }
            var restaurants = RestaurantsManager.RestaurantsByOwnerId(ownId);
            bool newRestaurant = true;
            bool requestFlag=true;
            string status;
            string request;
            foreach (RestaurantOwners rest in restaurants)
            {
                if (viewOwner.RestaurantId == rest.RestaurantId)
                {
                    newRestaurant = false;
                    status = rest.Status;
                    request = rest.RequestStatus;
                    requestFlag = (bool)rest.Request;                    
                }
            }
            if (newRestaurant)
            {
                var restaurantOwner = new RestaurantOwners
                {
                    OwnerId = ownId,
                    RestaurantId = viewOwner.RestaurantId,
                    RequestStatus = "on Hold",
                    Request=true,
                    Status="Applicant",
                    Active=false,
                };
                RestaurantsManager.AddOwnerToRestaurant(restaurantOwner);
                if (newOwner)
                {
                    TempData["Message"] = "You successfully applied for an Onwership. You need to Login again to upgrade your new credential!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Logout", "Account");
                }
                else
                {
                    TempData["Message"] = "You successfully applied for an Onwership!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Profile", "Account");
                }
            }
            else
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!! Your already applied to the Restaurant's ownership or you are one of the  Restaurant's owners.";
                return RedirectToAction("Profile", "Account");
            }
        }
        //Show Owner Details
        [Authorize]
        public IActionResult OwnerDetails(int id)
        {
            LogRestaurant();
            int restId = id;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var ownId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "OwnerID").Value);
            var ownerRestaurant=RestaurantsManager.RestaurantByOwnerId(restId,ownId);
            var viewOwnerRestaurant = new OwnerModelView 
            { 
                Restaurant= ownerRestaurant.Restaurant.RestaurantName,
                RestaurantId= ownerRestaurant.RestaurantId,
                OwnerId= ownerRestaurant.OwnerId,
                Status= ownerRestaurant.Status,
                StartDate= (DateTime)ownerRestaurant.StartDate,
                EndDate= ownerRestaurant.EndDate,
                Active= ownerRestaurant.Active,
                RequestFlag= ownerRestaurant.Request,
                RequestStatus= ownerRestaurant.RequestStatus
            };
            return View(viewOwnerRestaurant);
        }
        //Employee Application
        [Authorize]
        public IActionResult EmployeeApply()
        {
            LogRestaurant();
            ViewBag.Restaurants = BasicGetRestaurants();
            return View();
        }
        //[Post]Employee Application
        [Authorize]
        [HttpPost]
        public IActionResult EmployeeApply(EmployeeModelView viewEmployee)
        {
            LogRestaurant();
            bool newEmployee = false;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var userId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "UserID").Value);
            var empId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "EmployeeID").Value);
            if (empId == 0)
            {
                var employee = new Employees { UserId = userId };
                EmployeesManager.CreateEmployee(employee);
                empId = EmployeesManager.GetEmployeeIdByUserId(userId);
                newEmployee = true;
            }
            var restaurants = RestaurantsManager.RestaurantsByEmployeeId(empId);
            bool newRestaurant = true;
            bool requestFlag = true;
            string status;
            string request;
            foreach (RestaurantEmployees rest in restaurants)
            {
                if (viewEmployee.RestaurantId == rest.RestaurantId)
                {
                    newRestaurant = false;
                    status = rest.Status;
                    request = rest.RequestStatus;
                    requestFlag = (bool)rest.NewRequestFlag;
                }
            }
            if (newRestaurant )
            {
                var restaurantEmployee = new RestaurantEmployees
                {
                    EmployeeId = empId,
                    RestaurantId = viewEmployee.RestaurantId,
                    RequestStatus = "on Hold",
                    NewRequestFlag = true,
                    Status = "Applicant",
                    Active = false,
                };
                RestaurantsManager.AddEmployeeToRestaurant(restaurantEmployee);
                if (newEmployee)
                {
                    TempData["Message"] = "You successfully applied for a position in a Restaurant. You need to Login again to upgrade your new credential!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Logout", "Account");
                }
                else
                {
                    TempData["Message"] = "You successfully applied for a position in a Restaurant!!";
                    TempData["ErrorMessage"] = null;
                    return RedirectToAction("Profile", "Account");
                }
            }
            else
            {
                TempData["Message"] = null;
                TempData["ErrorMessage"] = "Sorry!! Your already applied to the Restaurant's position";
                return RedirectToAction("Profile", "Account");
            }
        }
        //Show Employee Details
        [Authorize]
        public IActionResult EmployeeDetails(int id)
        {
            LogRestaurant();
            int restId = id;
            ClaimsPrincipal cp = this.User;
            var claims = cp.Claims.ToList();
            var empId = Convert.ToInt32(claims.SingleOrDefault(p => p.Type == "EmployeeID").Value);

            var employeeRestaurant=RestaurantsManager.RestaurantByEmployeeId(restId,empId);
            var viewEmployeeRestaurant = new EmployeeModelView 
            { 
                Restaurant= employeeRestaurant.Restaurant.RestaurantName,
                RestaurantId= employeeRestaurant.RestaurantId,
                EmployeeId= employeeRestaurant.EmployeeId,
                Status= employeeRestaurant.Status,
                StartDate= (DateTime)employeeRestaurant.StartDate,
                EndDate= employeeRestaurant.EndDate,
                Active= employeeRestaurant.Active,
                RequestFlag= employeeRestaurant.NewRequestFlag,
                RequestStatus= employeeRestaurant.RequestStatus
            };
            return View(viewEmployeeRestaurant);
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
        //List of type of Users
        protected IEnumerable BasicGetUsers()
        {
            List<SelectListItem> userTypes = new List<SelectListItem>();
            userTypes.Add(new SelectListItem
            {
                Text = "Customer",
                Value = "0"
            });
            userTypes.Add(new SelectListItem
            {
                Text = "Restaurant Staff",
                Value = "1"
            });
            var styles = new SelectList(userTypes, "Value", "Text");
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
