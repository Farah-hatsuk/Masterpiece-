using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeSweet.Models;

public partial class SecialOrder
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(\+9627|07)[7-9][0-9]{7}$", ErrorMessage = "Phone number must start with +9627 or 07 and be a valid Jordanian number")]
    public string? Phone { get; set; }

    public string? Img { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public string? Date { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
    public string? Message { get; set; }
}
