 using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;

 namespace LibraryManagementSystem.Models;

public class UserDetail
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Profile picture is required")]
    public string ProfilePicture { get; set; }

    [Required(ErrorMessage = "Register number is required")]
    public string RegisterNo { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email ID is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailID { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Age is required")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; }
}



// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace LibraryManagementSystem.Models;


// public partial class UserDetail
// {
//     [Key]
//     public int ID { get; set; }
    

//     [Required(AllowEmptyStrings = true)]
//     public string ProfilePicture { get; set; }


// [Required(ErrorMessage = "Register number field is required.")]
//     public int RegisterNo { get; set; }


// [Required(ErrorMessage = "Name field is required.")]
//     public string Name { get; set; }

// [Required(ErrorMessage = "Email ID field is required.")]
//     public string EmailID { get; set; }

// [Required(ErrorMessage = "Password field is required.")]
//     public string Password { get; set; }

//     [Required(ErrorMessage = "Gender field is required.")]
//     public string Gender { get; set; }


// [Required(ErrorMessage = "Age field is required.")]
//     public int Age { get; set; }


//     [Required(ErrorMessage = "Address field is required.")]
//     public string Address { get; set; }

//     [Required(ErrorMessage = "phone number field is required.")]
//     public string PhoneNumber { get; set; }

// }
