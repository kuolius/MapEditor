using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class SaveFile : Form
    {
        public string name;
        public bool responded = false;
        public SaveFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("File name cannot be empty.", "Error");
            else
            {
                name = textBox1.Text;
                responded = true;
            }
        }

        public void update(out string name,out bool responded)
        {
            name = this.name;
            responded = this.responded;
        }
    }
}
