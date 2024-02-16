using Microsoft.EntityFrameworkCore;

namespace GraduationProjectsManagement.Data
{
    public class MyDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly IServiceProvider _provider;

        public MyDbContextFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ApplicationDbContext CreateDbContext()
        {
            if (_provider == null)
            {
                throw new InvalidOperationException("You must configure an instance of IServiceProvider");
            }

            return ActivatorUtilities.CreateInstance<ApplicationDbContext>(_provider);
        }
    }
}
