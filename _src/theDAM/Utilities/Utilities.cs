using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Graph.Workspaces;

namespace theDAM.Utilities
{
    public static class Utilities
    {
        public static WorkspaceModel WorkspaceFromJSON(string file)
        {
            string json = File.ReadAllText(file);
            //this amazing little portion constructs a DYN from JSON.
            var wm = WorkspaceModel.FromJson(json, theDAM.DynView.Model.LibraryServices,
                null,
                null,
                theDAM.DynView.Model.NodeFactory,
                true,
                true,
                theDAM.DynView.Model.CustomNodeManager);

            return wm;
        }
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
}
