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
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.labVersion.Text = "Version " + version;

            InitializeComponent();
        }
    }
}
