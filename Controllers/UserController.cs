using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Dynamic;
using Loyalty.Models;
using Loyalty.Repository;
using Microsoft.AspNetCore.Session;
using System.Globalization;

namespace Loyalty.Controllers
{

    public class UserController : Controller
    {
        #region Constructor      

        UserRepository _user = new UserRepository();
        #endregion


        public IActionResult Account()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                TempData["msg"] = "<script>ModalPopupsAlert('Loyalty','  Session Expired! Login Again... ');</script>";
                return RedirectToAction("Login", "User");

            }
            string userid = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userid))
            {
                List<Users> _usr = _user.GetUserByID(userid);
                return View(_usr[0]);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Account(Users _User)
        {
            double dbsize = _user.Getdbsize();

            if (dbsize >= 4.50)
            {
                return View();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                TempData["msg"] = "<script>ModalPopupsAlert('Loyalty','  Session Expired! Login Again... ');</script>";
                return RedirectToAction("Login", "User");
            }
            if (_User.UserID != null)
            {
                _user.UpdateUser(_User);
                return RedirectToAction("Account");
            }
            return View();
        }


        public IActionResult Login(Login _login)
        {

            _user.CreateUserTable();
            _user.CreateMembershipTable();
            List<Login> _usrlogin = _user.GetLogin(_login);

            if (_usrlogin != null && _usrlogin.Count > 0)
            {
                if (_usrlogin[0].UserID != null)
                {
                    HttpContext.Session.SetString("UserID", _usrlogin[0].UserID.ToString());
                    HttpContext.Session.SetString("UserName", _usrlogin[0].FirstName.ToString() + ' ' + _usrlogin[0].LastName.ToString());
                }
                return RedirectToAction("Index", "Membership");
            }else
            {
                if (ModelState.IsValid)
                    TempData["notice"] = "Please enter valid User Name / Password.";
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                TempData["msg"] = "<script>ModalPopupsAlert('Loyalty','  Session Expired! Login Again... ');</script>";
            }

            return View();
        }
        public IActionResult SessionTimeOut()
        {
            //Session.Abandon();
            //Session.Clear();
            HttpContext.Session.Clear();
            return View();
        }


        public IActionResult UserName()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return Content("Welcome " + HttpContext.Session.GetString("UserName").ToString());
            }
            else
                return Content("");
        }



        public IActionResult Logout()
        {
            //Session.Abandon();
            //Session.Clear();
            if (ModelState.IsValid)
                TempData["notice"] = "Successfully Logged out.";
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }


        public IActionResult Create(Users usr)
        {

            double dbsize = _user.Getdbsize();

            if (dbsize >= 4.50)
            {
                return View();
            }


            if (ModelState.IsValid && usr.EmailID != null)
            {

                List<Users> lstUser = _user.GetUserByMailID(usr.EmailID);

                if (lstUser.Count == 0)
                {
                    _user.Register(usr);
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    return View();
                }


            }
            return View();
        }
    }
}