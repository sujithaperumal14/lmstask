using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;  
using System.Configuration;  
using System.Data;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class LoginController:Controller{
    private readonly UserDetailDbContext db;
        private readonly IWebHostEnvironment _environment;
        public LoginController(UserDetailDbContext context, IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }
         
          public IActionResult Index()
    {
       
        return View();
    }

    [HttpGet]
    public IActionResult AdminLogin()
    {
       
        return View();
    }
    
    [HttpPost]
     public IActionResult AdminLogin(Login login)
    {
       string result= Database.AdminLogin(login);
       Console.WriteLine(result);
       if(result=="success")
       {
          HttpContext.Session.SetString("EmailID", login.EmailID);
                if (login.RememberMe)
      {
          var option = new CookieOptions
          {
              Expires = DateTime.UtcNow.AddMonths(1), // Expires after 1 month
              HttpOnly = true, // Cannot be accessed by JavaScript
              SameSite = SameSiteMode.Strict // Protects against cross-site request forgery attacks
          };
          
          Response.Cookies.Append("EmailID", login.EmailID, option);
      }
          return RedirectToAction("AdminHomePage","Admin");
       }
      
       return RedirectToAction("AdminLogin","Login");
        
    }
    public IActionResult AdminRegister(Register register)
    {
           
                bool IsInserted= false;
                if(ModelState.IsValid){
             
                    IsInserted=Database.AdminRegister(register);
                    if(IsInserted){
                        ViewBag.Message="Inserted successfully";
                        return View("AdminLogin");
                    }
                    else{
                        TempData["Error"]="Not inserted";
                    }
                }
                return View();
                  
          //HttpContext.Session.SetString("EmailID ", register.EmailID);
          
       }

       [HttpGet]
    public IActionResult UserLogin()
    {
       
        return View();
    }
    
    [HttpPost]
     public IActionResult UserLogin(Login login)
    {
       string result= Database.UserLogin(login);
       Console.WriteLine(result);
       if(result=="success")
       {
          HttpContext.Session.SetString("EmailID", login.EmailID);
          return RedirectToAction("UserHomePage","User");
       }
      
       return RedirectToAction("UserLogin","Login");
        
    }
    [HttpGet]
    public IActionResult Details(UserDetail userDetail)
    {
        var email = HttpContext.Session.GetString("EmailID");
          var u = from s in db.UserDetails
                     where s.EmailID == email
                     select s;
        return View(u);
    }

    [HttpGet]

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)

        {



            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;


            var email = HttpContext.Session.GetString("EmailID");
          var li = from s in db.UserDetails
                     where s.EmailID == email
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.Name.Contains(searchString)
                                       || s.EmailID.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.Name);
                    break;
                default:
                    li = li.OrderBy(s => s.RegisterNo);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<UserDetail>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
}

            
                        /*if(modelLogin.EmailID==EmailID && modelLogin.Password==Password){
                            List<Claim> claims=new List<Claim>(){
                                new Claim(ClaimTypes.NameIdentifier,EmailID),
                                new Claim("OtherProperties","Example Role")
                            };
                            ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties=new AuthenticationProperties(){
                                AllowRefresh=true,
                                IsPersistent=modelLogin.KeepLoggedIn
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),properties);
                            return RedirectToAction("Index","Home");
                        }
                        ViewData["ValidateMessage"]="User not found";*/
    

            
    
        /*[HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminRegister(AdminLogin adminLogin)
        {
                 List<AdminLogin> adminLogins = new List<AdminLogin>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into users (FirstName,LastName,EmailID,Password,ConfirmPassword) Values ('{adminLogin.FirstName}','{adminLogin.LastName}', '{adminLogin.EmailID}','{adminLogin.Password}','{adminLogin.ConfirmPassword}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = "Success";
            return View();
        }*/




/* var isEmailAlreadyExists = db.User.Any(x => x.Email == obj.Email);
        if(isEmailAlreadyExists)
        {
            ModelState.AddModelError("Email", "User with this email already exists");
            return View(obj)
        }*/