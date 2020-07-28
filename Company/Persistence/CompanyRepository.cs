using Persistence.Interfaces;

namespace Persistence
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }
    }
}
