using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;
using TreeStruct.Models;

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
            var treenode = await _context.TreeNode.FindAsync(id);

            if (treenode != null)
            {
                TreeNode = treenode;
                _context.TreeNode.Remove(TreeNode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
