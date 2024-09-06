using Azure.Core;
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
    public class EfGetSingleCategoryQuery : IGetSingleCategoryQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSingleCategoryQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 23;

        public string Name => "Get single category using EF";

        public CategoryDto Execute(int search)
        {
            var category = _context.Categories.Find(search);

            if (category == null)
                throw new EntityNotFoundException(search, typeof(Category));

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
