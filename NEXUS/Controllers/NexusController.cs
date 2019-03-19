using System;
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
                    role = 1,
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
                    role = 1,
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
                    Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token)),
                   Role = UserProfile.role
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
                Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token)),
                Role = UserProfile.role
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
                PhoneNumber = User.phone_number,
                Role = User.role
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

        [HttpPost]
        [Route("LoginAdmin")]
        public UserModel LoginAdmin(UserModel model)
        {
            var UserAccount = _service.Login(model);
            if (Equals(UserAccount, null))
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "err_username_or_password_invalid");
            }

            if (UserAccount.role == 1)
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "err_authorization");
            }
            var UserProfile = _service.GetUserProfileByPhoneNumber(model.PhoneNumber);

            var token = new TokenModel()
            {
                Id = UserAccount.user_id,
                PhoneNumber = UserAccount.phone_number,
                Role = UserAccount.role
            };

            return new UserModel()
            {
                Id = UserAccount.user_id,
                FullName = UserProfile.full_name,
                PhoneNumber = UserAccount.phone_number,
                UserCode = "UID_" + UserAccount.user_id.ToString().PadLeft(5, '0'),
                Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token)),
                Role = UserProfile.role
            };
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public UserModel RegisterAdmin(UserModel model)
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
                    role = model.Role,
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
                    role = model.Role,
                    user_id = UserAccount.user_id,
                    created_date = ConvertDatetime.GetCurrentUnixTimeStamp(),
                };

                _service.SaveUserProfile(UserProfile);
                UserProfile = _service.GetUserProfileByPhoneNumber(model.PhoneNumber);


                var token = new TokenModel()
                {
                    Id = UserAccount.user_id,
                    PhoneNumber = UserAccount.phone_number,
                    Role = model.Role
                };

                scope.Complete();

                return new UserModel()
                {
                    Id = UserAccount.user_id,
                    FullName = UserProfile.full_name,
                    PhoneNumber = UserAccount.phone_number,
                    UserCode = "UID_" + UserAccount.user_id.ToString().PadLeft(5, '0'),
                    Token = Encrypt.Base64Encode(JsonConvert.SerializeObject(token)),
                    Role = UserProfile.role
                };
            }
        }

        [HttpGet]
        [Route("GetListUser/{page}/{search?}")]
        public PagingResult<UserModel> GetListUser(int page, string search = null)
        {
            var users = _service.GetListUserProfile(search);
            var userList = users.Skip((page - 1) * 10).Take(10).Select(p => new UserModel()
            {
                PhoneNumber = p.phone_number,
                FullName = p.full_name,
                Role = p.role,
                Id = p.user_id,
                Address = p.address,
                Gender = p.gender,
                Birthday = p.birthday,
                Email = p.email
            }).ToList();
            return new PagingResult<UserModel>()
            {
                total = users.Count,
                data = userList
            };
        }

        [HttpGet]
        [Route("GetListStore")]
        public List<StoreModel> GetListStore()
        {
            return _service.GetListStore().Select(p => new StoreModel()
            {
                Name = p.name,
                Address = p.store_address,
                Status = p.status,
                StoreId = p.store_id
            }).ToList();
        }

        [HttpPost]
        [Route("CreateStore")]
        public void CreateStore(StoreModel model)
        {
            var Store = _service.GetStoreByName(model.Name);
            if (!Equals(Store, null))
            {
                ExceptionContent(HttpStatusCode.Unauthorized, "err_phone_number_already_existed");
            }
            Store = new store()
            {
                name = model.Name,
                store_address = model.Address,
                status = 1
            };
            _service.SaveStore(Store);
        }

        [HttpPost]
        [Route("SaveStore/{id}")]
        public void SaveStore(int id, StoreModel model)
        {
            var Store = _service.GetStoreById(id);
            Store.name = model.Name;
            Store.store_address = model.Address;
            Store.status = model.Status;
            _service.SaveStore(Store);
        }

        [HttpGet]
        [Route("GetListEmployee/{page}/{search?}")]
        public PagingResult<UserModel> GetListEmployee(int page, string search = null)
        {
            var users = _service.GetListEmployee(search);
            var userList = users.Skip((page - 1) * 10).Take(10).Select(p => new UserModel()
            {
                PhoneNumber = p.phone_number,
                FullName = p.full_name,
                Role = p.role,
                Id = p.user_id,
                Address = p.address,
                Gender = p.gender,
                Birthday = p.birthday,
                Email = p.email
            }).ToList();
            return new PagingResult<UserModel>()
            {
                total = users.Count,
                data = userList
            };
        }

        [HttpGet]
        [Route("GetEmployeeDetail/{id}")]
        public EmployeeModel GetEmployeeDetail(int id)
        {
            var employee = _service.GetUserProfileById(id);
            var store_employee = _service.GetListStoreByEmployeeId(id);
            var employeemodel = new EmployeeModel()
            {
                PhoneNumber = employee.phone_number,
                FullName = employee.full_name,
                Role = employee.role,
                Id = employee.user_id,
                Address = employee.address,
                Gender = employee.gender,
                Birthday = employee.birthday,
                Email = employee.email,
                StoreId = 0,
                Store = new StoreModel()
            };
            if (!Equals(store_employee,null))
            {
                employeemodel.StoreId = store_employee.store.store_id;
                employeemodel.Store = new StoreModel()
                {
                    Name = store_employee.store.name,
                    Address = store_employee.store.store_address,
                    Status = store_employee.store.status,
                    StoreId = store_employee.store.store_id
                };
            }

            return employeemodel;

        }

        [HttpGet]
        [Route("GetStoreDetail/{id}")]
        public StoreModel GetStoreDetail(int id)
        {
            var Store = _service.GetStoreById(id);
            
            return new StoreModel()
            {
                Name = Store.name,
                Address = Store.store_address,
                StoreId = Store.store_id,
                Status = Store.status,
                ListUser = _service.GetListUserByStoreId(id).Select(p => new UserModel()
                {
                    PhoneNumber = p.user.user_profile.FirstOrDefault().phone_number,
                    FullName = p.user.user_profile.FirstOrDefault().full_name,
                    Address = p.user.user_profile.FirstOrDefault().address,
                    Id = p.user.user_profile.FirstOrDefault().user_id,
                    Role = p.user.user_profile.FirstOrDefault().role,
                    Birthday = p.user.user_profile.FirstOrDefault().birthday,
                    Gender = p.user.user_profile.FirstOrDefault().gender,
                    Email = p.user.user_profile.FirstOrDefault().email
                }).ToList()
            };
        }

        [HttpPost]
        [Route("SaveEmployeeDetail/{id}")]
        public void SaveEmployeeDetail(int id,EmployeeModel model)
        {
            //var EmployeeStore = _service.GetEmployeeStoreById(id, model.StoreId);
            //var test = 0;
            //if (Equals(EmployeeStore, null))
            //{
            //    test = 1;
            //}
            using (TransactionScope scope = new TransactionScope())
            {
                var EmployeeStore = _service.GetEmployeeStoreById(id, model.StoreId);
                var EmployeeProfile = _service.GetUserProfileById(id);
                var Employee = _service.GetUserById(id);
                if (Equals(Employee, null))
                {
                    ExceptionContent(HttpStatusCode.InternalServerError, "employee_or_store_not_exist");
                }

                if (Equals(EmployeeStore,null))
                {
                    EmployeeStore = new employee_store()
                    {
                        employee_id = id,
                        employee_store_id = 0
                    };
                }
                EmployeeStore.store_id = model.StoreId;
                _service.SaveEmployeeStore(EmployeeStore);

                EmployeeProfile.full_name = model.FullName;
                EmployeeProfile.address = model.Address;
                EmployeeProfile.birthday = model.Birthday;
                EmployeeProfile.email = model.Email;
                EmployeeProfile.gender = model.Gender;
                EmployeeProfile.role = model.Role;
                Employee.email = model.Email;
                Employee.role = model.Role;

                _service.SaveUserProfile(EmployeeProfile);
                _service.SaveUser(Employee);

                scope.Complete();
            }

            //return test;
        }

        [HttpGet]
        [Route("GetListFeedback/{search?}")]
        public PagingResult<FeedbackModel> GetListFeedback(string search = null)
        {
            var Feedbacks = _service.GetListFeedback(search);
            return new PagingResult<FeedbackModel>()
            {
                total = Feedbacks.Count(),
                data = Feedbacks.Select(p => new FeedbackModel()
                {
                    Status = p.status,
                    CustomerEmail = p.customer_email,
                    CustomerName = p.customer_name,
                    Note = p.note,
                    FeedbackId = p.feedback_id
                }).ToList()
            };
        }

        [HttpGet]
        [Route("GetFeedbackDetail/{id}")]
        public FeedbackModel GetFeedbackDetail(int id)
        {
            var Feedback = _service.GetFeedbackById(id);
            return new FeedbackModel()
            {
                Status = Feedback.status,
                CustomerEmail = Feedback.customer_email,
                CustomerName = Feedback.customer_name,
                Note = Feedback.note,
                FeedbackId = Feedback.feedback_id
            };
        }


        [HttpPost]
        [Route("CreateFeedback")]
        public void CreateFeedback(FeedbackModel model)
        {
            var Feedback = _service.GetFeedbackById(0);
            if (!Equals(Feedback,null))
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "employee_or_store_not_exist");
            }
            Feedback = new feedback()
            {
                feedback_id = 0,
                status = 1,
                customer_email = model.CustomerEmail,
                customer_name = model.CustomerName,
                note = model.Note
            };
            _service.SaveFeedback(Feedback);
        }

        [HttpPost]
        [Route("SaveFeedback/{id}")]
        public void SaveFeedback(int id,FeedbackModel model)
        {
            var Feedback = _service.GetFeedbackById(id);
            //Feedback.customer_email = model.CustomerEmail;
            //Feedback.customer_name = model.CustomerName;
            //Feedback.note = model.Note;
            Feedback.status = model.Status;
            _service.SaveFeedback(Feedback);
        }

        [HttpGet]
        [Route("GetListProductAdmin/{page}/{search?}")]
        public PagingResult<ProductModel> GetListProductAdmin(int page,string search = null)
        {
            var products = _service.GetListProducts(search);
            var listProduct = products.Select(p => new ProductModel()
            {
                ConnectionGroupId = p.connection_group_id,
                TimeType = p.time_type,
                PpmMobile = p.ppm_mobile,
                PpmStd = p.ppm_std,
                TimeUsed = p.time_used,
                Type = p.type,
                Description = p.description,
                MonthAvailable = p.month_available,
                PpmLocal = p.ppm_local,
                Price = p.price,
                ProductName = p.product_name,
                ProductId = p.product_id,
                Status = p.status
            }).ToList();
            return new PagingResult<ProductModel>()
            {
                total = listProduct.Count,
                data = listProduct
            };
        }

        [HttpGet]
        [Route("GetListConnectionGroup")]
        public List<ConnectionGroupModel> GetListConnectionGroup()
        {
            var connectionGroups = _service.GetListConnectionGroup();
            return connectionGroups.Select(p => new ConnectionGroupModel()
            {
                ConnectionGroupId = p.connection_group_id,
                Name = p.connection_group_name,
                Bandwidth = p.bandwidth,
                ConnectionName = p.connection.connection_name
            }).ToList();
        }

        [HttpGet]
        [Route("GetProductDetail/{id}")]
        public ProductModel GetProductDetail(int id)
        {
            var Product = _service.GetProductById(id);
            return new ProductModel()
            {
                ConnectionGroupId = Product.connection_group_id,
                TimeType = Product.time_type,
                PpmMobile = Product.ppm_mobile,
                PpmStd = Product.ppm_std,
                TimeUsed = Product.time_used,
                Type = Product.type,
                Description = Product.description,
                MonthAvailable = Product.month_available,
                PpmLocal = Product.ppm_local,
                Price = Product.price,
                ProductName = Product.product_name,
                ProductId = Product.product_id,
                Status = Product.status
            };
        }

        [HttpPost]
        [Route("SaveProductDetail/{id}")]
        public void SaveProductDetail(int id,ProductModel model)
        {
            var Product = _service.GetProductById(id);
            Product.connection_group_id = model.ConnectionGroupId;
            Product.time_type = model.TimeType;
            Product.ppm_mobile = model.PpmMobile;
            Product.ppm_std = model.PpmStd;
            Product.time_used = model.TimeUsed;
            Product.type = model.Type;
            Product.description = model.Description;
            Product.month_available = model.MonthAvailable;
            Product.ppm_local = model.PpmLocal;
            Product.price = model.Price;
            Product.product_name = model.ProductName;
            Product.status = model.Status;
            _service.SaveProduct(Product);
        }

        [HttpPost]
        [Route("CreateProductDetail")]
        public void CreateProductDetail(ProductModel model)
        {
            var Product = _service.GetProductById(0);
            if (!Equals(Product,null))
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "product_exist");
            }
            Product = new product();
            Product.connection_group_id = model.ConnectionGroupId;
            Product.time_type = model.TimeType;
            Product.ppm_mobile = model.PpmMobile;
            Product.ppm_std = model.PpmStd;
            Product.time_used = model.TimeUsed;
            Product.type = model.Type;
            Product.description = model.Description;
            Product.month_available = model.MonthAvailable;
            Product.ppm_local = model.PpmLocal;
            Product.price = model.Price;
            Product.product_name = model.ProductName;
            Product.product_id = model.ProductId;
            Product.status = model.Status;
            Product.product_id = 0;
            _service.SaveProduct(Product);
        }

        [HttpGet]
        [Route("GetListContract/{search?}")]
        public List<ContractModel> GetListContract(string search = null)
        {
            return _service.GetListContract(search).Select(p => new ContractModel()
            {
                Status = p.status,
                PpmStd = p.ppm_std,
                PpmLocal = p.ppm_local,
                PpmMobile = p.ppm_mobile,
                ProductId = p.product_id,
                ContractId = p.contract_id,
                EmployeeId = p.employee_id,
                EndDate = p.end_date,
                StartDate = p.start_date,
                SigningDate = p.signing_date,
                ProductName = p.product_name,
                ProductTimeUsed = p.product_time_used,
                 SecurityDeposit = p.security_deposit,
                Price = p.price,
                Note = p.note,
                StoreId = p.store_id,
                TimeUsedAvailable = p.time_used_available,
                UserId = p.user_id,
                ProductType = p.product_type
            }).ToList();
        }

        [HttpPost]
        [Route("CreateContract")]
        public void CreateContract(ContractModel model)
        {
            var Contract = _service.GetContractById(0);
            var Product = _service.GetProductById(model.ProductId);
            if (Equals(Contract,null))
            {
                ExceptionContent(HttpStatusCode.InternalServerError, "contract_exist");
            }
            Contract = new contract()
            {
                address = "",
                contract_id = 0,
                employee_id = model.EmployeeId,
                note = model.Note,
                ppm_local = model.PpmLocal,
                ppm_mobile = model.PpmMobile,
                ppm_std = model.PpmStd,
                product_id = model.ProductId,
                price = model.Price,
                product_name = Product.product_name,
                product_time_used = Product.time_used,
                product_type = Product.time_type,
                security_deposit = _service.GetSecurityDepositByProductId(model.ProductId),
                signing_date = ConvertDatetime.GetCurrentUnixTimeStamp(),
                status = 1,
                store_id = model.StoreId,
                time_used_available = Product.time_used,
                user_id  = model.UserId,
                start_date = model.StartDate,
                end_date = ConvertDatetime.GetEndDateWithMonthUsed(model.StartDate,Product.month_available)
            };
            _service.SaveContract(Contract);
        }

        [HttpGet]
        [Route("GetContractDetail/{id}")]
        public ContractModel GetContractDetail(int id)
        {
            var Contract = _service.GetContractById(id);
            return new ContractModel()
            {
                PpmStd = Contract.ppm_std,
                Status = Contract.status,
                StartDate = Contract.start_date,
                ProductId = Contract.product_id,
                Address = Contract.address,
                PpmLocal = Contract.ppm_local,
                PpmMobile = Contract.ppm_mobile,
                Price = Contract.price,
                ProductName = Contract.product_name,
                SecurityDeposit = Contract.security_deposit,
                Note = Contract.note,
                StoreId = Contract.store_id,
                EmployeeId = Contract.employee_id,
                ProductType = Contract.product_type,
                TimeUsedAvailable = Contract.time_used_available,
                SigningDate = Contract.signing_date,
                EndDate = Contract.end_date,
                UserId = Contract.user_id,
                ProductTimeUsed = Contract.product_time_used,
                ContractId = Contract.contract_id
            };
        }

        [HttpPost]
        [Route("SaveContract/{id}")]
        public void SaveContract(int id, ContractModel model)
        {
            var Contract = _service.GetContractById(id);
            Contract.status = model.Status;
            Contract.note = model.Note;
            _service.SaveContract(Contract);
        }


    }
}