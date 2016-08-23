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
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Loyalty.Controllers
{
    public class MembershipController : Controller
    {
        MembershipRepository _membership = new MembershipRepository();
        UserRepository _user = new UserRepository();
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                TempData["msg"] = "<script>ModalPopupsAlert('Loyalty','  Session Expired! Login Again... ');</script>";
                return RedirectToAction("Login", "User");

            }
            string userid = HttpContext.Session.GetString("UserID");
       
            return View(_membership.GetMembership(userid));
        }

        public IActionResult Create(Membership memship)
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

            if (ModelState.IsValid && memship.MemberID != null)
            {
                string userid = HttpContext.Session.GetString("UserID");
                _membership.AddMembership(memship, userid);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Membership memship)
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
            if (memship.MemberID != null)
            {
                _membership.UpdateMembership(memship);
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                TempData["msg"] = "<script>ModalPopupsAlert('Loyalty','  Session Expired! Login Again... ');</script>";
                return RedirectToAction("Login", "User");

            }
            List<Membership> _memship = _membership.GetMembershipbyID(id);
            return View(_memship[0]);
        }
    }
}
