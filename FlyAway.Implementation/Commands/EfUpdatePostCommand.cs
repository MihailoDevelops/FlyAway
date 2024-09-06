using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfUpdatePostCommand : IUpdatePostCommand
    {
        private readonly FlyAwayContext _context;
        private readonly UpdatePostValidator _validator;

        public EfUpdatePostCommand(FlyAwayContext context, UpdatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Update Post Command using EF";

        public void Execute(CreatePostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = _context.Posts.Find(request.Id);
            post.Title = request.Title;
            post.Content = request.Content;
            post.UserId = request.UserId;
            post.CategoryId = request.CategoryId;
            _context.SaveChanges();
        }
    }
}
