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
    public class EfDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteCommentCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Delete Comment Command using EF";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);

            if (comment == null)
                throw new EntityNotFoundException(request, typeof(Comment));

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
