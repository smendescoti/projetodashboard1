using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Projeto.Domain;

namespace Projeto.Presentation.Mvc.Models
{
    public class ContaConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataMax { get; set; }

        //dados exibidos na página
        public List<Conta> Contas { get; set; }
    }
}
