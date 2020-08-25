using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Business.Services
{
    public class UserService :IUserService
    {
        private IUserAccess userAccess;
        private IUserQueries userQueries;

        public UserService(IUserAccess userAccess, IUserQueries userQueries)
        {
            this.userAccess = userAccess;
            this.userQueries = userQueries;
        }

        public void Add(User user)
        {
            try
            {
                GetUserPasswordHash(user);

                userAccess.Add(user);
            }
            catch (DuplicateEntryException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
            
        }

        public Guid LogIn(string login, string password)
        {
            try
            {
                Guid id = userQueries.GetUserByLogin(login, password);
                if (id == Guid.Empty)
                {
                    throw new WrongPasswordException("входа");
                }

                if (userAccess.Get(id).IsDisabled)
                {
                    throw new DisabledUserException();
                }
                return id;
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
            
        }

        public List<string> GetActions(User user)
        {
            try
            {
                return userQueries.GetUserActions(user.Id);
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
            
        }

        public void UpdatePassword(Guid userId, string oldPassword, string newPassword)
        {
            try
            {
                User user = userAccess.Get(userId);
                if (oldPassword == user.Password)
                {
                    user.Password = newPassword;
                    Update(user);
                }
                else
                {
                    throw new WrongPasswordException("смены пароля");
                }
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }

        }

        public void Update(User user)
        {
            try
            {
                userAccess.Update(user);
            }
            catch (DuplicateEntryException e)
            {
                throw new DuplicateEntryException(e.EntityName, e);
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public UserMainInfoDto GetMainListDto(Guid id)
        {
            try
            {
                return userQueries.GetMainInfo(id);
            }
            catch (Exception e)
            {
                throw new CriticalException(e);
            }
        }

        public List<UserMainInfoDto> GetUsersList()
        {
            try
            {
                return userQueries.GetUsers();
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

        private static void GetUserPasswordHash(User user)
        {
            string userPassword = user.Password;

            var userPasswordHash = GetHash(userPassword);

            user.Password = userPasswordHash;
        }

        private static string GetHash(string str)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] input = Encoding.Unicode.GetBytes(str);
            byte[] output = sha1.ComputeHash(input);
            string hash = Convert.ToBase64String(output);
            return hash;
        }
    }
}
