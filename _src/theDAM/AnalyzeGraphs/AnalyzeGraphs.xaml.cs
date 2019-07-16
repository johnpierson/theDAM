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
using Dynamo.UI;

namespace theDAM.AnalyzeGraphs
{
    /// <summary>
    /// Interaction logic for AnalyzeGraphs.xaml
    /// </summary>
    public partial class AnalyzeGraphs : Window
    {
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
                    TextBoxDirectory.Text = fbd.SelectedPath;
                }
            }


        }
    }
}
