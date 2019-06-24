using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTF_system.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //Si no hay nadie logueado
            if (Session["id"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["tipo"] = null;

            return RedirectToAction("Login", "Home");
        }
    }
}