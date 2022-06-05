using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Data;
using TreeStruct.Models;

namespace TreeStruct.Pages_TreeNodes
{
    public class IndexModel : PageModel
    {
        private readonly TreeStruct.Data.TreeStructDbContext _context;

        public IndexModel(TreeStruct.Data.TreeStructDbContext context)
        {
            _context = context;
        }

        public IList<TreeNode> TreeNode { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TreeNode != null)
            {
                TreeNode = await _context.TreeNode.ToListAsync();
            }
        }
    }
}
