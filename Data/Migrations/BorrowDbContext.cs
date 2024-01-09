using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class BorrowDbContext : DbContext  
    {  
        public BorrowDbContext(DbContextOptions<BorrowDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<Borrow> borrows { get; set; }  
    }  
}  