using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Workspaces;
using TextBox = System.Windows.Controls.TextBox;

namespace theDAM.AnalyzeGraphs
{
    /// <summary>
    /// Interaction logic for AnalyzeGraphs.xaml
    /// </summary>
    public partial class AnalyzeGraphs : Window
    {
        public class TheDamGraph
        {
            public WorkspaceModel WorkspaceModel { get; set; }
            public string GraphName { get; set; }
            public string GraphPurpose { get; set; }
            public int NodeCount { get; set; }
            public string FilePath { get; set;}
        }

        private List<string> _filePaths;
        private Dictionary<string, string> _categoryDictionary = new Dictionary<string, string>();
        public AnalyzeGraphs()
        {
            LoadCategorizationGraphs();
            InitializeComponent();
        }

        private void ButtonDirectory_Click(object sender, RoutedEventArgs e)
        {       
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SearchOption searchOption = SearchOption.TopDirectoryOnly;
                    if (this.CheckBoxSubdirectories.IsChecked.Value)
                    {
                        searchOption = SearchOption.AllDirectories;
                    }

                    TextBox.Text = fbd.SelectedPath;

                    _filePaths = Directory.EnumerateFiles(fbd.SelectedPath, "*.*", searchOption)
                        .Where(s => s.EndsWith(".dyn")).ToList();
                    PackLists();
                }
            }
        }

        private void PackLists()
        {
           // System.Windows.DataTemplate celltemplate = new DataTemplate(typeof(TextBox));

            //grid view to add the dynamo info to the list
            GridView grid = new GridView();
            //column to contain graph names
            GridViewColumn col0 = new GridViewColumn();
            col0.Width = 200;
            col0.Header = "Graph Name";
            col0.DisplayMemberBinding = new System.Windows.Data.Binding("GraphName");
            grid.Columns.Add(col0);
            //column to add node counts to
            GridViewColumn col1 = new GridViewColumn();
            col1.Width = 200;
            //col1.CellTemplate = celltemplate;
            col1.Header = "Graph Purpose";
            col1.DisplayMemberBinding = new System.Windows.Data.Binding("GraphPurpose");
            grid.Columns.Add(col1);
            //column to add node counts to
            GridViewColumn col2 = new GridViewColumn();
            col2.Width = 200;
            col2.Header = "Node Count";
            col2.DisplayMemberBinding = new System.Windows.Data.Binding("NodeCount");
            grid.Columns.Add(col2);

            //bind the list view to the grid
            this.ListViewDynamoInfo.View = grid;

            //iterate through the file paths to get the info
            foreach (string file in _filePaths)
            {
                WorkspaceModel workspaceModel = Utilities.Utilities.WorkspaceFromJSON(file);

                List<string> graphType = new List<string>();
                foreach (NodeModel node in workspaceModel.Nodes)
                {
                    _categoryDictionary.TryGetValue(node.Name, out var returnValue);
                    if (returnValue != null)
                    {
                        graphType.Add(returnValue);
                    }
                }

                this.ListViewDynamoInfo.Items.Add(new TheDamGraph()
                {
                    WorkspaceModel = workspaceModel,
                    GraphName = workspaceModel.Name,
                    GraphPurpose = string.Join(", ", graphType.Distinct()),//we join the unique graph purposes in one string
                    NodeCount = workspaceModel.Nodes.Count(),
                    FilePath = file,
                });
                
            }
        }




        // this method loads the "training data" from the extra folder
        private Dictionary<string, string> LoadCategorizationGraphs()
        {
            _categoryDictionary.Clear();
            string extraPath = theDAM.ExecutingPath.Replace("bin\\theDAM.dll", "extra\\Categorization\\");

            foreach (var dyn in Directory.GetFiles(extraPath))
            {
                var ws = Utilities.Utilities.WorkspaceFromJSON(dyn);
                foreach (NodeModel node in ws.Nodes)
                {
                    try
                    {
                        _categoryDictionary.Add(node.Name, ws.Name);
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                    
                }
               
            }
            
            return _categoryDictionary;
        }

        private void ButtonSetPurpose_Click(object sender, RoutedEventArgs e)
        {
            //set the description of the graphs based on our dataz
            foreach (TheDamGraph damGraph in this.ListViewDynamoInfo.Items)
            {
                damGraph.WorkspaceModel.Description = damGraph.GraphPurpose;
                damGraph.WorkspaceModel.Save(damGraph.FilePath);
            }
        }
    }
}
