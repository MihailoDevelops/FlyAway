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
    public class CreateUserUseCaseValidator : AbstractValidator<UserUseCaseDto>
    {
        public CreateUserUseCaseValidator(FlyAwayContext context) 
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID can't be empty.").DependentRules(() =>
            {
                RuleFor(x => x.UserId).Must(id => context.Users.Any(x => x.Id == id)).WithMessage("An user with that ID doesn't exist.");
            }).WithMessage("A user with that ID doesn't exist.");
            RuleFor(x => x.UseCaseId).NotEmpty().WithMessage("UseCase ID can't be empty.");
        }
    }
}
