using FluentValidation;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Validators
{
    public class CreateCommentLikeValidator : AbstractValidator<CreateCommentLikeDto>
    {
        public CreateCommentLikeValidator(FlyAwayContext context) 
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID can't be empty").DependentRules(() =>
            {
                RuleFor(x => x.UserId).Must(id => context.Users.Any(x => x.Id == id)).WithMessage("An user with that ID doesn't exist.");
            });
            RuleFor(x => x.CommentId).NotEmpty().WithMessage("Comment ID can't be empty").DependentRules(() =>
            {
                RuleFor(x => x.CommentId).Must(id => context.Comments.Any(x => x.Id == id)).WithMessage("A comment with that ID doesn't exist.");
            });
            RuleFor(x => x.UserId).Must((dto, id) => !context.CommentLikes.Any(x => x.UserId == id && x.CommentId == dto.CommentId))
                .WithMessage("That user has already liked the post with that ID.");
        }
    }
}
