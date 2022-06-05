using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TreeStruct.Controllers
{
    public class StructureController : Controller
    {
        // 
        // GET: /Structure/

        public IActionResult Index()
        {
			//Show main app window
            //return "This is my default action...";
			return View();

        }

        // 
        // GET: /Structure/Fold/ 

        public string Fold(int id=0)
        {
			//Fold given node
            //return "Action folding node with given id";
			return HtmlEncoder.Default.Encode($"Hello from {id}");
        }

        // 
        // GET: /Structure/Fold/ 

        public string Unfold(int id=0)
        {
			//Unfold given node
            return "Action unfolding node with given id";
        }

        // 
        // GET: /Structure/Fold/ 

        public string Move(int nodeId=0, int newParentId=0)
        {
			//Move given node to new parent node
            return "Action moving the node";
        }

		public string AddNode(int parentId=0)
		{
			//Add new node to the structure
			return "Action adding new node";
		}

		public string RemoveNode(int nodeId)
		{
			//Remove given node from structure (With children)
			return "Action deleting given node and all of it's children";
		}

		public string RemoveNodeWithPreserve(int nodeId, int newParentId=0)
		{
			//Remove given node from structure and move it's children to new node
			return "Action deleting node from tree, and sending it's children to new node";
		}


    }
}