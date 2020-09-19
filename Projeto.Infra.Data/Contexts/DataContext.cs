using Microsoft.EntityFrameworkCore;
using Projeto.Domain;
using Projeto.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contexts
{
    //Regra 1) HERDAR DbContext
    public class DataContext : DbContext
    {
        //Regra 2) Construtor para receber a connectionstring
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        //Regra 3) Declarar uma propriedade DbSet para cada entidade mapeada
        public DbSet<Conta> Contas { get; set; }

        //Regra 4) Sobrescrever o método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento
            modelBuilder.ApplyConfiguration(new ContaMap());
        }
    }
}
