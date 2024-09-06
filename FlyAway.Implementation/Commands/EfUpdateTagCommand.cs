using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfUpdateTagCommand : IUpdateTagCommand
    {
        private readonly FlyAwayContext _context;
        private readonly UpdateTagValidator _validator;

        public EfUpdateTagCommand(FlyAwayContext context, UpdateTagValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Update Tag Command using EF";

        public void Execute(TagDto request)
        {
            _validator.ValidateAndThrow(request);

            var tag = _context.Categories.Find(request.Id);
            tag.Name = request.Name;
            _context.SaveChanges();
        }
    }
}
