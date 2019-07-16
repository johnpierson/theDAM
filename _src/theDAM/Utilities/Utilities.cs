using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Graph.Workspaces;

namespace theDAM.Utilities
{
    public class Utilities
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
    }
}
