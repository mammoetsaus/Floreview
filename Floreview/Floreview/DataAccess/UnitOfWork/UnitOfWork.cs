using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields & Props
        private FlowerContext context = null;
        #endregion

        #region Constructor
        public UnitOfWork(FlowerContext context)
        {
            this.context = context;
        }
        
        public UnitOfWork()
        {

        }
        #endregion

        #region IUnitOfWork Interface
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        #endregion
    }
}