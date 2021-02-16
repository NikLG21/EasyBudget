using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EasyBudget.Common.Business.Services.UserServices;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services.UserServices
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IUserAccess _userAccess;
        private readonly IUserQueries _userQueries;

        public AdminUserService(IUserAccess userAccess, IUserQueries userQueries)
        {
            _userAccess = userAccess;
            _userQueries = userQueries;
        }
        public void AddUserByAdmin(Guid userId, User user)
        {
            try
            {
                if (!CheckUnityInUser(user))
                {
                    throw new UnityInUserRequiredException();
                }
                if (user.Name == null)
                {
                    throw new LackMandatoryInformation("Їм'я");
                }
                if (user.Login == null)
                {
                    throw new LackMandatoryInformation("Логін");
                }
                if (user.Password == null)
                {
                    throw new LackMandatoryInformation("Пароль");
                }

                user.IsDisabled = false;
                user = GetUserPasswordHash(user);
                _userAccess.Add(user);
            }
            catch (DuplicateEntryException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public void UpdateUserByAdmin(Guid userId, User user)
        {
            try
            {
                if (_userAccess.Get(user.Id).Login != user.Login)
                {
                    throw new NonChangedLoginException();
                }

                if (!CheckUnityInUser(user))
                {
                    throw new UnityInUserRequiredException();
                }
                _userAccess.Update(user);
            }
            catch (DuplicateEntryException)
            {
                throw;
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public List<UserMainInfoDto> GetUsersList(Guid userId)
        {
            try
            {
                return _userQueries.GetUsers();
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        private static User GetUserPasswordHash(User user)
        {
            string userPassword = user.Password;

            var userPasswordHash = GetHash(userPassword);

            user.Password = userPasswordHash;
            return user;
        }

        private static string GetHash(string str)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] input = Encoding.Unicode.GetBytes(str);
            byte[] output = sha1.ComputeHash(input);
            string hash = Convert.ToBase64String(output);
            return hash;
        }

        private static bool CheckUnityInUser(User user)
        {
            foreach (Role role in user.Roles)
            {
                if (role.Name == RoleNames.Requester | role.Name == RoleNames.Approver)
                {
                    //TODO: Please use  smart statement
                    if (user.Unit != null)
                    {
                        return true;
                    }

                    return false;
                }
            }

            //TODO: Please use smart statement
            if (user.Unit == null)
            {
                return true;
            }
            
            return false;
        }
    }
}
