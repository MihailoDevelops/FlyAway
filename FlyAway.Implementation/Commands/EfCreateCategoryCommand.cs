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
using System.Windows.Input;

namespace FlyAway.Implementation.Commands
{
    public class EfCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly FlyAwayContext _context;
        private readonly CreateCategoryValidator _validator;

        public EfCreateCategoryCommand(FlyAwayContext context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Create New Category using EF";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            
            var category = new Category
            {
                Name = request.Name,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}
