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
    public class EfGetTagsQuery : IGetTagsQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetTagsQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 8;

        public string Name => "Tags search";

        public PageResponse<TagDto> Execute(TagSearch search)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<TagDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new TagDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return response;
        }
    }
}
