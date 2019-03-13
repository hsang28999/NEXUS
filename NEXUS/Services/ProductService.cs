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

        private GenericRepository<NEXUS.Models.connection> _connectionRepository;

        public GenericRepository<NEXUS.Models.connection> ConnectionRepository
        {
            get
            {
                if (this._connectionRepository == null)
                    this._connectionRepository = new GenericRepository<connection>(_context);
                return _connectionRepository;
            }
        }

        private GenericRepository<NEXUS.Models.connection_group> _connectionGroupRepository;

        public GenericRepository<NEXUS.Models.connection_group> ConnectionGroupRepository
        {
            get
            {
                if (this._connectionGroupRepository == null)
                    this._connectionGroupRepository = new GenericRepository<connection_group>(_context);
                return _connectionGroupRepository;
            }
        }

        public void SaveProduct(product model)
        {
            ProductRepository.Save(model);
        }

        public product GetProductById(int id)
        {
            return ProductRepository.FindBy(p => p.product_id == id)
                .FirstOrDefault();
        }

        public List<product> GetListProducts(string search)
        {
            return ProductRepository.FindBy(p => p.status == 1 && (Equals(search, null) || p.product_name.Equals(search))).ToList();
        }

        public List<product> GetListProductsByConnectionGroupId(int connection_group_id)
        {
            return ProductRepository.FindBy(p => p.status == 1 && p.connection_group_id == connection_group_id).ToList();
        }

        //public List<ConnectionGroupModel> GetListConnectionGroupsByConnectionId(int connection_id)
        //{
        //    var connection_group_list = ConnectionGroupRepository.FindBy(p => p.connection_id == connection_id).ToList();
        //    //List<ConnectionGroupModel> connection_group_model_list = new List<ConnectionGroupModel>();
        //    List<ConnectionGroupModel> connection_group_model_list = connection_group_list.Select(p => new ConnectionGroupModel()
        //    {
        //        Name = p.connection_group_name,
        //        Bandwidth = p.bandwidth,
        //        Products = GetListProductsByConnectionGroupId(p.connection_group_id)
        //    }).ToList();
        //    ;
        //    return connection_group_model_list;
        //}

        public List<connection> GetListConnect()
        {
            return ConnectionRepository.GetAll().ToList();
        }
    }
}