using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;
using TreeStruct.Models;
using TreeStruct.Pages.Shared;

namespace TreeStruct.Pages_TreeNodes
{
    public class AddModel : PageModel
    {
        private readonly TreeStruct.Database.TreeStructDbContext _context;

        public AddModel(TreeStruct.Database.TreeStructDbContext context)
        {
            _context = context;
        }

      [BindProperty]
      public TreeNode TreeNode { get; set; } = default!; 
      public IEnumerable<SelectListItem> NodeList = default!;

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
                var tmpNodeList = _context.TreeNode.ToList();
                NodeList = tmpNodeList.Select(p => new SelectListItem
                {
                    Text = _Functions.GetExtendedValue(_context, p),
                    Value = p.ID.ToString(),
                    Selected = p.ID == treenode.ID 
                });
                TreeNode = new TreeNode();
                TreeNode.TreeNodeID = treenode.ID;
                TreeNode.children = treenode.children;

            }
            return Page();
        }
     public async Task<IActionResult> OnPostAsync()
         {
           if (!ModelState.IsValid || _context.TreeNode == null || TreeNode == null)
             {
                 return Page();
             }
 
             _context.TreeNode.Add(TreeNode);
             await _context.SaveChangesAsync();
 
             return RedirectToPage("./Index");
         }   
    }
}
