using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Exchange { get; set; }

            public string Ticker { get; set; }

            public string Isin { get; set; }

            public string Website { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request,
                CancellationToken cancellationToken)
            {
                var company = await _context.Companies.FindAsync(request.Id);

                company.Name = request.Name ?? company.Name;
                company.Exchange = request.Exchange ?? company.Exchange;
                company.Ticker = request.Ticker ?? company.Ticker;
                company.Isin = request.Isin ?? company.Isin;
                company.Website = request.Website ?? company.Website;

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Unit.Value;
                
                throw new Exception("Problem saving changes");
            }
        }
    }
}
