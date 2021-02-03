using System;
using System.Collections.Generic;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentAccess _departmentAccess;
        private readonly IDepartmentQueries _departmentQueries;

        public DepartmentService(IDepartmentAccess departmentAccess, IDepartmentQueries departmentQueries)
        {
            _departmentAccess = departmentAccess;
            _departmentQueries = departmentQueries;
        }

        public List<Department> GetAllDepartments()
        {
            try
            {
                return _departmentQueries.GetAll();
            }
            catch (CriticalException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw  new CriticalException(e);
            }
        }
    }
}
