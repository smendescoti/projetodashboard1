using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Projeto.Domain;

namespace Projeto.Presentation.Mvc.Models
{
    public class ContaCadastroModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe a descrição da conta")]
        public string Descricao { get; set; }

        //ENUMS..
        public CategoriaConta Categoria { get; set; }
        public TipoConta Tipo { get; set; }
    }
}
