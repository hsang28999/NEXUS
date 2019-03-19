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

        public List<contract> GetListContract(string search)
        {
            if (Equals(search,null))
            {
                search = "";
            }
            return ContractRepository.FindBy(p =>
                p.user.phone_number.Contains(search) || p.user.email.Contains(search) || p.user1.email.Contains(search) ||
                p.user1.phone_number.Contains(search) || p.store.name.Contains(search) ||
                p.product_name.Contains(search)).ToList();
        }

        public contract GetContractById(int id)
        {
            return ContractRepository.FindBy(p => p.contract_id == id).FirstOrDefault();
        }

        public void SaveContract(contract model)
        {
            ContractRepository.Save(model);
        }
    }
}