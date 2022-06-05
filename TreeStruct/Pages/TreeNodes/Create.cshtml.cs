using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreeStruct.Data;
using TreeStruct.Models;

namespace TreeStruct.Pages_TreeNodes
{
    public class CreateModel : PageModel
    {
        private readonly TreeStruct.Data.TreeStructDbContext _context;

        public CreateModel(TreeStruct.Data.TreeStructDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TreeNode TreeNode { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
