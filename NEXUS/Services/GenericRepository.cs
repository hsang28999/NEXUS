using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NEXUS.Models;
using NEXUS.Helper;

namespace NEXUS.Services
{
    public class GenericRepository<T> where T : class
    {
        internal NexusDBEntities context;
        internal DbSet<T> dbSet;

        public GenericRepository(NexusDBEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual void Save(T entity)
        {
            dbSet.AddOrUpdate(entity);
            Save();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
            Save();
        }

//        public virtual void Update(T entity)
//        {
//            //            dbSet.Attach(entity);
////            dbSet
//            dbSet.AddOrUpdate(entity);
//            Save();
//        }

        public virtual void Save()
        {
            //context.SaveChanges();
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        public virtual void Delete(object id)
        {
            T entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            Save();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}