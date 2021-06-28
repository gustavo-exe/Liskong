using pryStreaingUnicah.Clases;
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
    public partial class Login : Form
    {
        LiskongEntities entity = new LiskongEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Validadicion de campos vacios
            if (txtEmpleado.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el correo o numero de identidad");
            }
            else if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Ingrese la contraseña.");
                return;
            }
            // Encriptacion
            Hash hash = new Hash();
            string password = hash.obtenerHash256(txtPassword.Text);

            //Consulta
            var tablaEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == txtEmpleado.Text && x.Password == password);
            if (tablaEmpleado == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectas.");
                return;
            }
            else
            {
                Menu formMenu = new Menu(tablaEmpleado.NumeroDeIdentidad, tablaEmpleado.Nombre, tablaEmpleado.Apellido);
                this.Hide();
                formMenu.Show();
            }
        }
    }
}
