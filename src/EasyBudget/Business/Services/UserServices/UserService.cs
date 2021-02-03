using System;
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
    public class UserService : IUserService
    {
        private readonly IUserAccess _userAccess;
        private readonly IUserQueries _userQueries;

        public UserService(IUserAccess userAccess, IUserQueries userQueries)
        {
            _userAccess = userAccess;
            _userQueries = userQueries;
        }
        public Guid LogInUser(Guid userId, string login, string password)
        {
            try
            {
                Guid id = _userQueries.GetUserByLogin(login, GetHash(password));
                if (id == Guid.Empty)
                {
                    throw new UnityInUserRequiredException();
                }

                if (_userAccess.Get(id).IsDisabled)
                {
                    throw new DisabledUserException();
                }
                return id;
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

        public void ChangePasswordByUser(Guid userId, string oldPassword, string newPassword)
        {
            try
            {
                User user = _userAccess.Get(userId);
                if (GetHash(oldPassword) == user.Password)
                {
                    user.Password = newPassword;
                    user = GetUserPasswordHash(user);
                    _userAccess.Update(user);
                }
                else
                {
                    throw new WrongPasswordException();
                }
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

        public UserMainInfoDto GetUserMainInfoDto(Guid userId, Guid id)
        {
            try
            {
                return _userQueries.GetMainInfo(id);
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

        public User GetUser(Guid userId, Guid id)
        {
            try
            {
                return _userAccess.Get(id);
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
    }
}
