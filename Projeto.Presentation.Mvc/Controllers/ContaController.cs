using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain;
using Projeto.Infra.Data.Contracts;
using Projeto.Presentation.Mvc.Models;
using Projeto.Presentation.Mvc.Reports;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContaCadastroModel model, 
            [FromServices] IContaRepository contaRepository)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta();

                    conta.Id = Guid.NewGuid();
                    conta.Nome = model.Nome;
                    conta.Data = DateTime.Parse(model.Data);
                    conta.Valor = decimal.Parse(model.Valor);
                    conta.Descricao = model.Descricao;
                    conta.Categoria = model.Categoria;
                    conta.Tipo = model.Tipo;

                    contaRepository.Create(conta);

                    TempData["MensagemSucesso"] = "Conta cadastrada com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(ContaConsultaModel model, 
            [FromServices] IContaRepository contaRepository)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var dataInicio = DateTime.Parse(model.DataMin);
                    var dataTermino = DateTime.Parse(model.DataMax);

                    model.Contas = contaRepository.GetByDatas(dataInicio, dataTermino);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View(model);
        }

        public IActionResult Exclusao(string id, 
            [FromServices] IContaRepository contaRepository)
        {
            try
            {
                var conta = contaRepository.GetById(Guid.Parse(id));
                contaRepository.Delete(conta);

                TempData["MensagemSucesso"] = "Conta excluida com sucesso.";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(string id, 
            [FromServices] IContaRepository contaRepository)
        {
            var model = new ContaEdicaoModel();

            try
            {
                var conta = contaRepository.GetById(Guid.Parse(id));
                model.Id = conta.Id.ToString();
                model.Nome = conta.Nome;
                model.Data = conta.Data.ToString("dd/MM/yyyy");
                model.Valor = conta.Valor.ToString();
                model.Descricao = conta.Descricao;
                model.Tipo = conta.Tipo;
                model.Categoria = conta.Categoria;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ContaEdicaoModel model,
            [FromServices] IContaRepository contaRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta();

                    conta.Id = Guid.Parse(model.Id);
                    conta.Nome = model.Nome;
                    conta.Data = DateTime.Parse(model.Data);
                    conta.Valor = decimal.Parse(model.Valor);
                    conta.Descricao = model.Descricao;
                    conta.Categoria = model.Categoria;
                    conta.Tipo = model.Tipo;

                    contaRepository.Update(conta);

                    TempData["MensagemSucesso"] = "Conta atualizada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Relatorio()
        {
            return View();
        }

        //método para gerar o relatorio em formato EXCEL
        public void GerarRelatorioExcel([FromServices] IContaRepository contaRepository)
        {
            try
            {
                //consultar as contas no banco de dados
                var contas = contaRepository.GetAll();

                //gerando o relatorio
                var arquivo = ContaReportExcel.GenerateReport(contas);

                //DOWNLOAD do arquivo
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers.Add("content-disposition", "attachment; filename=contas.xlsx");
                Response.Body.WriteAsync(arquivo, 0, arquivo.Length);
                Response.Body.Flush();
                Response.StatusCode = StatusCodes.Status200OK;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
        }

        //método para gerar o relatorio em formato PDF
        public void GerarRelatorioPDF([FromServices] IContaRepository contaRepository)
        {
            try
            {
                //consultar as contas no banco de dados
                var contas = contaRepository.GetAll();

                //gerando o relatorio
                var arquivo = ContaReportPDF.GenerateReport(contas);

                //DOWNLOAD do arquivo
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-disposition", "attachment; filename=contas.pdf");
                Response.Body.WriteAsync(arquivo, 0, arquivo.Length);
                Response.Body.Flush();
                Response.StatusCode = StatusCodes.Status200OK;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
        }

        public JsonResult ObterDadosGraficoCategoria
            ([FromServices] IContaRepository contaRepository)
        {
            try
            {
                //consulta de contas..
                var consulta = contaRepository.GetAll();

                var result = consulta
                            .GroupBy(c => c.Categoria)
                            .Select(
                                c => new HighChartsModel
                                {
                                    name = c.Key.ToString(),
                                    data = new List<decimal> { c.Sum(c => c.Valor) }
                                }
                            ).ToList();

                return Json(result);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult ObterDadosGraficoTipo
            ([FromServices] IContaRepository contaRepository)
        {
            try
            {
                //consulta de contas..
                var consulta = contaRepository.GetAll();

                var result = consulta
                            .GroupBy(c => c.Tipo)
                            .Select(
                                c => new HighChartsModel
                                {
                                    name = c.Key.ToString(),
                                    data = new List<decimal> { c.Sum(c => c.Valor) }
                                }
                            ).ToList();

                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}
