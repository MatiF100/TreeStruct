using Microsoft.EntityFrameworkCore;

namespace TreeStruct.Database
{
	public class TreeStructDbContext : DbContext
	{
		public TreeStructDbContext(DbContextOptions<TreeStructDbContext> options) : base(options)
		{

		}
		public DbSet<TreeStruct.Models.TreeNode> TreeNode => Set<TreeStruct.Models.TreeNode>();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("DefaultConnection");
            }
        }
	}
}