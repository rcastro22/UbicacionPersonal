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
    public partial class LogIn : Form
    {
        public bool autenticado = false;
        Form1 frm1;
        public LogIn(Form1 frm)
        {
            InitializeComponent();

            textBox1.GotFocus += new EventHandler(this.txtbx1_GotFocus);
            textBox1.LostFocus += new EventHandler(this.txtbx1_LostFocus);

            textBox2.GotFocus += new EventHandler(this.txtbx2_GotFocus);
            textBox2.LostFocus += new EventHandler(this.txtbx2_LostFocus);

            frm1 = frm;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            
        }

        public void logIn()
        {
            
            wsDigitalizacion.Service srv = new wsDigitalizacion.Service();
            autenticado = srv.validar(textBox1.Text, textBox2.Text);

            if (autenticado == true)
            {
                frm1.autenticado = true;
                frm1.showForm();
                this.Close();

                SearchForm sf = new SearchForm();
                sf.MdiParent = frm1;
                sf.Show();
            }
        }

        public void txtbx1_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Usuario")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void txtbx1_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "Usuario";
                tb.ForeColor = Color.Gray;
            }
        }

        public void txtbx2_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Contraseña")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
                tb.PasswordChar = '*';
            }
        }

        public void txtbx2_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "Contraseña";
                tb.ForeColor = Color.Gray;
                tb.PasswordChar = '\0';
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                logIn();
                if(autenticado == false)
                    MessageBox.Show("Usuario o Contraseña incorrectos!!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                logIn();
                if (autenticado == false)
                    MessageBox.Show("Usuario o Contraseña incorrectos!!!");
            }
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(autenticado == false)
                Application.Exit();
        }
    }
}
