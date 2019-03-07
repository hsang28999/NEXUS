using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NEXUS.Models;

namespace NEXUS.Services
{
    public partial class Service : IService, IDisposable
    {
        private readonly NexusDBEntities _context = new NexusDBEntities();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}