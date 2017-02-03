using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public bool autenticado { get; set; }
        public bool loginClosed = false;
        public Form1()
        {
            InitializeComponent();
            this.Width = 1300;
            this.Height = 700;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             

            LogIn frm = new LogIn(this);
            frm.MdiParent = this;
            frm.Show();

            menuStrip1.Visible = false;
        }

        public void showForm()
        {
            menuStrip1.Visible = autenticado;
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm sf = new SearchForm();
            sf.MdiParent = this;
            sf.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
