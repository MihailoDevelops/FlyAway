using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using FlyAway.Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfCreatePostCommand : ICreatePostCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreatePostValidator _validator;

        public EfCreatePostCommand(FlyAwayContext context, CreatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Create New Tag using EF";

        public void Execute(CreatePostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                PublishedAt = DateTime.UtcNow,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            foreach (var tag in request.Tags)
            {
                _context.PostTags.Add(new PostTag
                {
                    PostId = post.Id,
                    TagId = tag
                });
                _context.SaveChanges();
            }
        }
    }
}
