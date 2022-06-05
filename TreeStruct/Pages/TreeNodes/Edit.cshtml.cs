using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Data;
using TreeStruct.Models;

namespace TreeStruct.Pages_TreeNodes
{
    public class EditModel : PageModel
    {
        private readonly TreeStruct.Data.TreeStructDbContext _context;

        public EditModel(TreeStruct.Data.TreeStructDbContext context)
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

            var treenode =  await _context.TreeNode.FirstOrDefaultAsync(m => m.ID == id);
            if (treenode == null)
            {
                return NotFound();
            }
            TreeNode = treenode;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TreeNode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreeNodeExists(TreeNode.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TreeNodeExists(int id)
        {
          return (_context.TreeNode?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
