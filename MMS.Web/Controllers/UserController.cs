using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using MMS.Data.Services;
using MMS.Web.Models;
using System.Security.Claims;
using MMS.Data.Entities;

namespace MMS.Web.Controllers;
public class UserController : BaseController
{
    private readonly IUserService _svc;

    public UserController()
    {
        _svc = new UserServiceDb();
    }

    // GET /user/login
    public IActionResult Login()
    {
        return View();
    }
    
    // POST /user/login
    [HttpPost] [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("Email,Password")]User m)
    {        
        // call service to Authenticate User
        var user = _svc.Authenticate(m.Email, m.Password);
        
        // check if user not authenticated and add validation errors for email and password
        if (user == null)
        {
            ModelState.AddModelError("Email", "Invalid Login Credentials");
            ModelState.AddModelError("Password", "Invalid Login Credentials");
            return View(m);
        }
        
        // authenticated so sign user in using cookie authentication to store principal
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            BuildClaimsPrincipal(user)
        );

        // login successful now redirect to home page
        Alert("Login Successful");
        return RedirectToAction("Index","Home");
    }

    // GET /user/register
    public IActionResult Register()
    {
        return View();
    }
    
    // POST /user/register
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public IActionResult Register([Bind("Name,Email,Password,PasswordConfirm,Role")]UserRegisterViewModel m)
    {
        // check if email address is already in use - replaced by use of remote validator in UserRegisterViewModel
        if (_svc.GetUserByEmail(m.Email) != null) {
            ModelState.AddModelError(nameof(m.Email),"This email address is already in use. Choose another");
        }

        if(_svc.GetUserByName(m.Name) != null) {
            ModelState.AddModelError(nameof(m.Name),"Username already in use. Please choose another");
        }
        // check validation
        if (ModelState.IsValid)
        {
            // register user
            var user = _svc.Register(m.Name, m.Email, m.Password, m.Role);               
            
            // registration successful now redirect to login page
            Alert("Registration Successful. Please Login");
            return RedirectToAction(nameof(Login));
        }

        // redisplay form with validation errors
        return View(m);
    }


    // POST /user/logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {       
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      
        // logout successful so redirect to login page
        Alert("Logout Successful");
        return RedirectToAction(nameof(Login));
    }

    // GET //user/errornotauthorised
    public IActionResult ErrorNotAuthorised()
    {   
        Alert("You are not Authorised to Carry out that action");
        return RedirectToAction(nameof(Login)); 
        //return View();
    } 
    // GET //user/errornotauthenticated
    public IActionResult ErrorNotAuthenticated()
    {
        Alert("You must first Authenticate to carry out that action");
        return RedirectToAction(nameof(Login)); 
    }     

    // used by remote validator to verify email address is available for registration
    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyEmailAddress(string email)
    {
        if (_svc.GetUserByEmail(email) != null)
        {
            return Json(false); //$"Email Address {email} is already in use. Please choose another");
        }
        return Json(true);
    }

    // used by remote validator to verify email address is available for registration
    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyUsername(string name)
    {
        if (_svc.GetUserByName(name) != null)
        {
            return Json(false); //$"Username {name} is already in use. Please choose another");
        }
        return Json(true);
    }

    // =========================== PRIVATE UTILITY METHOD ==============================

    // return a claims principle using the info from the user parameter
    private ClaimsPrincipal BuildClaimsPrincipal(User user)
    { 
        // define user claims
        var claims = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())                              
        }, CookieAuthenticationDefaults.AuthenticationScheme);

        // build principal using claims
        return  new ClaimsPrincipal(claims);
    } 
} 
