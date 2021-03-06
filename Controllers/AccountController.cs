﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Brief.Areas.Identity.Data;
using Brief.Models;
using Microsoft.AspNetCore.Authentication;using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Brief.Controllers
{
    public class AccountController : Controller
    {

        private readonly IMapper _mapper;
        private readonly UserManager<BriefUser> _userManager;
        private readonly SignInManager<BriefUser> _signInManager;
        public AccountController(IMapper mapper, UserManager<BriefUser> userManager, SignInManager<BriefUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public BriefUser Input { get; set; }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = _mapper.Map<BriefUser>(userModel);
            user.EmailConfirmed = true;
            user.JoinedOn = DateTime.Now;
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoggedUserModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //GetUserInfo(input.Email);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public void GetUserInfo(string username)
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            //string query = "SELECT FirstName, Id FROM AspNetUsers WHERE UserName = " + username;
            string query = "SELECT FirstName, Id FROM AspNetUsers WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar));
            cmd.Parameters["@UserName"].Value = username;
            SqlDataReader rdr = cmd.ExecuteReader();
            con.Open();

            LoggedUserModel user = new LoggedUserModel
            {
                Email = username,
                CreatorFirstName = rdr["FirstName"].ToString(),
                CreatorID = rdr["Id"].ToString()
            };
        }
    }
}
