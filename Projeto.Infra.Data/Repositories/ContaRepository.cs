using Projeto.Domain;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        //atributo
        private readonly DataContext dataContext;

        //construtor para injeção de dependência
        public ContaRepository(DataContext dataContext)
            : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax)
        {
            return dataContext
                    .Contas
                    .Where(c => c.Data >= dataMin && c.Data <= dataMax)
                    .OrderBy(c => c.Data)
                    .ToList();
        }

        public decimal GetTotalPorTipo(TipoConta tipoConta)
        {
            return dataContext
                    .Contas
                    .Where(c => c.Tipo == tipoConta)
                    .Sum(c => c.Valor);
        }
    }
}
