using FluentValidation;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using FlyAway.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    public class EfCreateTagCommand : ICreateTagCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateTagValidator _validator;

        public EfCreateTagCommand(FlyAwayContext context, CreateTagValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Create New Tag using EF";

        public void Execute(TagDto request)
        {
            _validator.ValidateAndThrow(request);

            var tag = new Tag
            {
                Name = request.Name,
            };

            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}
