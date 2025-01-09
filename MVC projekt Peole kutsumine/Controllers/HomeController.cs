using MVC_projekt_Peole_kutsumine.Models;
using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC_projekt_Peole_kutsumine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Ootan sind minu peole! Palun tule!!!";
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast!";
            return View();
        }

        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            if (ModelState.IsValid)
            {
                E_mail(guest); // Функция для отправки письма с ответом
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "daragalcenko3@gmail.com";
                WebMail.Password = "iqer zkvm czuv lgqn";
                WebMail.From = "daragalcenko3@gmail.com";
                WebMail.Send("daragalcenko3@gmail.com", "Vastus kutsele", guest.Name + " vastas " + (guest.WillAttend.HasValue && guest.WillAttend.Value ? "tuleb peole" : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju!Ei saa kirja saada!!!";
            }
        }
    }
}
