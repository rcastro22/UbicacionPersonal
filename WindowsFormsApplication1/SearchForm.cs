using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            ttipTxtSearch.SetToolTip(this.txtSearch, "Busqueda por: Codigo, Nombre, Extención ó Departamento");
            cargarTorres();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                wsInterfaceDyn.WSInterfaceDynamics dynamcis = new wsInterfaceDyn.WSInterfaceDynamics();
                DataTable dt = dynamcis.Buscar(txtSearch.Text == "" ? "*" : txtSearch.Text);

                dataGridView1.DataSource = dt;
                dataGridView1.AllowUserToAddRows = false;
                
                dataGridView1.AutoResizeColumns();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string Nombre = string.Empty;
                string Torre = string.Empty;
                string Oficina = string.Empty;
                string Extension = string.Empty;
                string Correlativo = string.Empty;
                string Departamento = string.Empty;
                string Puesto = string.Empty;

                string Codigo = Convert.ToString(dataGridView1.CurrentRow.Cells["Codigo"].Value);

                wsInterfaceDyn.WSInterfaceDynamics dynamics = new wsInterfaceDyn.WSInterfaceDynamics();
                dynamics.Cargar(Codigo, out Torre, out Oficina, out Extension, out Correlativo, out Departamento, out Puesto);

                textBox1.Text = Codigo;
                textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Nombre"].Value);
                textBox3.Text = Correlativo;
                textBox4.Text = Departamento;
                textBox5.Text = Torre;
                textBox6.Text = Oficina;
                textBox7.Text = Extension;

                webBrowser1.Navigate("http://dg.galileo.edu/fotos/ReadPhoto.aspx?id="+Correlativo);

                cargarTorres();

                comboBox1.SelectedValue = Torre;
                comboBox2.SelectedValue = Oficina;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void clearText()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            webBrowser1.Navigate("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    wsInterfaceDyn.WSInterfaceDynamics dynamics = new wsInterfaceDyn.WSInterfaceDynamics();
                    dynamics.Guardar(textBox1.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                    MessageBox.Show("Registro actualizado con exito!!", "Dialogo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clearText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un registro para guardar.", "Dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = comboBox1.SelectedValue.ToString();
            cargarSalones(textBox5.Text);
        }

        public void cargarTorres()
        {
            wsInterfaceDyn.WSInterfaceDynamics dynamics = new wsInterfaceDyn.WSInterfaceDynamics();
            DataSet ds = dynamics.retTorres();
            comboBox1.DataSource = ds.Tables[0].DefaultView;
            comboBox1.DisplayMember = "NOMBRETORRE";
            comboBox1.ValueMember = "TORRE";
        }

        public void cargarSalones(string _torre)
        {
            wsInterfaceDyn.WSInterfaceDynamics dynamics = new wsInterfaceDyn.WSInterfaceDynamics();
            DataSet ds = dynamics.retSalones(_torre);
            comboBox2.DataSource = ds.Tables[0].DefaultView;
            comboBox2.DisplayMember = "NOMBREC";
            comboBox2.ValueMember = "SALON";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text = comboBox2.SelectedValue.ToString();
        }
    }
}
