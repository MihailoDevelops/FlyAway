using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        private readonly FlyAwayContext _context;
        private readonly UpdateUserValidator _validator;

        public EfUpdateUserCommand(FlyAwayContext context, UpdateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 11;

        public string Name => "Update User Command using EF";

        public void Execute(CreateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = _context.Users.Find(request.Id);
            user.Username = request.Username;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Password = request.Password;
            _context.SaveChanges();
        }
    }
}
