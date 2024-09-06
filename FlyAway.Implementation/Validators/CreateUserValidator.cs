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
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(FlyAwayContext context) 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can't be empty.")
                .MinimumLength(3).WithMessage("Username can't be under three characters.")
                .MaximumLength(15).WithMessage("Username can't have more than 15 characters.").DependentRules(() =>
                {
                    RuleFor(x => x.Username).Must((dto, username) => !context.Users.Any(x => x.Username == username && x.Id != dto.Id))
                    .WithMessage("An user with that username already exists.");
                });

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name can't be empty.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Username can't be empty.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty.")
                .MinimumLength(5).WithMessage("Password can't be under five characters.");
        }
    }
}
