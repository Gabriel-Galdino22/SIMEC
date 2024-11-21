using SIMECAPI.Data;
using SIMECAPI.Models;
using SIMECAPI.Repositories.Base;

namespace SIMECAPI.Repositories
{
    public class MoedaRepository : Repository<Moeda>, IMoedaRepository
    {
        public MoedaRepository(SIMECContext context) : base(context)
        {
        }
    }
}
