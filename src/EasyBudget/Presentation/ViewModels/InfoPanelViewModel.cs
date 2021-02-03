using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Presentation.Interfaces;

namespace EasyBudget.Presentation.ViewModels
{
    public class InfoPanelViewModel : IInfoPanelViewModel
    {
        public UserMainInfoDto UserInfo { get; set; }

        public void LoadData()
        {
            UserInfo = new UserMainInfoDto(Guid.Parse("3148ce2c-540e-4cc4-a372-42e0c29a478b"))
            {
                Login = "dd",
                Name = "Дені Діпсон",
                CurrentRoleId = Guid.Parse("aab78899-6781-4a42-b7a0-18c18ca652d4"),
                CurrentRoleName = "Director",
            };
        }
    }
}
