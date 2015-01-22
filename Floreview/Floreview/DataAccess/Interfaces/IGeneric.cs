using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Interfaces
{
    public interface IGeneric<T> where T : class
    {
        IEnumerable<T> All();

        void Delete(object id);

        void Delete(T entity);

        T GetByID(object id);

        T Insert(T entity);

        void Update(T entity);
    }
}