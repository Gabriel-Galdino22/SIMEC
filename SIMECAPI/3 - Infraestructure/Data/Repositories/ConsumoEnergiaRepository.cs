using SIMECAPI.Data;
using SIMECAPI.Models;
using SIMECAPI.Repositories.Base;

namespace SIMECAPI.Repositories
{
    public class ConsumoEnergiaRepository : Repository<ConsumoEnergia>, IConsumoEnergiaRepository
    {
        public ConsumoEnergiaRepository(SIMECContext context) : base(context)
        {
        }
    }
}
