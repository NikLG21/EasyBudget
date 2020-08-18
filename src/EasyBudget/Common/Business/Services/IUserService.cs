using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model.Security;
using Action = System.Action;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void UserLogIn(string login, string password);
        void UserLogOut(User user);
        List<Action> GetActions(User user);
        void UserChangePassword(User user, string oldPassword, string newPassword);
    }
}
