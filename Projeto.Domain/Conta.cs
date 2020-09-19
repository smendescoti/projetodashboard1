using System;

namespace Projeto.Domain
{
    public class Conta
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        #region Relacionamentos

        public TipoConta Tipo { get; set; }
        public CategoriaConta Categoria { get; set; }

        #endregion
    }
}
