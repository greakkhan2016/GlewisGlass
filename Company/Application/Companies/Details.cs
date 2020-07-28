﻿using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies
{
    public class Details
    {
      public class Query: IRequest<Company>
      {
            public int Id { get; set; }
      }


        public class Handler : IRequestHandler<Query, Company>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
               _context = context;
            }
            public async Task<Company> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var activity = await _context.Companies.FindAsync(request.Id);

                return activity;
            }
        }
    }
}
