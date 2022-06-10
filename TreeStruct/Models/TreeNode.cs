using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;

namespace TreeStruct.Models
{
	public class TreeNode
	{
		[Required]
		[KeyAttribute]
		public int ID {get; set;}
		
		[ForeignKey("ID")]
		public int? TreeNodeID { get; set; }
		
		[Required(ErrorMessage = "The node needs to have a name")]
		[StringLength(256)]
		[RegularExpression("([a-zA-Z0-9_\\s]+)", ErrorMessage = "Name can only contain alphanumerical characters and underscores")]
		public string value {get; set;} = string.Empty;

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