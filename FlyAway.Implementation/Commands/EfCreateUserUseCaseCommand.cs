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
    public class EfCreateUserUseCaseCommand : ICreateUserUseCaseCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateUserUseCaseValidator _validator;

        public EfCreateUserUseCaseCommand(FlyAwayContext context, CreateUserUseCaseValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Create New UserUseCase using EF";

        public void Execute(UserUseCaseDto request)
        {
            _validator.ValidateAndThrow(request);

            var userUseCase = new UserUseCase
            {
                UserId = request.UserId,
                UseCaseId = request.UseCaseId
            };

            _context.UserUseCases.Add(userUseCase);
            _context.SaveChanges();
        }
    }
}
