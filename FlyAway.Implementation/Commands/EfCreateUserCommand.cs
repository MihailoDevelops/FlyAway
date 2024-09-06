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
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateUserValidator _validator;

        public EfCreateUserCommand(FlyAwayContext context, CreateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create New User using EF";

        public void Execute(CreateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
