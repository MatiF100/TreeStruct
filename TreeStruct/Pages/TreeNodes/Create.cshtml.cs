using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreeStruct.Database;
using TreeStruct.Models;
using TreeStruct.Pages.Shared;

namespace TreeStruct.Pages_TreeNodes
{
    public class CreateModel : PageModel
    {
        private readonly TreeStruct.Database.TreeStructDbContext _context;

        public CreateModel(TreeStruct.Database.TreeStructDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var tmpNodeList = _context.TreeNode.ToList();
            NodeList = tmpNodeList.Select(p => new SelectListItem
            {
                Text = _Functions.GetExtendedValue(_context, p),
                Value = p.ID.ToString()
            });
            return Page();
        }

        [BindProperty]
        public TreeNode TreeNode { get; set; } = default!;
        public IEnumerable<SelectListItem> NodeList = default!;
        

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
