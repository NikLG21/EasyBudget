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
        private readonly IUserAccess _userAccess;
        private readonly IUserQueries _userQueries;
        
        public UserService(
            IUserAccess userAccess,
            IUserQueries userQueries)
        {
            this._userAccess = userAccess;
            this._userQueries = userQueries;
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
                    throw  new LackMandatoryInformation("Їм'я");
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
                    throw new WrongPasswordException("зміни пароля");
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

        public void UpdateByAdmin(Guid userId, User user)
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

        public UserMainInfoDto GetMainInfoDto(Guid userId, Guid id)
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
                if (role.Name == "Инициатор запроса" | role.Name == "Утверждающий")
                {
                    if (user.Unit != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (user.Unit == null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
