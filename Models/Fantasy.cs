using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;


public partial class Fantasy
{
    [Key]

    [Required]
    public int ID { get; set; }
    

[Required]    
public string? ProfilePicture { get; set; }

    [Required]
    public int BookID { get; set; }

    [Required]
    public string? BookName { get; set; }

    [Required]
    public int BookEdition { get; set; }

    [Required]
    public int TotalPages { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? AuthorName { get; set; }
[Required]

    public DateTime? AddedOn { get; set; }
    [Required]
    public string? Category { get; set; }
}
