using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamo.Controls;
using Dynamo.Engine;
using Dynamo.Graph.Workspaces;
using Dynamo.UI;

namespace theDAM.AnalyzeGraphs
{
    /// <summary>
    /// Interaction logic for AnalyzeGraphs.xaml
    /// </summary>
    public partial class AnalyzeGraphs : Window
    {
        public class TheDamGraph
        {
            public string GraphName { get; set; }
            public int NodeCount { get; set; }
        }

        private List<string> _filePaths;

        public AnalyzeGraphs()
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

                    TextBoxDirectory.Text = fbd.SelectedPath;

                    _filePaths = Directory.EnumerateFiles(fbd.SelectedPath, "*.*", searchOption)
                        .Where(s => s.EndsWith(".dyn")).ToList();
                    PackLists();
                }
            }
        }

        private void PackLists()
        {
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
            col1.Header = "Node Count";
            col1.DisplayMemberBinding = new System.Windows.Data.Binding("NodeCount");
            grid.Columns.Add(col1);
            //bind the list view to the grid
            this.ListViewDynamoInfo.View = grid;
            //iterate through the file paths to get the info
            foreach (string file in _filePaths)
            {
                FileInfo fInfo = new FileInfo(file);
                this.ListViewDynamoInfo.Items.Add(new TheDamGraph() {GraphName = fInfo.Name, NodeCount = 1});
            }
        }

        private int CountNodes(string file)
        {
            return 1;
        }

    }
}
