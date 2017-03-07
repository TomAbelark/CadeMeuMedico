using CadeMeuMedico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedico.Controllers
{
    public class HomeController : BaseController
    {

        private CadeMeuMedicoBDEntities1 db = new CadeMeuMedicoBDEntities1();

        // GET: Home
        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)";
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Especialidades = new SelectList(db.Especialidades, "IDEspecialidade", "Nome");
            ViewBag.Cidades = new SelectList(db.Cidades, "IDCidade", "Nome");

            return View();
        }

        public ActionResult Pesquisar(Pesquisa pesquisa)
        {
            
            var medicos = from m in db.Medicos
                          where m.IDCidade == pesquisa.IdCidade && m.IDEspecialidade == pesquisa.IdEspecialidade
                          select new { Nome = m.Nome, Crm = m.CRM };

            return Json(medicos, JsonRequestBehavior.AllowGet);
        }
    }
}