using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Projeto.Domain;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports
{
    public class ContaReportPDF
    {
        public static byte[] GenerateReport(List<Conta> contas)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            using (var document = new Document(pdf))
            {
                var formatacaoTitulo = new Style();
                formatacaoTitulo.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                formatacaoTitulo.SetFontSize(20);
                formatacaoTitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

                var formatacaoSubtitulo = new Style();
                formatacaoSubtitulo.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                formatacaoSubtitulo.SetFontSize(10);
                formatacaoSubtitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

                document.Add(new Paragraph
                    ("Relatório de Contas - C# WebDeveloper")
                    .AddStyle(formatacaoTitulo));

                document.Add(new Paragraph
                    ($"Data de geração: {DateTime.Now.ToString("dddd, dd/MM/yyyy")}")
                    .AddStyle(formatacaoSubtitulo));

                var table = new Table(6);

                var tamanhoFonteCabecalho = 11;
                var tamanhoFonteCorpo = 10;

                table.AddHeaderCell("Nome da Conta")
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                table.AddHeaderCell("Data da Conta")
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                table.AddHeaderCell("Valor").SetFontSize(tamanhoFonteCabecalho)
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                table.AddHeaderCell("Descrição").SetFontSize(tamanhoFonteCabecalho)
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                table.AddHeaderCell("Tipo da Conta").SetFontSize(tamanhoFonteCabecalho)
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                table.AddHeaderCell("Categoria").SetFontSize(tamanhoFonteCabecalho)
                    .SetFontSize(tamanhoFonteCabecalho)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                foreach (var item in contas)
                {
                    table.AddCell(item.Nome)
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

                    table.AddCell(item.Data.ToString("dd/MM/yyyy"))
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

                    table.AddCell(item.Valor.ToString("c"))
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

                    table.AddCell(item.Descricao)
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

                    table.AddCell(item.Tipo.ToString())
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

                    table.AddCell(item.Categoria.ToString())
                        .SetFontSize(tamanhoFonteCorpo)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                }

                document.Add(table);
            }

            return memoryStream.ToArray();
        }
    }
}
