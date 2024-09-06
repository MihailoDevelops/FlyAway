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
    public class EfDeletePostCommand : IDeletePostCommand
    {
        private readonly FlyAwayContext _context;

        public int Id => 14;

        public string Name => "Delete Post Command using EF";

        public void Execute(int request)
        {
            var post = _context.Posts.Find(request);

            if (post == null)
                throw new EntityNotFoundException(request, typeof(Post));

            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
