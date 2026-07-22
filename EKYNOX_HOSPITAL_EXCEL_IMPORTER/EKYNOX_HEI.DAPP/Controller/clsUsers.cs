using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.Users;
using EKYNOX_HEI.DATA.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DAPP.Controller
{
    public class clsUsers
    {
        private readonly DatabaseContext context;
        public clsUsers(DatabaseContext _context)
        {
            context = _context;
        }

        public ReturnData<int> CreateUserNr()
        {
            var result = new ReturnData<int>();
            try
            {
                var lastUser = context.Users.OrderByDescending(u => u.LOGICALREF).FirstOrDefault();
                var template = lastUser != null ? lastUser.NR + 1 : 1;
                result.Status = StatusEnum.Success;
                result.Data = template;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnData<List<UsersViewModel>> GetAllUsers()
        {
            var result = new ReturnData<List<UsersViewModel>>();
            try
            {
                var users = context.Users.Select(u => new UsersViewModel
                {
                    LogicalRef = u.LOGICALREF,
                    Nr = u.NR,
                    UserName = u.USERNAME,
                    Name = u.NAME,
                    Surname = u.SURNAME,
                    EMail = u.EMAIL,
                    Phone = u.PHONE,
                    Role = u.ROLE
                }).ToList();
                result.Status = StatusEnum.Success;
                result.Data = users;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnData<bool> DeleteUser(int userId)
        {
            var result = new ReturnData<bool>();

            try
            {
                var userControl = context.Users.Find(userId);
                if (userControl is null)
                {
                    result.Message = "Kullanıcı Bulunamadı.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Users.Remove(userControl);
                    context.SaveChanges();
                    transaction.Commit();
                }

                result.Status = StatusEnum.Success;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<bool> UpdateUser(UsersModel user)
        {
            var result = new ReturnData<bool>();
            try
            {
                var control = context.Users.FirstOrDefault(c => c.LOGICALREF != user.LogicalRef && c.USERNAME == user.UserName || c.EMAIL == user.EMail);
                if (control is not null)
                {
                    result.Message = "Aynı kullanıcı adı veya mail adresi tekrar kullanılamaz.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                var existingUser = context.Users.Find(user.LogicalRef);
                if (existingUser != null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        existingUser.USERNAME = user.UserName;
                        existingUser.NAME = user.Name;
                        existingUser.SURNAME = user.Surname;
                        existingUser.EMAIL = user.EMail;
                        existingUser.PHONE = user.Phone;
                        existingUser.ROLE = user.Role;

                        if (!string.IsNullOrEmpty(user.Password))
                            existingUser.PASSWORD = user.Password;

                        context.SaveChanges();
                        result.Status = StatusEnum.Success;
                        result.Data = true;
                        transaction.Commit();
                    }
                }
                else
                {
                    result.Status = StatusEnum.Error;
                    result.Message = "Kullanıcı Bulunamadı.";
                    result.Data = false;
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = false;
            }
            return result;
        }

        public void AdminUserControl()
        {
            var adminUser = context.Users.FirstOrDefault(u => u.USERNAME == "EKYNOX");
            if (adminUser is null)
            {
                var newAdminUser = new DATA.DataModel.Users
                {
                    NR = 1,
                    USERNAME = "EKYNOX",
                    NAME = "EKYNOX",
                    SURNAME = "EKYNOX",
                    PASSWORD = Cryptography.Encrypt("€KYN0X"),
                    EMAIL = "ekynox@ekynox.com",
                    ROLE = RoleEnum.Admin
                };

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Users.Add(newAdminUser);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public ReturnData<bool> AddUser(UsersModel user)
        {
            var result = new ReturnData<bool>();
            try
            {
                var control = context.Users.FirstOrDefault(c => c.USERNAME == user.UserName || c.EMAIL == user.EMail);
                if (control is not null)
                {
                    result.Message = "Aynı kullanıcı adı veya mail adresi tekrar kullanılamaz.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    var newUser = new DATA.DataModel.Users
                    {
                        NR = user.Nr,
                        USERNAME = user.UserName,
                        NAME = user.Name,
                        SURNAME = user.Surname,
                        PASSWORD = user.Password,
                        EMAIL = user.EMail,
                        PHONE = user.Phone,
                        ROLE = user.Role
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    transaction.Commit();
                }
                result.Status = StatusEnum.Success;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = false;
            }
            return result;
        }

        public ReturnData<UsersLoginModel> LoginControl(string username, string password)
        {
            var result = new ReturnData<UsersLoginModel>();
            try
            {
                var user = context.Users.FirstOrDefault(u => u.USERNAME == username);
                if (user is not null)
                {
                    if (!Cryptography.VerifyPassword(password, user.PASSWORD))
                    {
                        result.Status = StatusEnum.Warning;
                        result.Message = "Kullanıcı adı veya şifre hatalı.";
                        result.Data = null;
                        return result;
                    }

                    result.Status = StatusEnum.Success;
                    result.Data = new UsersLoginModel
                    {
                        LogicalRef = user.LOGICALREF,
                        Nr = user.NR,
                        UserName = user.USERNAME,
                        Name = user.NAME,
                        Surname = user.SURNAME,
                        EMail = user.EMAIL,
                        Phone = user.PHONE,
                        Role = user.ROLE,
                        Password = user.PASSWORD
                    };
                }
                else
                {
                    result.Status = StatusEnum.Warning;
                    result.Message = "Kullanıcı adı veya şifre hatalı.";
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = null;
            }
            return result;
        }
    }
}
