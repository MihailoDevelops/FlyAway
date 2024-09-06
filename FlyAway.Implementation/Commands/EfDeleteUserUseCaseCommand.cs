using FlyAway.Application.Commands;
using FlyAway.Application.Exceptions;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfDeleteUserUseCaseCommand : IDeleteUserUseCaseCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteUserUseCaseCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 30;

        public string Name => "Delete UserUseCase Command using EF";

        public void Execute(int request)
        {
            var userUseCase = _context.UserUseCases.Find(request);

            if (userUseCase == null)
                throw new EntityNotFoundException(request, typeof(UserUseCase));

            _context.UserUseCases.Remove(userUseCase);
            _context.SaveChanges();
        }
    }
}
