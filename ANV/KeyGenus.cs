using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANV
{
    public partial class KeyGenus : Form
    {
        public string code;
        public KeyGenus()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            if (textBox1.Text == "YOUARENOTLOOSER")
            {
                f1.closing = false;
                code = "1";
                this.Close();
            }
            else
            {
                textBox1.Text = "NO! NO! NO!";
            }
        }
    }
}
