using MyvarVM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyvarVM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Engine en = new Engine();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {             
                en.Run(File.ReadAllBytes(dlg.FileName));
                richTextBox1.Text += "Executed File" + "\n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "";
            for (int i = 0; i < 8; i++)
            {
                label1.Text += "\n" + ((Registor32)i).ToString() + ": " + en.GetRegistor32(((Registor32)i));
            }

            label1.Text += "\n\n\n\n";

            for (int i = 0; i < 8; i++)
            {
                label1.Text += "\n" + ((Registor8)i).ToString() + ": " + en.GetRegistor8(((Registor8)i));
            }
        }
    }
}
