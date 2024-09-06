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
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetUsersQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 12;

        public string Name => "Users search";

        public PageResponse<ReadUserDto> Execute(UserSearch search)
        {
            var query = _context.Users.AsQueryable();

            if(search.Keyword != null)
                query = query.Where(x => x.Username.ToLower().Contains(search.Keyword.ToLower()) ||
                    x.FirstName.ToLower().Contains(search.Keyword.ToLower()) || x.LastName.ToLower().Contains(search.Keyword.ToLower()));

            if (search.Username != null)
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));

            if (search.FirstName != null)
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));

            if (search.LastName != null)
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadUserDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadUserDto
                {
                    Id = x.Id,
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList()
            };

            return response;
        }
    }
}
