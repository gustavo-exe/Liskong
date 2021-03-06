using Liskong.FormCliente;
using Liskong.FormEmpleado;
using Liskong.FormDepartamento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liskong.FomReporte;

namespace Liskong
{
    public partial class Menu : Form
    {
        LiskongEntities entity = new LiskongEntities();
        //Variables
        int empleadoId = 0;
        string empleadoIdentidad = "";
        string empleadoNombre = "";
        string empleadoAppelido = "";
        public Menu(int _idEmpleado, string _numeroDeIdentidad, string _nombre, string _appelido)
        {
            InitializeComponent();
            empleadoId = _idEmpleado;
            empleadoIdentidad = _numeroDeIdentidad;
            empleadoNombre = _nombre;
            empleadoAppelido = _appelido;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblApellido.Text = empleadoAppelido;
            lblNombre.Text = empleadoNombre;
        }

        private void btnCambiarDatos_Click(object sender, EventArgs e)
        {
            EditarEmpleado ventana = new EditarEmpleado(empleadoIdentidad);
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void crearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CrearCliente ventana = new CrearCliente();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditarCliente ventana = new EditarCliente();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void departamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRUDDepartamento ventana = new CRUDDepartamento();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void editarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CrearReporte ventana = new CrearReporte(empleadoId);
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolucionRporte ventana = new SolucionRporte();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void departamentoToolStripMenuItem_DropDownOpened_1(object sender, EventArgs e)
        {
            departamentoToolStripMenuItem.ForeColor = Color.Black;
        }

        private void departamentoToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            departamentoToolStripMenuItem.ForeColor = Color.White;
        }

        private void reporteToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            reporteToolStripMenuItem.ForeColor = Color.Black;
        }

        private void reporteToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            reporteToolStripMenuItem.ForeColor = Color.White;
        }

        private void clienteToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            clienteToolStripMenuItem.ForeColor = Color.Black;
        }

        private void clienteToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            clienteToolStripMenuItem.ForeColor = Color.White;
        }

        private void actulizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            this.Dispose();
        }
    }
}
