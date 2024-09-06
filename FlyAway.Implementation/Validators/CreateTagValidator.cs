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
    public class CreateTagValidator : AbstractValidator<TagDto>
    {
        public CreateTagValidator(FlyAwayContext context)
        {
            RuleFor(x => x.Name).MaximumLength(15)
                .WithMessage("Tag name can't have more than 15 characters.")
                .NotEmpty().WithMessage("Tag name can't be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(name => !context.Tags.Any(x => x.Name == name))
                            .WithMessage("A tag with that name already exists.");
                });
        }
    }
}
