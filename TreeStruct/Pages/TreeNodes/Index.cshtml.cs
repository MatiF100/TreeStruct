using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NuGet.Protocol;
using TreeStruct.Database;
using TreeStruct.Models;

namespace TreeStruct.Pages_TreeNodes
{
    public class IndexModel : PageModel
    {

        private readonly TreeStruct.Database.TreeStructDbContext _context;

        public IndexModel(TreeStruct.Database.TreeStructDbContext context)
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

        public async Task<IActionResult> OnGetFoldAsync(int? id)
        {

            if (_context.TreeNode != null)
            {
                TreeNode = await _context.TreeNode.ToListAsync();
            }

            if (id != null)
            {
                int localId = id ?? -1;
                var foldState = HttpContext.Session.Get("Expand") ?? new byte[0];
                var foldDict = new Dictionary<int, bool>();
                if (foldState.Length != 0)
                {
                    foldDict = JsonConvert.DeserializeObject<Dictionary<int, bool>>(Encoding.ASCII.GetString(foldState)) ?? new Dictionary<int, bool>();
                }

                bool prevState = false;
                foldDict.TryGetValue(localId, out prevState);
                 
                if (foldDict.ContainsKey(-1))
                {
                    prevState = foldDict[-1];
                    foldDict = new Dictionary<int, bool>();
                }
                
                foldDict[localId] = !prevState;
                HttpContext.Session.Set("Expand", Encoding.ASCII.GetBytes(foldDict.ToJson()) );
            }

            return Redirect(".");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var foldState = Request.Form["fold"];
            if (foldState == "foldAll")
            {
                HttpContext.Session.Remove("Expand");
                var foldDict = new Dictionary<int, bool>();
                foldDict[-1] = false;
                HttpContext.Session.Set("Expand", Encoding.ASCII.GetBytes(foldDict.ToJson()) );
            }else if (foldState == "expandAll")
            {
                HttpContext.Session.Remove("Expand");
                var foldDict = new Dictionary<int, bool>();
                foldDict[-1] = true;
                HttpContext.Session.Set("Expand", Encoding.ASCII.GetBytes(foldDict.ToJson()) );
            }

            TreeNode = await _context.TreeNode.ToListAsync();
            return Page();
        }
    }
}
