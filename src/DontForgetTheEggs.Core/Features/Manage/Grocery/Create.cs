﻿using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Data;
using DontForgetTheEggs.Core.Features.Manage.Category;
using FluentValidation;
using MediatR;

namespace DontForgetTheEggs.Core.Features.Manage.Grocery
{
    public static class Create
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public int? CategoryId { get; set; }
            public string CategoryName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull();
            }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly EggsDbContext _dbContext;

            public CommandHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _dbContext.Categories.FindOrCreateCategory(request.CategoryId, request.CategoryName, cancellationToken)
                    .ConfigureAwait(false);

                var grocery = new Data.Grocery
                {
                    Name = request.Name,
                    Category = category
                };

                _dbContext.Groceries.Add(grocery);
                await _dbContext.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return grocery.Id;
            }
        }
    }
}