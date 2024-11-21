using SIMECAPI.Data;
using SIMECAPI.Models;
using SIMECAPI.Repositories.Base;

namespace SIMECAPI.Repositories
{
    public class DicaSustentabilidadeRepository : Repository<DicaSustentabilidade>, IDicaSustentabilidadeRepository
    {
        public DicaSustentabilidadeRepository(SIMECContext context) : base(context)
        {
        }
    }
}
