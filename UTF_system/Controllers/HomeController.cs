using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTF_system.Models;

namespace UTF_system.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Si no hay nadie logueado
            if (Session["id"] == null)
                return RedirectToAction("Login");
            else if((Models.User.Tipo)Session["tipo"] == Models.User.Tipo.admin)
            {
                AdminController controller = new AdminController();
                controller.ControllerContext = ControllerContext;
                return controller.Index();
              
            }

            return RedirectToAction("Login");

        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string password)
        {

            int ID = 0;


            //No se inserto un numero
            if(!int.TryParse(id, out ID))
            {
                return View();
            }

            Models.User user = Models.User.SelectUserById(ID);

            //No existe el usuario
            if(user == null)
            {
                return View();
            }

            //Si la contrasena no es la correcta
            if (password != user.Password)
                return View();

            //TODO: verificar cuando haya sido un problema de conexion con la base de datos
            Session["id"] = id;
            Session["tipo"] = user.Type;

            if(user.Type == Models.User.Tipo.admin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return Content(user.Nombre);
        }
    }
}