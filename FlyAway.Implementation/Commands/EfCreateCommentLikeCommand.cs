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
    public class EfCreateCommentLikeCommand : ICreateCommentLikeCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateCommentLikeValidator _validator;

        public EfCreateCommentLikeCommand(FlyAwayContext context, CreateCommentLikeValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Create New CommentLike using EF";

        public void Execute(CreateCommentLikeDto request)
        {
            _validator.ValidateAndThrow(request);

            var commentLike = new CommentLike
            {
                UserId = request.UserId,
                CommentId = request.CommentId,
            };

            _context.CommentLikes.Add(commentLike);
            _context.SaveChanges();
        }
    }
}
