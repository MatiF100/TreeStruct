using Microsoft.EntityFrameworkCore;
using TreeStruct.Data;

namespace TreeStruct.Models
{
	public class TreeNode
	{
		public int ID {get; set;}
		public string value {get; set;} = string.Empty;

  // An alternative would be to keep information about Node's parent instead of list of it's children
  // This however, even though being easier to implement, would make iteration over nodes a lot harder - more database accesses
		public List<TreeNode> children {get; set;}= new List<TreeNode>();

	}

	public class SeedExampleData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var ctx = new TreeStructDbContext(serviceProvider.GetRequiredService<DbContextOptions<TreeStructDbContext>>()))
			{
				if (ctx == null || ctx.TreeNode == null)
				{
					throw new ArgumentNullException("Null database context!");
				}

				if (ctx.TreeNode.Any())
				{
					return; //Database already contains data - seeding is undesirable at this stage
				}

				List<TreeNode> level2 = new List<TreeNode>();
				level2.Add(
					new TreeNode{
						value = "My photos",
						children = new List<TreeNode>()
					}
				);

				List<TreeNode> level1 = new List<TreeNode>();
				level1.Add(
					new TreeNode{
						value = "Documents",
						children = new List<TreeNode>()
					}
				);
				level1.Add(
					new TreeNode{
						value = "Photos",
						children = level2
					}
				);
				level1.Add(
					new TreeNode{
						value = "Music",
						children = new List<TreeNode>()
					}
				);

				ctx.TreeNode.AddRange(
					new TreeNode{
						value="Root",
						children = level1
					}
				);
				ctx.SaveChanges();
			}
		}

	}
}