using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class OrderEntity
    {
        [Key]
        public int id{get;set;}
        public string? Name{get; set;}

        public string? Email{get; set;}

        public string? Mobile{get; set;}

        public   string? Total_amt{get; set;}

         [NotMapped]

         public string? TransactionId{get; set;}

        [Column("Order_Id")]

         public string? OrderId{get; set;}

         public string PaymentStatus{get; set;}
}
}