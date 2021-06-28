using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liskong
{
    public partial class Menu : Form
    {
        LiskongEntities entity = new LiskongEntities();
        //Variables
        string empleadoIdentidad = "";
        string empleadoNombre = "";
        string empleadoAppelido = "";
        public Menu(string _numeroDeIdentidad, string _nombre, string _appelido)
        {
            InitializeComponent();
            empleadoIdentidad = _numeroDeIdentidad;
            empleadoNombre = _nombre;
            empleadoAppelido = _appelido;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblApellido.Text = empleadoAppelido;
            lblNombre.Text = empleadoNombre;
        }
    }
}
