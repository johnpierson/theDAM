using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Dynamo.Graph.Workspaces;
using CheckBox = System.Windows.Controls.CheckBox;

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
            public string FilePath { get; set; }
            public string Nodes { get; set; }
        }

        private List<string> _filePaths;
        private List<SimpleGraph> graphList = new List<SimpleGraph>();
        private int check = 1;
        public GraphBrowser()
        {
            InitializeComponent();
        }

        private void ListViewDynamoInfoOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SimpleGraph simpleGraph = ListViewDynamoInfo.SelectedItem as SimpleGraph;
            //display the dialog
            theDAM.DynView.OpenCommand.Execute(simpleGraph.FilePath);
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
            graphList.Clear();
            //iterate through the file paths to get the info
            foreach (string file in _filePaths)
            {
                WorkspaceModel workspaceModel = Utilities.Utilities.WorkspaceFromJSON(file);
                
                SimpleGraph sGraph = new SimpleGraph();
                sGraph.WorkspaceModel = workspaceModel;
                sGraph.GraphName = workspaceModel.Name;
                sGraph.FilePath = file;
                sGraph.Nodes = string.Join(", ", workspaceModel.Nodes.Select(n => n.Name));
                sGraph.Description = " " + workspaceModel.Description;

                graphList.Add(sGraph);
            }
            this.ListViewDynamoInfo.ItemsSource = graphList;
            CollectionViewSource.GetDefaultView(ListViewDynamoInfo.ItemsSource).Filter = UserFilter;
            ListViewDynamoInfo.MouseDoubleClick += ListViewDynamoInfoOnMouseDoubleClick;

        }

        private void TextBoxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListViewDynamoInfo.ItemsSource).Filter = UserFilter;
            CollectionViewSource.GetDefaultView(ListViewDynamoInfo.ItemsSource).Refresh();
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearchBar.Text))
                return true;

            var simpleGraph = (SimpleGraph)item;


            switch (check)
            {
                case 1:
                    return simpleGraph.GraphName.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());

                case 3:
                    return simpleGraph.GraphName.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower())
                           || simpleGraph.Description.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());
                case 5:
                    return simpleGraph.Nodes.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());
                case 6:
                    return simpleGraph.GraphName.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower())
                           || simpleGraph.Nodes.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());
                case 8:
                    return simpleGraph.Description.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower())
                           || simpleGraph.Nodes.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());
                case 9:
                    return simpleGraph.Description.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower())||
                        simpleGraph.GraphName.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower())
                           || simpleGraph.Nodes.Contains(TextBoxSearchBar.Text.Replace(" ", "").ToLower());
                default:
                    return true;
            }

        }

        private void CheckBoxGraphName_Checked(object sender, RoutedEventArgs e)
        {
            check += 1;
        }
        private void CheckBoxGraphName_UnChecked(object sender, RoutedEventArgs e)
        {
            check += 1;
        }

        private void CheckBoxGraphPurpose_Checked(object sender, RoutedEventArgs e)
        {
            check += 3;
        }
        private void CheckBoxGraphPurpose_UnChecked(object sender, RoutedEventArgs e)
        {
            check -= 3;
        }

        private void CheckBoxNodes_Checked(object sender, RoutedEventArgs e)
        {
            check += 5;
        }
        private void CheckBoxNodes_UnChecked(object sender, RoutedEventArgs e)
        {
            check -= 5;
        }
    }
}
