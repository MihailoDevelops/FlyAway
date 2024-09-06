using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using FlyAway.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetPostsQuery : IGetPostsQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetPostsQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Posts search";

        public PageResponse<ReadPostDto> Execute(PostSearch search)
        {
            var query = _context.Posts.Include(x => x.Comments).Include(x => x.Category)
                .Include(x => x.User).Include(x => x.PostTags).ThenInclude(x => x.Tag).AsQueryable();

            if (search.Keyword != null)
                query = query.Where(x => x.Title.ToLower().Contains(search.Keyword.ToLower()) ||
                    x.Content.ToLower().Contains(search.Keyword.ToLower()));

            if (search.Title != null)
                query = query.Where(x => x.Title.ToLower().Contains(search.Title.ToLower()));

            if (search.Content != null)
                query = query.Where(x => x.Content.ToLower().Contains(search.Content.ToLower()));

            if (search.PublishedBefore != null)
                query = query.Where(x => x.PublishedAt < search.PublishedBefore);

            if (search.PublishedAfter != null)
                query = query.Where(x => x.PublishedAt > search.PublishedAfter);

            if(search.UserId != null)
                query = query.Where(x => x.UserId == search.UserId);

            if (search.CategoryId != null)
                query = query.Where(x => x.UserId == search.CategoryId);

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadPostDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadPostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImageUrl = x.ImageUrl,
                    PublishedAt = x.PublishedAt,
                    CategoryName = x.Category.Name,
                    Author = x.User.Username,
                    Tags = x.PostTags.Where(pt => pt.PostId == x.Id).Select(pt => pt.Tag.Name).ToList(),
                    TotalComments = x.Comments.Count(),
                    Comments = x.Comments.Select(c => new ReadCommentDto
                    {
                        Id = c.Id,
                        Content = c.Content,
                        PublishedBy = c.User.Username,
                        PublishedAt = c.PublishedAt,
                        CommentPostTitle = c.Post.Title,
                        TotalLikes = c.CommentLikes.Count()
                    }).ToList(),
                }).ToList()
            };

            return response;
        }
    }
}
