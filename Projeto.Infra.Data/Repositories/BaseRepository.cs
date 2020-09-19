using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        //atributo
        private readonly DataContext dataContext;

        //construtor para injeção de dependência
        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Create(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Added;
            dataContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Deleted;
            dataContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return dataContext.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return dataContext.Set<T>().Find(id);
        }
    }
}
