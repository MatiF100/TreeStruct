using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;
using TreeStruct.Models;
using TreeStruct.Pages.Shared;

namespace TreeStruct.Pages_TreeNodes
{
    public class DeleteModel : PageModel
    {
        private readonly TreeStruct.Database.TreeStructDbContext _context;

        public DeleteModel(TreeStruct.Database.TreeStructDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TreeNode TreeNode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TreeNode == null)
            {
                return NotFound();
            }

            var treenode = await _context.TreeNode.FirstOrDefaultAsync(m => m.ID == id);

            if (treenode == null)
            {
                return NotFound();
            }
            else 
            {
                TreeNode = treenode;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TreeNode == null)
            {
                return NotFound();
            }
            var treenode = await _context.TreeNode.Include(i => i.children).SingleAsync(p => p.ID == id);

            if (treenode != null)
            {
                TreeNode = treenode;
                //_context.TreeNode.Remove(TreeNode);
                await _Functions.DeleteRecursive(_context, treenode);
                await _context.SaveChangesAsync();
                
            }

            return RedirectToPage("./Index");
        }
    }
}
