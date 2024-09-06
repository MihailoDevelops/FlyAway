using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetCommentsQuery : IGetCommentsQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetCommentsQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 19;

        public string Name => "Comment search";

        public PageResponse<ReadCommentDto> Execute(CommentSearch search)
        {
            var query = _context.Comments.Include(x => x.User).Include(x => x.Post).Include(x => x.CommentLikes).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Content.ToLower().Contains(search.Keyword.ToLower()));

            if (search.Content != null)
                query = query.Where(x => x.Content.ToLower().Contains(search.Content.ToLower()));

            if (search.PublishedBefore != null)
                query = query.Where(x => x.PublishedAt < search.PublishedBefore);

            if (search.PublishedAfter != null)
                query = query.Where(x => x.PublishedAt > search.PublishedBefore);

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadCommentDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadCommentDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    PublishedBy = x.User.Username,
                    PublishedAt = x.PublishedAt,
                    CommentPostTitle = x.Post.Title,
                    TotalLikes = x.CommentLikes.Count()
                }).ToList()
            };

            return response;
        }
    }
}
