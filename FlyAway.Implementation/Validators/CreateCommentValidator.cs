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
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator(FlyAwayContext context)
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Comment content can't be empty");
            RuleFor(x => x.PostId).NotEmpty().WithMessage("Post ID can't be empty.").DependentRules(() =>
            {
                RuleFor(x => x.PostId).Must(id => context.Posts.Any(x => x.Id == id)).WithMessage("A post with that ID doesn't exist.");
            });
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID can't be empty.").DependentRules(() =>
            {
                RuleFor(x => x.UserId).Must(id => context.Users.Any(x => x.Id == id)).WithMessage("An user with that ID doesn't exist.");
            });
        }
    }
}
