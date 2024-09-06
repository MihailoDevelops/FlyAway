using Azure;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FlyAway.Implementation.Queries
{
    public class EfGetSingleCommentQuery : IGetSingleCommentQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSingleCommentQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 26;

        public string Name => "Get single comment using EF";

        public ReadCommentDto Execute(int search)
        {
            var comment = _context.Comments.Find(search);
            if (comment == null) 
                throw new EntityNotFoundException(search, typeof(Comment));

            return new ReadCommentDto
            {   
                Id = comment.Id,
                Content = comment.Content,
                PublishedBy = _context.Users.FirstOrDefault(x => x.Id == comment.UserId).Username,
                PublishedAt = comment.PublishedAt,
                CommentPostTitle = _context.Posts.FirstOrDefault(x => x.Id == comment.PostId).Title,
                TotalLikes = _context.CommentLikes.Select(x => x.CommentId == comment.Id).Count()
            };
        }
    }
}
