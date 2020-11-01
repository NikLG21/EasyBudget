using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.Business.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
    }
}
