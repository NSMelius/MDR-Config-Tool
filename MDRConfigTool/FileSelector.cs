using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDRConfigTool
{
    public partial class FileSelector : Form
    {
        public FileSelector()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program c = new Program();
            c.SetNetId();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program c = new Program();
            c.ScanDevicesAndBoxes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program c = new Program();
            c.ActivateConfiguration();
        }
    }
}
