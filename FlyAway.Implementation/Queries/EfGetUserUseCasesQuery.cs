using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetUserUseCasesQuery : IGetUserUseCasesQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetUserUseCasesQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 31;

        public string Name => "UserUseCases search";

        public PageResponse<UserUseCaseDto> Execute(UserUseCaseSearch search)
        {
            var query = _context.UserUseCases.AsQueryable();

            if (search.UserId != null)
                query = query.Where(x => x.UserId == search.UserId);

            if (search.UseCaseId != null)
                query = query.Where(x => x.UseCaseId == search.UseCaseId);

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<UserUseCaseDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new UserUseCaseDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UseCaseId = x.UseCaseId
                }).ToList()
            };

            return response;
        }
    }
}
