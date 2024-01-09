using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models{
    public class Ordering{
        public int Id{get; set;}

        public string? Name{get; set;}

        public string? Email{get; set;}

        public string? Mobile{get; set;}

        public   string? Total_amt{get; set;}

         [NotMapped]

         public string? TransactionId{get; set;}

        [NotMapped]

         public string? OrderId{get; set;}
        
    }
}