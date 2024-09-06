using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using FlyAway.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfCreateCommentCommand : ICreateCommentCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateCommentValidator _validator;

        public EfCreateCommentCommand(FlyAwayContext context, CreateCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Create New Comment using EF";

        public void Execute(CreateCommentDto request)
        {
            _validator.ValidateAndThrow(request);

            var comment = new Comment
            {
                Content = request.Content,
                PostId = request.PostId,
                UserId = request.UserId,
                PublishedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
