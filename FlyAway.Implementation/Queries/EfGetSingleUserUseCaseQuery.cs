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
    public class EfGetSingleUserUseCaseQuery : IGetSingleUserUseCaseQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSingleUserUseCaseQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 32;

        public string Name => "Get single userusecase using EF";

        public UserUseCaseDto Execute(int search)
        {
            var userUseCase = _context.UserUseCases.Find(search);

            if (userUseCase == null)
                throw new EntityNotFoundException(search, typeof(UserUseCase));

            return new UserUseCaseDto
            {
                Id = userUseCase.Id,
                UserId = userUseCase.UserId,
                UseCaseId = userUseCase.UseCaseId,
            };
        }
    }
}
