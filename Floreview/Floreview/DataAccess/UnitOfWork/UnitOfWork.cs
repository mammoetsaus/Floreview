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
        private FlowerContext context = null;

        public UnitOfWork(FlowerContext context)
        {
            this.context = context;
        }
        
        public UnitOfWork()
        {

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}