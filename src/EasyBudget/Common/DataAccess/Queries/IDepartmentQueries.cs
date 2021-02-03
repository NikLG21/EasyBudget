using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Model;

namespace EasyBudget.Common.DataAccess.Queries
{
    public interface IDepartmentQueries
    {
        List<Department> GetAll();
    }
}
