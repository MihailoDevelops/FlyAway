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
    public class UpdateTagValidator : AbstractValidator<TagDto>
    {
        public UpdateTagValidator(FlyAwayContext context)
        {
            RuleFor(x => x.Name).MaximumLength(15)
                .WithMessage("Tag name can't have more than 15 characters.")
                .NotEmpty().WithMessage("Tag name can't be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id).Must(pk => context.Tags.Any(x => x.Id == pk)).WithMessage("A Tag with that ID doesn't exist.").DependentRules(() =>
                    {
                        RuleFor(x => x.Name).Must(name => !context.Tags.Any(x => x.Name == name))
                            .WithMessage("A Tag with that name already exists.");
                    });
                });
        }
    }
}
