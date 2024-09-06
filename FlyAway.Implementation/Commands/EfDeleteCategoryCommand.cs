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
    public class EfDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteCategoryCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 2;

        public string Name => "Delete Category Command using EF";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);

            if (category == null)
                throw new EntityNotFoundException(request, typeof(Category));

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
