using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using Dynamo.Graph.Workspaces;

namespace theDAM.GraphBrowser
{
    /// <summary>
    /// Interaction logic for GraphBrowser.xaml
    /// </summary>
    public partial class GraphBrowser : Window
    {
        public class SimpleGraph
        {
            public WorkspaceModel WorkspaceModel { get; set; }
            public string GraphName { get; set; }
            public string Description { get; set; }
        }

        private List<string> _filePaths;
        public GraphBrowser()
        {
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
            col1.DisplayMemberBinding = new System.Windows.Data.Binding("Description");
            grid.Columns.Add(col1);

            //bind the list view to the grid
            this.ListViewDynamoInfo.View = grid;

            //iterate through the file paths to get the info
            foreach (string file in _filePaths)
            {
                WorkspaceModel workspaceModel = Utilities.Utilities.WorkspaceFromJSON(file);

                this.ListViewDynamoInfo.Items.Add(new SimpleGraph()
                {
                    WorkspaceModel = workspaceModel,
                    GraphName = workspaceModel.Name,
                    Description = workspaceModel.Description
                });
                //TODO: Figure out how to filter on the fly.
            }
        }

    }
}
