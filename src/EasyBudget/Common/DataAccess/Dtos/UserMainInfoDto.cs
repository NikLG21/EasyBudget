using System;
using System.Collections.Generic;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace EasyBudget.Common.DataAccess.Dtos
{
    public class UserMainInfoDto : Entity
    {
        public UserMainInfoDto()
        {
            Roles = new List<Role>();
        }

        public string Name { get; set; }
        public string Login { get; set; }
        public List<Role> Roles { get; set; }

        public Guid CurrentRoleId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid UnitId { get; set; }

        public string CurrentRoleName { get; set; }
        public string DepartmentName { get; set; }
        public string UnitName { get; set; }
    }
}
