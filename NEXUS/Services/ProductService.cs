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

        private GenericRepository<NEXUS.Models.store> _storeRepository;

        public GenericRepository<NEXUS.Models.store> StoreRepository
        {
            get
            {
                if (this._storeRepository == null)
                    this._storeRepository = new GenericRepository<store>(_context);
                return _storeRepository;
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

        private GenericRepository<NEXUS.Models.employee_store> _employeeStoreRepository;

        public GenericRepository<NEXUS.Models.employee_store> EmployeeStoreRepository
        {
            get
            {
                if (this._employeeStoreRepository == null)
                    this._employeeStoreRepository = new GenericRepository<employee_store>(_context);
                return _employeeStoreRepository;
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
            if (Equals(search, null))
            {
                search = "";
            }
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
        public List<connection_group> GetListConnectionGroup()
        {
            return ConnectionGroupRepository.GetAll().ToList();
        }
        public employee_store GetListStoreByEmployeeId(int id)
        {
            return EmployeeStoreRepository.FindBy(p => p.employee_id == id).Include(p => p.user.user_profile).Include(p => p.store).FirstOrDefault();
        }

        public employee_store GetEmployeeStoreById(int EmployeeId,int StoreId)
        {
            return EmployeeStoreRepository.FindBy(p => p.employee_id == EmployeeId && p.store_id == StoreId)
                .FirstOrDefault();
        }

        public List<connection> GetListConnect()
        {
            return ConnectionRepository.GetAll().ToList();
        }

        public store GetStoreByName(string name)
        {
            return StoreRepository.FindBy(p => p.name == name).FirstOrDefault();
        }

        public store GetStoreById(int id)
        {
            return StoreRepository.FindBy(p => p.store_id == id).FirstOrDefault();
        }

        public List<store> GetListStore()
        {
            return StoreRepository.FindBy(p => p.status == 1).ToList();
        }

        public void SaveStore(store model)
        {
            _storeRepository.Save(model);
        }

        public void SaveEmployeeStore(employee_store model)
        {
            _employeeStoreRepository.Save(model);
        }
        
    }
}