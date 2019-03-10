using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NEXUS.Models;

namespace NEXUS.Services
{
    public partial class Service
    {
        private GenericRepository<NEXUS.Models.product> _productRepository;

        public GenericRepository<NEXUS.Models.product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<product>(_context);
                return _productRepository;
            }
        }

        public void SaveProduct()
        {

        }

        
    }
}