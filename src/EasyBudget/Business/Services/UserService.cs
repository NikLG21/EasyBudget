using System;
using System.Collections.Generic;
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
            userAccess.Add(user);
        }

        public Guid LogIn(string login, string password)
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

        public List<string> GetActions(User user)
        {
            return userQueries.GetUserActions(user.Id);
        }

        public void UpdatePassword(Guid userId, string oldPassword, string newPassword)
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

        public void Update(User user)
        {
            userAccess.Update(user);
        }

        public UserMainListDto GetMainListDto(Guid id)
        {
            return userQueries.GetMainInfo(id);
        }

        //public UserMainListDto GetUsersList()
        //{

        //}
    }
}
