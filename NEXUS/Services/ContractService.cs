using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NEXUS.Models;

namespace NEXUS.Services
{
    public partial class Service
    {
        private GenericRepository<NEXUS.Models.contract> _contractRepository;

        public GenericRepository<NEXUS.Models.contract> ContractRepository
        {
            get
            {
                if (this._contractRepository == null)
                    this._contractRepository = new GenericRepository<contract>(_context);
                return _contractRepository;
            }
        }
    }
}