using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models{
    public class SignupViewModel{

        [Key]
        public int Userid{get; set;}

        public string? Name{get; set;}

        public string? Email{get; set;}

        public String? Password{get; set;}

        public string? confirmpassword{get; set;}
    }
}