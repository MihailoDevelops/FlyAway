using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetCommentLikesQuery : IGetCommentLikesQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetCommentLikesQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 22;

        public string Name => "CommentLike search";

        public PageResponse<ReadCommentLikeDto> Execute(CommentLikeSearch search)
        {
            var query = _context.CommentLikes.Include(x => x.User).Include(x => x.Comment).AsQueryable();

            foreach (var like in query)
            {
                Console.WriteLine($"User: {like.User?.Username}, Comment: {like.Comment?.Content}");
            }

            if (search.UserId != null)
                query = query.Where(x => x.UserId == search.UserId);

            if (search.CommentId != null)
                query = query.Where(x => x.CommentId == search.CommentId);

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadCommentLikeDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadCommentLikeDto
                {
                    UserLiked = x.User.Username,
                    CommentContent = x.Comment.Content
                }).ToList()
            };

            return response;
        }
    }
}
