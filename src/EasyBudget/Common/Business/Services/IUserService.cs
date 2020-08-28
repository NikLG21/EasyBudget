using System;
using System.Collections.Generic;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services
{
    public interface IUserService
    {
        void AddUserByAdmin(Guid userId, User user);
        Guid LogInUser(Guid userId, string login, string password);
        void ChangePasswordByUser( Guid userId, string oldPassword, string newPassword);
        void UpdateByAdmin(Guid userId, User user);
        List<UserMainInfoDto> GetUsersList(Guid userId);
        UserMainInfoDto GetMainInfoDto(Guid userId, Guid id);
    }
}
