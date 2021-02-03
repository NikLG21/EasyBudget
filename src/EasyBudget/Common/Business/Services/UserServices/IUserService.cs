using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.UserServices
{
    public interface IUserService
    {
        Guid LogInUser(Guid userId, string login, string password);
        void ChangePasswordByUser(Guid userId, string oldPassword, string newPassword);
        //TODO: Probably GetUserMainInfoDto. Done
        UserMainInfoDto GetUserMainInfoDto(Guid userId, Guid id);
        User GetUser(Guid userId, Guid id);
    }
}
