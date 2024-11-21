using SIMECAPI.Data;
using SIMECAPI.Models;
using SIMECAPI.Repositories.Base;

namespace SIMECAPI.Repositories
{
    public class ApartamentoRepository : Repository<Apartamento>, IApartamentoRepository
    {
        public ApartamentoRepository(SIMECContext context) : base(context)
        {
        }
    }
}
