using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDAM.NodeDescriptions
{
    public class nodedesc
    {
        public static string myPublicFileName; 
        public static int GetNODECOUNT()
        {
           return theDAM.DynView.HomeSpace.Nodes.Count();
        }

        public static string[] GetNODEdesc()
        {

           int myNodeCount =  theDAM.DynView.HomeSpace.Nodes.Count();

           string[] stringArraydesc = new string[myNodeCount];
            //create loop herenthru all count
            //start..end..step
            for (int i = 0; i < myNodeCount; i = i + 1)
            {
                stringArraydesc[i] = theDAM.DynView.HomeSpace.Nodes.ElementAt(i).Description;            
            }
            return stringArraydesc;                

        }

        public static string[] GetNODEName()
        {
            int myNodeCount = theDAM.DynView.HomeSpace.Nodes.Count();

            myPublicFileName = theDAM.DynView.HomeSpace.FileName;

            string[] stringArrayName = new string[myNodeCount];
            //create loop herenthru all count
            //start..end..step
            for (int i = 0; i < myNodeCount; i = i + 1)
            {
                stringArrayName[i] = theDAM.DynView.HomeSpace.Nodes.ElementAt(i).Name;
            }
            return stringArrayName;
                     
        }

        //public static string[] GetGraphPreview()
        //{

        //    theDAM.DynView.
        //}
    }
}
