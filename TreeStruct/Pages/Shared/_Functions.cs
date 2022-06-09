using System.Text;
using TreeStruct.Models;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;

namespace TreeStruct.Pages.Shared;

public class _Functions
{
    public static string GetExtendedValue(TreeStructDbContext ctx, TreeNode node)
    {
        var extendedValue = new StringBuilder();
        extendedValue.Append(node.value);
        var nextId = node.TreeNodeID;
        while (nextId != null)
        {
            var tmp = ctx.TreeNode.Where(p => p.ID == nextId).First();
            extendedValue.Insert(0, "/");
            extendedValue.Insert(0, tmp.value);
            nextId = tmp.TreeNodeID;
        }
        extendedValue.Insert(0, "/");

        return extendedValue.ToString();
    }

    public static async Task DeleteRecursive(TreeStructDbContext ctx, TreeNode node)
    {
        if (node != null)
        {
            foreach (var child in node.children)
            {
                await DeleteRecursive(ctx, ctx.TreeNode.Include(i => i.children).Single(p=>p.ID == child.ID));
            }
            ctx.TreeNode.Remove(node);
        }
    }
}