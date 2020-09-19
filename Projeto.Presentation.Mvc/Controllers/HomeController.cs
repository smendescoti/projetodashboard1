using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain;
using Projeto.Infra.Data.Contracts;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(
            [FromServices] IContaRepository contaRepository)
        {
            try
            {
                var totalReceitas = contaRepository.GetTotalPorTipo(TipoConta.Receita);
                var totalDespesas = contaRepository.GetTotalPorTipo(TipoConta.Despesa);

                TempData["TotalReceitas"] = totalReceitas.ToString("c");
                TempData["TotalDespesas"] = totalDespesas.ToString("c");
                TempData["Saldo"] = (totalReceitas - totalDespesas).ToString("c");
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }
    }
}
