using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NEXUS.Models;
using NEXUS.Helper;

namespace NEXUS.Services
{
    public partial class Service
    {
        private GenericRepository<NEXUS.Models.user_profile> _userProfileRepository;

        public GenericRepository<NEXUS.Models.user_profile> UserProfileRepository
        {
            get
            {
                if(this._userProfileRepository == null)
                    this._userProfileRepository = new GenericRepository<user_profile>(_context);
                return _userProfileRepository;
            }
        }

        private GenericRepository<NEXUS.Models.user> _userRepository;

        public GenericRepository<NEXUS.Models.user> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<user>(_context);
                return _userRepository;
            }
        }

        public user Login(UserModel model)
        {
            var password = Encrypt.EncodePassword(model.Password);
            return UserRepository.FindBy(p => p.phone_number == model.PhoneNumber && p.password == password)
                .FirstOrDefault();
        }

        public user GetUserById(int id)
        {
            return UserRepository.FindBy(p => p.user_id == id).FirstOrDefault();
        }

        public user GetUserByPhoneNumber(string phone_number)
        {
            return UserRepository.FindBy(p => p.phone_number == phone_number).FirstOrDefault();
        }

        public void SaveUser(user model)
        {
            UserRepository.Save(model);
        }

        public user_profile GetUserProfileById(int id)
        {
            return UserProfileRepository.FindBy(p => p.user_id == id).FirstOrDefault();
        }
        public user_profile GetUserProfileByPhoneNumber(string PhoneNumber)
        {
            return UserProfileRepository.FindBy(p => p.phone_number == PhoneNumber).FirstOrDefault();
        }

        public void SaveUserProfile(user_profile model)
        {
            UserProfileRepository.Save(model);
        }
        public void UpdateUserProfile(user_profile model)
        {
            UserProfileRepository.Save(model);
        }

        public void UpdateUser(user model)
        {
            UserRepository.Save(model);
        }

        public UserModel ConvertUserToUserModel(user model)
        {
            return new UserModel()
            {
                Id = model.user_id,
                PhoneNumber = model.phone_number,
                Money = 0,
                FullName = "",
                Password = "",
                UserCode = "",
            };
        }
    }
}