﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Utils;
using EasyBudget.Common.DataAccess.Dtos;
using EasyBudget.Common.DataAccess.Queries;
using EasyBudget.Common.Exceptions;

namespace DataAccess.Queries
{
    public class BudgetRequestListQueries : IBudgetRequestListQueries
    {
        private readonly IBudgetRequestDbContextFactory _factory;

        public BudgetRequestListQueries(IBudgetRequestDbContextFactory factory)
        {
            _factory = factory;
        }

        public List<BudgetRequestMainListDto> GetAllDirectorRequests(DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.DateRequested >= from )
                        .Select(br => new BudgetRequestMainListDto(br.Id)
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name,
                            RequesterId = br.Requester.Id,
                            UnitId = br.Unit.Id,
                            DepartmentId = br.Department.Id,
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
        public List<BudgetRequestMainListDto> GetAllRequesterRequests(Guid userId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Requester.Id == userId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto(br.Id)
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name,
                            RequesterId = br.Requester.Id,
                            UnitId = br.Unit.Id,
                            DepartmentId = br.Department.Id,
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
        public List<BudgetRequestMainListDto> GetAllApproverRequests(Guid unitId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Unit.Id == unitId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto(br.Id)
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name,
                            RequesterId = br.Requester.Id,
                            UnitId = br.Unit.Id,
                            DepartmentId = br.Department.Id,
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }

        }
        public List<BudgetRequestMainListDto> GetAllExecutorRequests(Guid departmentId, DateTime from)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context
                        .BudgetRequests
                        .AsNoTracking()
                        .Where(br => br.Department.Id == departmentId)
                        .Where(br => br.DateRequested >= from)
                        .Select(br => new BudgetRequestMainListDto(br.Id)
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name,
                            RequesterId = br.Requester.Id,
                            UnitId = br.Unit.Id,
                            DepartmentId = br.Department.Id,
                        }).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
        public List<BudgetRequestMainListDto> GetListByIds(List<Guid> ids)
        {
            using (BudgetRequestDbContext context = _factory.Create())
            {
                try
                {
                    List<BudgetRequestMainListDto> list = context.BudgetRequests
                        .AsNoTracking()
                        .Where(bd => ids.Contains(bd.Id))
                        .Select(br => new BudgetRequestMainListDto(br.Id)
                        {
                            Name = br.Name,
                            RequesterName = br.Requester.Name,
                            DepartmentName = br.Department.Name,
                            DateRequested = br.DateRequested,
                            State = br.State,
                            RealPrice = br.RealPrice,
                            UnitName = br.Unit.Name,
                            RequesterId = br.Requester.Id,
                            UnitId = br.Unit.Id,
                            DepartmentId = br.Department.Id,
                        })
                        .ToList();
                    return list;
                }
                catch (Exception e)
                {
                    throw new CriticalException(e);
                }
            }
        }
    }
}
