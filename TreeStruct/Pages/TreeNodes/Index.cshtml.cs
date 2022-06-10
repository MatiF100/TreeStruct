using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
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

        public enum SortOrders
        {
            [Display(Name="By name, ascending")]
            AlphAsc,
            [Display(Name="By name, descending")]
            AlphDesc,
            [Display(Name="By length, ascending")]
            LenAsc,
            [Display(Name="By length, descending")]
            LenDesc,
            [Display(Name="By number of children, ascending")]
            ChildAsc,
            [Display(Name="By number of children, descending")]
            ChildDesc,
            [Display(Name="By ID, ascending")]
            IdAsc,
            [Display(Name="By ID, descending")]
            IdDesc
        }

        public IndexModel(TreeStruct.Database.TreeStructDbContext context)
        {
            _context = context;
        }

        [BindProperty] public SortOrders SortOrder { get; set; }
        public IList<TreeNode> TreeNode { get;set; } = default!;
        //var sortOrder = (IndexModel.SortOrders)(HttpContext.Session.GetInt32("SortOrder") ?? (int)IndexModel.SortOrders.AlphAsc);

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
                SortOrder = (SortOrders) (HttpContext.Session.GetInt32("SortOrder") ?? 0);
                HttpContext.Session.Remove("Expand");
                var foldDict = new Dictionary<int, bool>();
                foldDict[-1] = false;
                HttpContext.Session.Set("Expand", Encoding.ASCII.GetBytes(foldDict.ToJson()) );
            }else if (foldState == "expandAll")
            {
                SortOrder = (SortOrders) (HttpContext.Session.GetInt32("SortOrder") ?? 0);
                HttpContext.Session.Remove("Expand");
                var foldDict = new Dictionary<int, bool>();
                foldDict[-1] = true;
                HttpContext.Session.Set("Expand", Encoding.ASCII.GetBytes(foldDict.ToJson()) );
            }
            HttpContext.Session.SetInt32("SortOrder", (int)SortOrder);

            TreeNode = await _context.TreeNode.ToListAsync();
            return Page();
        }
    }
}
