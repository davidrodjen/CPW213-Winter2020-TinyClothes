using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        //private-only in this file, only accessed by constructor
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _http;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public AccountController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                //Check username is unique
                if ( !await AccountDb.IsUsernameAvailable(reg.Username, _context))
                {
                    Account acc = new Account()
                    {
                        Email = reg.Email,
                        FullName = reg.FullName,
                        Password = reg.Password,
                        Username = reg.Username
                    };

                    //if unique, add account to DB
                    await AccountDb.Register(_context, acc);

                    //with the DB made, this links the session CREATE USER SESSION
                    SessionHelper.CreateUserSession(acc.AccountId, acc.Username, _http);
                    

                    return RedirectToAction("Index", "Home");
                }
                else // If username is taken, add error
                {

                    // Display error with other username error messages
                    //Tell it what class this property is in
                    ModelState.AddModelError(nameof(Account.Username)
                        , "Username is already taken");
                }
            }

            return View(reg);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                Account acc = await AccountDb.DoesUserMatch(login, _context);
                if(acc != null)
                {
                    SessionHelper.CreateUserSession(acc.AccountId, acc.Username, _http);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Invalid Credentials");
                }


            }
            return View(login);

        }

        public IActionResult Logout()
        {
            SessionHelper.DestroyUserSession(_http);
            return RedirectToAction("Index", "Home"); //go to the index action of the home controller
        }
    }
}