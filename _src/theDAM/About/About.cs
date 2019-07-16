using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theDAM.About
{
    public partial class About : Form
    {
        public About()
        {
            //Version version = Assembly.GetEntryAssembly().GetName().Version;
            //this.txtVersion.Text= version.ToString() + "";

            InitializeComponent();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://github.com/johnpierson/theDAM");
        }
    }
}
