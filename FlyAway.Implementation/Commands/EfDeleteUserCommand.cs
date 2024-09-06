using FlyAway.Application.Commands;
using FlyAway.Application.Exceptions;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteUserCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 10;

        public string Name => "Delete User Command using EF";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
                throw new EntityNotFoundException(request, typeof(User));

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
