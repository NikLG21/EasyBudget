using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.Business.Services.UserServices
{
    public interface IAdminUserService
    {
        void AddUserByAdmin(Guid userId, User user);
        //TODO: Probably better UpdateUserByAdmin. Done
        void UpdateUserByAdmin(Guid userId, User user);
        List<UserMainInfoDto> GetUsersList(Guid userId);
    }
}
