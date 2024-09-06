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
    public class EfDeleteTagCommand : IDeleteTagCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteTagCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Delete Tag Command using EF";

        public void Execute(int request)
        {
            var tag = _context.Tags.Find(request);

            if (tag == null)
                throw new EntityNotFoundException(request, typeof(Tag));

            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }
    }
}
