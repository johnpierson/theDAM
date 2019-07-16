using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Dynamo.Controls;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;

namespace theDAM
{
    /// <summary>
    /// The View Extension framework for Dynamo allows you to extend
    /// the Dynamo UI by registering custom MenuItems. A ViewExtension has 
    /// two components, an assembly containing a class that implements 
    /// IViewExtension, and an ViewExtensionDefintion xml file used to 
    /// instruct Dynamo where to find the class containing the
    /// IViewExtension implementation. The ViewExtensionDefinition xml file must
    /// be located in your [dynamo]\viewExtensions folder.
    /// 
    /// This sample demonstrates an IViewExtension implementation which 
    /// shows a modeless window when its MenuItem is clicked. 
    /// The Window created tracks the number of nodes in the current workspace, 
    /// by handling the workspace's NodeAdded and NodeRemoved events.
    /// </summary>
    public class theDAM : IViewExtension
    {
        private MenuItem _theDamMenuItem;
        public static string ExecutingPath;
        public void Dispose()
        {
        }
        public static DynamoView view;


        public void Startup(ViewStartupParams p)
        {
        }

        public void Loaded(ViewLoadedParams p)
        {
            //get our executing path for usage throughout
            ExecutingPath = Assembly.GetExecutingAssembly().Location;
           
            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            view = p.DynamoWindow as DynamoView;
            //this is our main menu item
            _theDamMenuItem = new MenuItem { Header = "theDAM" };

            #region graph Analysis
            MenuItem analyzeGraphs = new MenuItem { Header = "Analyze Graphs" };
            _theDamMenuItem.Items.Add(analyzeGraphs);
            MenuItem analyzegraphPurpose = new MenuItem { Header = "01 | Set Graph Purpose" };
            analyzegraphPurpose.Click += (sender, args) =>
            {
                AnalyzeGraphs.AnalyzeGraphs aGraphs = new AnalyzeGraphs.AnalyzeGraphs();
                aGraphs.Show();
            };
            analyzeGraphs.Items.Add(analyzegraphPurpose);

            MenuItem graphBrowser = new MenuItem { Header = "02 | Browse Graphs" };
            graphBrowser.Click += (sender, args) =>
            {
                GraphBrowser.GraphBrowser graphBrowserWindow = new GraphBrowser.GraphBrowser();
                graphBrowserWindow.Show();
            };
            analyzeGraphs.Items.Add(graphBrowser);
            #endregion



            #region MarcelloPrototyping
            //this menu now allows a flyout
            MenuItem prototyping = new MenuItem { Header = "Prototyping" };
            _theDamMenuItem.Items.Add(prototyping);

            MenuItem nodeCount = new MenuItem { Header = "CountNodes" };
            nodeCount.Click += (sender, args) =>
            {
                MessageBox.Show(NodeDescriptions.nodedesc.GetNODECOUNT().ToString());
            };
            prototyping.Items.Add(nodeCount);

            MenuItem nodeDesciption = new MenuItem { Header = "Node Description" };
            nodeDesciption.Click += (sender, args) =>
            {
                //MessageBox.Show(NodeDescriptions.nodedesc.GetNODEdesc());
                string[] my_local_arraydesc = NodeDescriptions.nodedesc.GetNODEdesc();
                System.IO.File.WriteAllLines(@"D:\working_revit_organics\dynamo\HACKAthon02\theDAM\Descriptions.txt", my_local_arraydesc);
            };
            prototyping.Items.Add(nodeDesciption);


            MenuItem nodeName = new MenuItem { Header = "Node Name" };
            nodeName.Click += (sender, args) =>
            {
                //MessageBox.Show(NodeDescriptions.nodedesc.GetNODEdesc());
                string[] my_local_arrayName = NodeDescriptions.nodedesc.GetNODEName();
                System.IO.File.WriteAllLines(@"D:\working_revit_organics\dynamo\HACKAthon02\theDAM\Names.txt", my_local_arrayName);
            };
            prototyping.Items.Add(nodeName);
            #endregion

            //change the menu font color and add it to the dynamo ribbon
            p.dynamoMenu.Items.Add(_theDamMenuItem);
        }

        public void Shutdown()
        {
        }

        internal static void dynView()
        {
            throw new NotImplementedException();
        }

        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public string Name
        {
            get
            {
                return "theDAM View Extension";
            }
        }
        public static DynamoViewModel DynView
        {
            get { return view.DataContext as DynamoViewModel; }
        }

        public string[] ListDesc { get; private set; }
    }
}
