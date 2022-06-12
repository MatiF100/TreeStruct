using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStruct.Database;
using TreeStruct.Models;
using TreeStruct.Pages.Shared;

namespace TreeStruct.Pages_TreeNodes
{
    public class EditModel : PageModel
    {
        private readonly TreeStructDbContext _context;

        public EditModel(TreeStructDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TreeNode TreeNode { get; set; } = default!;
        
        [BindProperty]
        public IEnumerable<SelectListItem> NodeList { get; set; }= default!;


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

            var tmpNodeList = await GetExtendedChildrenList(
            _context.TreeNode.Include(p => p.children)
                    .Where(p => p.ID != treenode.ID && p.TreeNodeID == null).ToList(), treenode.ID
                );
            NodeList = tmpNodeList.Select(p => new SelectListItem
            {
                Text = GetExtendedValue(p),
                Value = p.ID.ToString()
            });
            
            TreeNode = treenode;
            return Page();
        }

        private async  Task<List<TreeNode>> GetExtendedChildrenList(IEnumerable<TreeNode> nodes, int nodeId)
        {
            var extendedList = new List<TreeNode>();
            foreach (var node in nodes)
            {
                if (node.ID != nodeId)
                {
                    extendedList.Add(node);
                    extendedList.AddRange(
                        await GetExtendedChildrenList(
                            _context.TreeNode
                                .Include(p=> p.children).Single(p => p.ID == node.ID).children, nodeId
                            )
                        );
                }
            }

            return extendedList;
        }

        private string GetExtendedValue(TreeNode node)
        {
            var extendedValue = new StringBuilder();
            extendedValue.Append(node.value);
            var nextId = node.TreeNodeID;
            while (nextId != null)
            {
                var tmp = _context.TreeNode.Where(p => p.ID == nextId).First();
                extendedValue.Insert(0, "/");
                extendedValue.Insert(0, tmp.value);
                nextId = tmp.TreeNodeID;
            }
            extendedValue.Insert(0, "/");

            return extendedValue.ToString();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _Functions.IsLooped(_context, TreeNode))
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
            catch (DbUpdateException)
            {
                return Page();
            }

            return RedirectToPage("./Index");
            
            
        }
        private bool TreeNodeExists(int id)
        {
          return (_context.TreeNode?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
