﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Transactions;
using System.Net.Http;
using Newtonsoft.Json;
using NEXUS.Models;
using NEXUS.Helper;
using NEXUS.Services;

namespace NEXUS.Controllers
{
    [RoutePrefix("api/nexus")]
    public class NexusController : BaseController
    {
        private IService _service = new Service();

        [HttpPost]
        [Route("Register")]
        public UserModel Register(UserModel model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var userAcc = _service.GetUserByPhoneNumber(model.PhoneNumber);
                if (!Equals(userAcc, null))
                {
                    ExceptionContent(HttpStatusCode.Unauthorized, "err_phone_number_already_existed");
                }

                userAcc = new user()
                {
                    phone_number = model.PhoneNumber,
                    email = model.Email,
                    password = Encrypt.EncodePassword(model.Password),
                    user_id = 0,
                    role_id = 1,
                    store_id = 1,
                };
                _service.SaveUser(userAcc);
                var UserAccount = _service.GetUserByPhoneNumber(userAcc.phone_number);

                var UserProfile = new user_profile()
                {
                    user_profile_id = 0,
                    address = "",
                    phone_number = model.PhoneNumber,
                    birthday = 0,
                    email = model.Email,
                    full_name = model.FullName,
                    gender = 1,
                    user_id = UserAccount.user_id,
                    created_date = ConvertDatetime.GetCurrentUnixTimeStamp(),
                };

                _service.SaveUserProfile(UserProfile);
                UserProfile = _service.GetUserProfileByPhoneNumber(model.PhoneNumber);
                

                var token = new TokenModel()
                {
                    Id = UserAccount.user_id,
                    PhoneNumber = UserAccount.phone_number,
                    Role = 1
                };

                scope.Complete();

                return new UserModel()
                {
                    Id = UserAccount.user_id,
                    FullName = UserProfile.full_name,
                    PhoneNumber = UserAccount.phone_number,
                    UserCode = "UID_" + UserAccount.user_id.ToString().PadLeft(5, '0'),
                    Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token))
                };
            }
        }

        [HttpPost]
        [Route("Login")]
        public UserModel Login(UserModel model)
        {
            var UserAccount = _service.Login(model);
            if (Equals(UserAccount, null))
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "err_username_or_password_invalid");
            }
            var UserProfile = _service.GetUserProfileByPhoneNumber(model.PhoneNumber);

            var token = new TokenModel()
            {
                Id = UserAccount.user_id,
                PhoneNumber = UserAccount.phone_number,
                Role = 1
            };

            return new UserModel()
            {
                Id = UserAccount.user_id,
                FullName = UserProfile.full_name,
                PhoneNumber = UserAccount.phone_number,
                UserCode = "UID_" + UserAccount.user_id.ToString().PadLeft(5, '0'),
                Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token))
            };
        }

        //[HttpGet]
        //[Route("Test")]
        //public List<UserModel> Test()
        //{
        //    var User = _service.GetUserById(2);
        //    return User.user_profile.Select(p => new UserModel()
        //    {
        //        Email = p.email,
        //        FullName = p.full_name,
        //        Gender = p.gender,
        //        Money = p.money,
        //        Address = p.address,
        //        PhoneNumber = p.phone_number
        //    }).ToList();
        //}

        [HttpGet]
        [Route("GetUserProfile/{id}")]
        public UserModel GetUserProfile(int id)
        {
            var User = _service.GetUserProfileById(id);
            return new UserModel()
            {
                Email = User.email,
                FullName = User.full_name,
                Gender = User.gender,
                Money = User.money,
                Address = User.address,
                PhoneNumber = User.phone_number
            };
        }

        [HttpPost]
        [Route("SaveUserProfile/{id}")]
        public void SaveUserProfile(int id,UserModel model)
        {
            var User = _service.GetUserProfileById(id);
            User.email = model.Email;
            User.full_name = model.FullName;
            User.gender = model.Gender;
            User.money = model.Money;
            User.address = model.Address;
            
            _service.SaveUserProfile(User);
        }

        //[HttpGet]
        //[Route("GetListProduct")]
        //public List<ConnectionModel> GetListProduct()
        //{
        //    var connections = _service.GetListConnect();
        //    return connections.Select(q => new ConnectionModel()
        //    {
        //        Name = q.connection_name,
        //        SecurityDeposit = q.security_deposit,
        //        ConnectionGroups = _service.GetListConnectionGroupsByConnectionId(q.connection_id),
        //    }).ToList();
        //}

        [HttpGet]
        [Route("GetListProduct")]
        public List<ConnectionModel> GetListProduct()
        {
            var connections = _service.GetListConnect();
            return connections.Select(q => new ConnectionModel()
            {
                Name = q.connection_name,
                SecurityDeposit = q.security_deposit,
                ConnectionGroups = q.connection_group.Select(c => new ConnectionGroupModel()
                {
                    Bandwidth = c.bandwidth,
                    Name = c.connection_group_name,
                    Products = c.product.Select(p => new ProductModel()
                    {
                        ConnectionGroupId = p.connection_group_id,
                        Description = p.description,
                        PpmLocal = p.ppm_local,
                        PpmMobile = p.ppm_mobile,
                        PpmStd = p.ppm_std,
                        Price = p.price,
                        ProductId = p.product_id,
                        ProductName = p.product_name,
                        Status = p.status,
                        TimeType = p.time_type,
                        TimeUsed = p.time_used,
                        Type = p.type,
                        MonthAvailable = p.month_available,
                    }).ToList()
                }).ToList(),
            }).ToList();
        }

    }
}