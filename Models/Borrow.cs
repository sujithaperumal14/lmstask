using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;

public class Borrow
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string EmailID { get; set; }

    [Required]
    public int BookID { get; set; }

    [Required]
    public string BookName { get; set; }

    [Required]
    public int BookEdition { get; set; }
    [Required]
    public DateTime BorrowDate {get;set;}
  
    public string? PdfName { get; set; }

   
    public byte[]? Content { get; set; }

    public DateTime IssueDate { get; set; }

    
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
}