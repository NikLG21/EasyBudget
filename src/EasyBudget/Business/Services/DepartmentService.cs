using System;
using System.Collections.Generic;
using System.Text;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;

namespace EasyBudget.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentAccess _departmentAccess;

        public DepartmentService(IDepartmentAccess departmentAccess)
        {
            _departmentAccess = departmentAccess;
        }

        public List<Department> GetAllDepartments()
        {
            try
            {
                return _departmentAccess.GetAll();
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
