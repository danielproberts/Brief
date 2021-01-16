﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Brief.Areas.Identity.Data;
using Brief.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Brief.Data;

namespace Brief.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;
        public IServiceCollection services;
        private IConfiguration Configuration;
        private readonly BriefContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<BriefUser> _userManager;


        public AdminController(IMapper mapper, IConfiguration _configuration, ILogger<AdminController> logger, BriefContext context, Microsoft.AspNetCore.Identity.UserManager<BriefUser> userManager)
        {
            _mapper = mapper;
            Configuration = _configuration;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MakeAdmin(string email)
        {
            BriefUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return View("Index");
        }
    }
}
