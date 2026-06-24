using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

}