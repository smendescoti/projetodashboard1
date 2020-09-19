using OfficeOpenXml;
using OfficeOpenXml.Style;
using Projeto.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports
{
    public class ContaReportExcel
    {
        //método para receber uma listagem de contas
        //e retornar um arquivo do tipo EXCEL
        public static byte[] GenerateReport(List<Conta> contas)
        {
            //definir o tipo de licença de uso do EPPLUS
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Cores..
            var white = ColorTranslator.FromHtml("#FFFFFF");
            var darkGrey = ColorTranslator.FromHtml("#363636");
            var lightGrey = ColorTranslator.FromHtml("#DCDCDC");

            //criando um arquivo EXCEL
            using (var excelPackage = new ExcelPackage())
            {
                var sheet = excelPackage.Workbook.Worksheets.Add("Contas");

                sheet.Cells["A1"].Value = "Relatório de Contas";

                var titulo = sheet.Cells["A1:F1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 16;
                titulo.Style.Font.Bold = true;
                titulo.Style.Font.Color.SetColor(white);
                titulo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                titulo.Style.Fill.BackgroundColor.SetColor(darkGrey);
                titulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                sheet.Cells["A2"].Value = DateTime.Now.ToString("dddd, dd/MM/yyyy");

                //colunas do relatorio
                sheet.Cells["A4"].Value = "Nome da Conta";
                sheet.Cells["B4"].Value = "Data";
                sheet.Cells["C4"].Value = "Valor";
                sheet.Cells["D4"].Value = "Descrição";
                sheet.Cells["E4"].Value = "Tipo";
                sheet.Cells["F4"].Value = "Categoria";

                var cabecalho = sheet.Cells["A4:F4"];
                cabecalho.Style.Font.Size = 12;
                cabecalho.Style.Font.Bold = true;
                cabecalho.Style.Font.Color.SetColor(white);
                cabecalho.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalho.Style.Fill.BackgroundColor.SetColor(darkGrey);
                cabecalho.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var linha = 5;
                foreach (var item in contas)
                {
                    sheet.Cells[$"A{linha}"].Value = item.Nome;
                    sheet.Cells[$"B{linha}"].Value = item.Data.ToString("dd/MM/yyyy");
                    sheet.Cells[$"C{linha}"].Value = item.Valor.ToString("c");
                    sheet.Cells[$"D{linha}"].Value = item.Descricao;
                    sheet.Cells[$"E{linha}"].Value = item.Tipo.ToString();
                    sheet.Cells[$"F{linha}"].Value = item.Categoria.ToString();

                    if (linha % 2 == 0) //linha é par?
                    {
                        var conteudo = sheet.Cells[$"A{linha}:F{linha}"];
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(lightGrey);
                    }

                    linha++;
                }

                var dados = sheet.Cells[$"A4:F{linha - 1}"];
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);                
                
                sheet.Cells["A:AZ"].AutoFitColumns();

                //retornar o conteudo do arquivo EXCEL..
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
