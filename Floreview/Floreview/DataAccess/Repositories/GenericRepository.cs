using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories {
    public class GenericRepository<T> : IGeneric<T> where T : class {

        internal FlowerContext context;
        internal DbSet<T> dbSet;

        public GenericRepository() {

        }
        public GenericRepository(FlowerContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> All() {
            int aantal = dbSet.Count<T>();
            return dbSet;
        }

        public virtual T GetByID(object id) {
            return dbSet.Find(id);
        }

        public virtual T Insert(T entity) {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id) {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete) {
            if (context.Entry(entityToDelete).State == EntityState.Detached) {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate) {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}