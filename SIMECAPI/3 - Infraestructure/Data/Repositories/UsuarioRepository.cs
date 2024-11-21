using SIMECAPI.Data;
using SIMECAPI.Models;
using SIMECAPI.Repositories.Base;

namespace SIMECAPI.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SIMECContext context) : base(context)
        {
        }
    }
}
