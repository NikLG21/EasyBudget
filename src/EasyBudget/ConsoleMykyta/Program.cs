using System;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Access;
using DataAccess.Queries;
using EasyBudget.Business.Services;
using EasyBudget.Common.Business.Services;
using EasyBudget.Common.DataAccess;
using EasyBudget.Common.Exceptions;
using EasyBudget.Common.Model;
using EasyBudget.Common.Model.Security;

namespace ConsoleMykyta
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IUserService userService = new UserService(new UserAccess(), new UserQueries());

                User user = new User()
                {
                    Id = Guid.Parse("009D1E72-BD33-41F1-B449-BA3D3754AB3C"),
                    IsDisabled = false,
                    Login = "1234",
                    Name = "Григорий Сковорода",
                    Password = "skovoroda01",
                    Roles = new List<Role>(),
                    Unit = new Unit()
                    {
                        Id = Guid.NewGuid(),
                        Name = "1"
                    }
                };
                //userService.AddUserByAdmin(user);
                //userService.ChangePasswordByUser(Guid.Parse("009D1E72-BD33-41F1-B449-BA3D3754AB3C"), "skovoroda01","skovoroda02");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //IDepartmentAccess departmentAccess = new DepartmentAccess();
            //Department department = new Department()
            //{
            //    Id = Guid.Parse("DF8E3D8F-4B48-4489-AAA6-53348267394A"),
            //    Name = "Хозчасть"
            //};
            //departmentAccess.Delete(Guid.Parse("DF8E3D8F-4B48-4489-AAA6-53348267394A"));
            return;

            IBudgetRequestAccess access = new BudgetRequestAccess();
            BudgetRequest request = new BudgetRequest()

            {
                Id = Guid.NewGuid(),
                Name = "Class",
                DateRequested = DateTime.Now,
                DateRequestedDeadline = DateTime.MaxValue,
                DateDirectorApprove = DateTime.Today,
                DateStartExecution = DateTime.Now,
                DateDeadlineExecution = DateTime.Now,
                DateEndExecution = DateTime.Now,
                RealPrice = 100,
                EstimatedPrice = 120,
                State = BudgetState.ApprovedDirector,
                //BudgetDescriptions =
                //{
                //    new BudgetDescription()
                //    {
                //        Date = DateTime.Today,
                //        Description = String.Empty,
                //        Id = Guid.NewGuid(),
                //        User = new User()
                //        {
                //            Id = Guid.NewGuid(),
                //            Login = "log",
                //            Name = "g",
                //            Password = "dfd"
                //        }
                //    }
                //}
                
            };

            access.Add(request);
            //Guid id = request.Id;
            //BudgetRequest budgetRequest = access.Get(id);
            //Console.WriteLine(budgetRequest.Id);
        }
    }
}
