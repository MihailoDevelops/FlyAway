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
    public class UpdatePostValidator : AbstractValidator<CreatePostDto>
    {
        public UpdatePostValidator(FlyAwayContext context)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Post title can't be empty.")
                .MaximumLength(100).WithMessage("Post title can't have more than 15 characters.")
                .Must(title => !context.Posts.Any(x => x.Title == title)).WithMessage("A post with that title already exists.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Post content can't be empty.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Post image url can't be empty.");
            RuleFor(x => x.PublishedAt).Empty().WithMessage("You can't edit a post's publishing time.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Post author can't be empty.").DependentRules(() =>
            {
                RuleFor(x => x.UserId).Must(id => context.Users.Any(x => x.Id == id)).WithMessage("An user with that ID doesn't exist.");
            });
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Post category can't be empty.").DependentRules(() =>
            {
                RuleFor(x => x.CategoryId).Must(id => context.Categories.Any(x => x.Id == id)).WithMessage("A category with that ID doesn't exist.");
            });
            RuleForEach(x => x.Tags).Must(id => context.Tags.Any(x => x.Id == id)).WithMessage("A tag with that ID doesn't exist");
        }
    }
}
