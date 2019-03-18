using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text;
using NEXUS.Models;

namespace NEXUS.Services
{
    public interface IService
    {
       
        void Dispose();
        user GetUserByPhoneNumber(string phone_number);
        user GetUserById(int id);
        user Login(UserModel model);
        void SaveUser(user model);
        void UpdateUser(user model);
        UserModel ConvertUserToUserModel(user model);
        void SaveUserProfile(user_profile model);
        void UpdateUserProfile(user_profile model);
        user_profile GetUserProfileById(int id);
        user_profile GetUserProfileByPhoneNumber(string PhoneNumber);
        //List<ConnectionGroupModel> GetListConnectionGroupsByConnectionId(int connection_id);
        List<product> GetListProductsByConnectionGroupId(int connection_group_id);
        List<product> GetListProducts(string search);
        List<connection_group> GetListConnectionGroup();
        product GetProductById(int id);
        void SaveProduct(product model);
        List<connection> GetListConnect();
        List<user_profile> GetListUserProfile(string search);
        employee_store GetListStoreByEmployeeId(int id);
        List<store> GetListStore();
        void SaveStore(store model);
        void SaveEmployeeStore(employee_store model);
        employee_store GetEmployeeStoreById(int EmployeeId, int StoreId);
        List<user_profile> GetListEmployee(string search);
        store GetStoreById(int id);
    }
}