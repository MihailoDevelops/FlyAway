using FlyAway.Application.DataTransfer;
using FlyAway.Application.Exceptions;
using FlyAway.Application.Queries;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetSingleTagQuery : IGetSingleTagQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetSingleTagQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 24;

        public string Name => "Get single tag using EF";

        public TagDto Execute(int search)
        {
            var tag = _context.Tags.Find(search);

            if (tag == null)
                throw new EntityNotFoundException(search, typeof(Tag));

            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}
