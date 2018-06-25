using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Tacs.Models.Repositories
{
    public class TestRepository<T> : IRepository<T> where T : class
    {
        protected List<T> Context;

        public TestRepository()
        {
            Context = new List<T>();
        }

        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return (Context as IQueryable<T>).Where(predicate);
        }

        public T Get(int id)
        {
            return (Context as List<IIdeable>).Find(e => e.Id == id) as T;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.ToList();
        }

        public void Remove(T entity)
        {
            Context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            (Context as List<IIdeable>).RemoveAll(e => (entities as IEnumerable<IIdeable>).Any(ie => ie.Id == e.Id));
        }
    }

    public interface IIdeable
    {
        int Id {get; set;}
    }
}