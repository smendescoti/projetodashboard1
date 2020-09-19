using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //chave primária
            builder.HasKey(c => c.Id);

            //campos da tabela
            builder.Property(c => c.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Data)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.Categoria)
                .IsRequired();
        }
    }
}
