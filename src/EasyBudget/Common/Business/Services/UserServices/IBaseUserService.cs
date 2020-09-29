using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.UserServices
{
    public interface IBaseUserService
    {
        Guid LogInUser(Guid userId, string login, string password);
        void ChangePasswordByUser(Guid userId, string oldPassword, string newPassword);
        UserMainInfoDto GetMainInfoDto(Guid userId, Guid id);
        User GetUser(Guid userId, Guid id);
    }
}
