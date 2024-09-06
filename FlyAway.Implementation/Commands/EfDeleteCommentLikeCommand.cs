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
    public class EfDeleteCommentLikeCommand : IDeleteCommentLikeCommand
    {
        private readonly FlyAwayContext _context;

        public EfDeleteCommentLikeCommand(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 21;

        public string Name => "Delete CommentLike Command using EF";

        public void Execute(int request)
        {
            var commentLike = _context.CommentLikes.Find(request);

            if (commentLike == null)
                throw new EntityNotFoundException(request, typeof(CommentLike));

            _context.CommentLikes.Remove(commentLike);
            _context.SaveChanges();
        }
    }
}
