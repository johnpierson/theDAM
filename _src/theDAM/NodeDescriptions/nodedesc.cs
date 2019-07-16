using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDAM.NodeDescriptions
{
    public class nodedesc
    {

        public static int GetNODECOUNT()
        {
           return theDAM.DynView.HomeSpace.Nodes.Count();



        }

        public static string GetNODEdesc()
        {
            

           return theDAM.DynView.HomeSpace.Nodes.First().Description;

        }

        public static string GetNODEName()
        {


           return theDAM.DynView.HomeSpace.Nodes.First().Name;

        }
    }
}
