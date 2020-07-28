using Domain;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Persistence;
using Persistence.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }

            public string Exchange { get; set; }

            public string Ticker { get; set; }

            public string Isin { get; set; }

            public string Website { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Isin).SetValidator(new MyValidator());
            }
        }

        public class MyValidator : PropertyValidator
        {
            public MyValidator(
                string errorMessage = "Isin. The first two characters of an ISIN must be letters / non numeric.") : base(errorMessage)
            {
            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                var stringToValidate = context.PropertyValue as String;
                return IsValid(stringToValidate);
            }

            public bool IsValid(string stringToValidate)
            {
                var charString = stringToValidate.ToCharArray();
                if (Regex.IsMatch(charString[0].ToString(), "[0-9]") || Regex.IsMatch(charString[1].ToString(), "[0-9]"))
                {
                    return false;
                }

                return true;
            }
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

                var companiesExist = _context.Companies.Any(c => c.Isin == request.Isin);

                if (companiesExist)
                    throw new CompanyException("Company Already Exists");

                //use automapper 
                var company = new Company
                {
                    Name = request.Name,
                    Exchange = request.Exchange,
                    Ticker = request.Ticker,
                    Isin = request.Isin,
                    Website = request.Website
                };

                await _context.Companies.AddAsync(company);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;


                throw new CompanyException("Issue saving new company");
            }
        }
    }
}
