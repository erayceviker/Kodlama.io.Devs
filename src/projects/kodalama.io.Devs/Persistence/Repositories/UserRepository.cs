using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User,BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in Context.Set<OperationClaim>()
                join userOperationClaim in Context.Set<UserOperationClaim>()
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
        }

        public async Task<User> GetByMail(string mail)
        {
            return await GetAsync(u => u.Email == mail);
        }
    }
}
