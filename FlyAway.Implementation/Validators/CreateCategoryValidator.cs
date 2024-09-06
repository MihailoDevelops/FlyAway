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
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(FlyAwayContext context)
        {
            RuleFor(x => x.Name).MaximumLength(15)
                .WithMessage("Category name can't have more than 15 characters.")
                .NotEmpty().WithMessage("Category name can't be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(name => !context.Categories.Any(x => x.Name == name))
                            .WithMessage("A category with that name already exists.");
                });
        }
    }
}
