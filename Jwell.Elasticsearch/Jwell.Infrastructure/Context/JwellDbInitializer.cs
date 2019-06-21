using System.Data.Entity;

namespace Jwell.Repository.Context
{
    public class JwellDbInitializer : IDatabaseInitializer<JwellDbContext>
    {
        public void InitializeDatabase(JwellDbContext context)
        {
            context.Database.Initialize(false);          
        }
    }
}
