using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void UserLogIn(string nickname, string password);
        void UserLogOut(User user);
    }
}
