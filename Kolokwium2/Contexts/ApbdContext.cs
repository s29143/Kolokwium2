using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Contexts;

public class ApbdContext : DbContext
{
    protected ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions options) : base(options)
    {
    }
    
    
}