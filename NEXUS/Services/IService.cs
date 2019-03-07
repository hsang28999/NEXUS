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
    }
}