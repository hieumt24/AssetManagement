﻿using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssignmentRepository : BaseRepositoryAsync<Assignment>, IAssignmentRepositoriesAsync
    {
        public AssignmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Assignment>> FilterAssignmentAsync(EnumLocation location, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate)
        {
            var includeAssignment = _dbContext.Assignments.Include(x => x.Asset).Include(x => x.AssignedBy).Include(x => x.AssignedTo);
            var query = includeAssignment.Where(x => x.Location == location).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Asset.AssetCode.ToLower().Contains(search.ToLower())
                                        || x.Asset.AssetName.ToLower().Contains(search.ToLower())
                                        || x.AssignedBy.Username.ToLower().Contains(search.ToLower())
                                        || x.AssignedTo.Username.ToLower().Contains(search.ToLower()));
            }
            if (assignmentState.HasValue)
            {
                query = query.Where(x => x.State == assignmentState);
            }
            if (assignedDate.HasValue)
            {
                query = query.Where(x => x.AssignedDate.Date == assignedDate.Value.Date);
            }
            // if state return request is complete not display in list assignment
            query = query.Where(x => x.ReturnRequest == null || x.ReturnRequest.ReturnState != EnumReturnRequestState.Completed);
            return query;
        }

        public async Task<IQueryable<Assignment>> GetAssignmentsByUserId(Guid userId)
        {
            return _dbContext.Assignments.Include(x => x.Asset).Where(x => x.AssignedIdTo == userId);
        }

        public async Task<Assignment> GetAssignemntByIdAsync(Guid assignmentId)
        {
            return await _dbContext.Assignments.Include(x => x.Asset)
                                         .Include(x => x.AssignedTo)
                                         .Include(x => x.AssignedBy)
                                         .Where(x => x.Id == assignmentId).FirstOrDefaultAsync();
        }

        public async Task<Assignment> FindExitingAssignment(Guid assetId)
        {
            return await _dbContext.Set<Assignment>()
                .FirstOrDefaultAsync(assigment => assigment.AssetId == assetId);
        }

        public async Task<IQueryable<Assignment>> FilterAssignmentOfUserAsync(Guid userId, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate)
        {
            var query = _dbContext.Assignments
                        .Include(x => x.Asset)
                        .Where(x => x.AssignedIdTo == userId)
                        .Where(x => x.ReturnRequest == null || x.ReturnRequest.ReturnState != EnumReturnRequestState.Completed)
                        .Where(x => x.State != EnumAssignmentState.Declined);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Asset.AssetCode.ToLower().Contains(search.ToLower())
                                                       || x.Asset.AssetName.ToLower().Contains(search.ToLower())
                                                       || x.AssignedTo.Username.ToLower().Contains(search.ToLower()));
            }
            if (assignmentState.HasValue)
            {
                query = query.Where(x => x.State == assignmentState);
            }
            if (assignedDate.HasValue)
            {
                query = query.Where(x => x.AssignedDate.Date == assignedDate.Value.Date);
            }
            query = query.Where(x => x.ReturnRequest == null || x.ReturnRequest.ReturnState != EnumReturnRequestState.Completed);

            return query;
        }
    }
}