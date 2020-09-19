using Projeto.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IContaRepository : IBaseRepository<Conta>
    {
        List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax);
        decimal GetTotalPorTipo(TipoConta tipoConta);
    }
}
