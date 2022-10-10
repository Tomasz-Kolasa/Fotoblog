using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Fotoblog.DAL
{
    public class AutoMigrations : IAutoMigrations
    {
        private readonly ApplicationDbContext _dbContext;
        public AutoMigrations(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Update()
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if(pendingMigrations != null && pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
        }
    }
}
