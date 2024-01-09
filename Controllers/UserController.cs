using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Controllers;

public class UserController : Controller
{
    private readonly UserDetailDbContext db;
    private readonly BorrowBookDbContext DB;
    private readonly IssueBookDbContext db1;
    private readonly BookDbContext dB;
    private readonly BorrowDbContext db2;
    private readonly IWebHostEnvironment _environment;
    public UserController(IssueBookDbContext context3,BorrowDbContext context2,BorrowBookDbContext context1,BookDbContext Context,UserDetailDbContext context, IWebHostEnvironment environment)
    {
        DB= context1;
        db = context;
        dB = Context;
        db2=context2;
        db1=context3;
        _environment = environment;
    }


    [HttpGet]

    public IActionResult UserHomePage()
    {
        return View();
    }

    [HttpGet]

    public async Task<IActionResult> Indexs()

    {

        var email = HttpContext.Session.GetString("EmailID");
        UserDetail u = await db.UserDetails.Where(x => x.EmailID == email).FirstOrDefaultAsync();
        //   var li = from s in db.UserDetails
        //              where s.EmailID == email
        //              select s;

        return View(u);
        //  return View(await li.AsNoTracking().ToListAsync());
    }

  

public async Task<IActionResult> BorrowBook(int BookID)
{
    // Retrieve the current user
    var email = HttpContext.Session.GetString("EmailID");
    UserDetail user = await db.UserDetails.FirstOrDefaultAsync(u => u.EmailID == email);

    // Retrieve the book based on the bookID value
    Book book = await dB.Books.FirstOrDefaultAsync(b => b.BookID == BookID);

    // Create a new BorrowedBook object
    BorrowBook borrowedBook = new BorrowBook
    {
        BookID = book.BookID,
        BookName = book.BookName,
        BookEdition = book.BookEdition,
        Name = user.Name,
        EmailID = user.EmailID,
        Date = DateTime.Now
    };

    // Add the new BorrowedBook object to the database and save changes
    DB.BorrowBooks.Add(borrowedBook);
    await DB.SaveChangesAsync();

    // Redirect the user back to the Books view
    return RedirectToAction("UserHomePage");
}
    [HttpGet]


    public IActionResult Index()
    {
        return View("MainIndex", "Category");
    }
    public IActionResult LoginPage()
    {
        return View();
    }
    public IActionResult AdminHomePage()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

public async Task<IActionResult> Borrow(int BookID)
{
    // Retrieve the current user
    var email = HttpContext.Session.GetString("EmailID");
    UserDetail user = await db.UserDetails.FirstOrDefaultAsync(u => u.EmailID == email);

    // Retrieve the book based on the bookID value
    Book book = await dB.Books.FirstOrDefaultAsync(b => b.BookID == BookID);

    // Create a new BorrowedBook object
    Borrow borrow = new Borrow
    {
        BookID = book.BookID,
        BookName = book.BookName,
        BookEdition = book.BookEdition,
        Name = user.Name,
        EmailID = user.EmailID,
        BorrowDate = DateTime.Now
    };

    // Add the new BorrowedBook object to the database and save changes
    db2.borrows.Add(borrow);
    await db2.SaveChangesAsync();

    // Redirect the user back to the Books view
    return RedirectToAction("UserHomePage");
}


[HttpGet]

        public async Task<IActionResult> IssueBookDetails(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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
          var li = from s in db1.IssueBooks
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
                    li = li.OrderBy(s => s.BookEdition);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<IssueBook>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        
 public async Task<IActionResult> ReturnBook(int BookID)
 {       
     IssueBook issueBook = await db1.IssueBooks.FirstOrDefaultAsync(b => b.BookID == BookID);

     // Retrieve the book based on the bookID value
        issueBook.ReturnDate=DateTime.Now;
        issueBook.Issued="Returned";

     // Add the new IssueBook object to the database and save changes
     db1.IssueBooks.Update(issueBook);
     await db1.SaveChangesAsync();

     // // Remove the book from the BorrowBooks table
     // Redirect to the AdminHomePage action
    return RedirectToAction("UserHomePage");
 }
}
