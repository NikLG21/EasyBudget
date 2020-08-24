using System;
using System.Collections.Generic;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void Add(User user);
        Guid LogIn(string login, string password);
        List<string> GetActions(User user);
        void UpdatePassword(Guid userId, string oldPassword, string newPassword);
        void Update(User user);
    }
}
