using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MMS.Data.Entities;

namespace MMS.Web.Models;
    
public class UserRegisterViewModel
{       
    [Required]
    [EmailAddress]
    [Remote(action: "VerifyEmailAddress", controller: "User", ErrorMessage = "Email address is already in use. Please choose another.")]

    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
    [Display(Name = "Confirm Password")]  
    public string PasswordConfirm  { get; set; }

    [Required]
    public Role Role { get; set; }

    [Required]
    [Remote(action:"VerifyUsername", controller:"User", ErrorMessage="Username is already in use. Please choose another")]
    public string Name { get; set; }


}
