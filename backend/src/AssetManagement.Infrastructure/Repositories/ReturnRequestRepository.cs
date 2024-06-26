﻿using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class ReturnRequestRepository : BaseRepositoryAsync<ReturnRequest>, IReturnRequestRepositoriesAsync
    {
        public ReturnRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<ReturnRequest>> FilterReturnRequestAsync(EnumLocation adminLocation, string? search, EnumReturnRequestState? returnState, DateTime? returnedDate)
        {
            var query = _dbContext.ReturnRequests
                .Include(x => x.AcceptedUser)
                .Include(x => x.RequestedUser)
                .Include(x => x.Assignment)
                .ThenInclude(a => a.Asset);
            var queryByAdmin = query.Where(x => x.Location == adminLocation);
            if (!string.IsNullOrEmpty(search))
            {
                queryByAdmin = query.Where(x => x.Assignment.Asset.AssetCode.ToLower().Contains(search.ToLower())
                || x.Assignment.Asset.AssetName.ToLower().Contains(search.ToLower())
                || x.RequestedUser.Username.ToLower().Contains(search.ToLower())
                );
            }
            if (returnState.HasValue)
            {
                queryByAdmin = query.Where(x => x.ReturnState == returnState);
            }
            if (returnedDate.HasValue)
            {
                queryByAdmin = query.Where(x => x.ReturnedDate.Date == returnedDate.Value.Date);
            }

            return queryByAdmin;
        }

        public async Task<ReturnRequest> GetReturnRequestByIdWithAssignmentAsync(Guid returnRequestId)
        {
            var returnRequestIncludeWithAssignment = _dbContext.ReturnRequests.Include(x => x.Assignment);
            var returnRequest = await returnRequestIncludeWithAssignment.FirstOrDefaultAsync(x => x.Id == returnRequestId);
            return returnRequest;
            
        }
    }
}