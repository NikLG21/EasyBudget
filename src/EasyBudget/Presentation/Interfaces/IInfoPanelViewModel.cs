using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.DataAccess.Dtos;

namespace EasyBudget.Presentation.Interfaces
{
    public interface IInfoPanelViewModel
    {
        UserMainInfoDto UserInfo { get; set; }
        void LoadData();
    }
}
