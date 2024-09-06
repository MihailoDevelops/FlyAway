using FlyAway.Application.DataTransfer;
using FlyAway.Application.Exceptions;
using FlyAway.Application.Queries;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetSinglePostQuery : IGetSinglePostQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSinglePostQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 25;

        public string Name => "Get single post using EF";

        public ReadPostDto Execute(int search)
        {
            var post = _context.Posts.Find(search);

            if (post == null)
                throw new EntityNotFoundException(search, typeof(Post));

            return new ReadPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                PublishedAt = post.PublishedAt,
                CategoryName = _context.Categories.FirstOrDefault(x => x.Id == post.CategoryId).Name,
                Author = _context.Users.FirstOrDefault(x => x.Id == post.UserId).Username,
                Tags =  _context.PostTags.Where(pt => pt.PostId == post.Id).Select(pt => pt.Tag.Name).ToList(),
                TotalComments = _context.Comments.Select(x => x.PostId == post.Id).Count(),
                Comments = _context.Comments.Where(x => x.PostId == post.Id).Select(x => new ReadCommentDto
                    {
                        Id = x.Id,
                        Content = x.Content,
                        PublishedBy = x.User.Username,
                        PublishedAt = x.PublishedAt,
                        CommentPostTitle = x.Post.Title,
                        TotalLikes = x.CommentLikes.Count()
                }).ToList()
            };
        }
    }
}
