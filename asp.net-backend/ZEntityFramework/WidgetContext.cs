using asp.net_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_backend.EntityFramework
{
    public class WidgetContext : DbContext
    {
        public WidgetContext(DbContextOptions<WidgetContext> options) : base(options) { }
        public DbSet<Widget> WidgetsItems { get; set; } = null!;
    }
}
