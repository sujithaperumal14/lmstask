using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5285/");
    }

    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }


        [HttpPost]
public async Task<IActionResult> Signup(SignupViewModel signupViewModel)
{
    var signupResponse = await _httpClient.PostAsJsonAsync("/signup", signupViewModel);

    if (signupResponse.IsSuccessStatusCode)
    {
        // Signup was successful, redirect to the login page
        return RedirectToAction("AdminLogin", "Login");
    }
    else if (signupResponse.StatusCode == HttpStatusCode.Conflict)
    {
        // User already exists, display an error message to the user
        TempData["ErrorMessage"] = "A user with the same email address already exists.";
        return View(signupViewModel);
    }
    else
    {
        // Signup failed, display an error message to the user
        var errorMessage = await signupResponse.Content.ReadAsStringAsync();
        ModelState.AddModelError("", errorMessage);
        return View(signupViewModel);
    }
}



}