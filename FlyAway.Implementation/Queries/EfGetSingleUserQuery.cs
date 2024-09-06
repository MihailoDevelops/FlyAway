using FlyAway.Application.DataTransfer;
using FlyAway.Application.Exceptions;
using FlyAway.Application.Queries;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetSingleUserQuery : IGetSingleUserQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSingleUserQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 27;

        public string Name => "Get single user using EF";

        public ReadUserDto Execute(int search)
        {
            var user = _context.Users.Find(search);

            if (user == null)
                throw new EntityNotFoundException(search, typeof(User));

            return new ReadUserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
