using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liskong.FormCliente
{
    public partial class CrearCliente : Form
    {
        LiskongEntities entity = new LiskongEntities();
        Cliente tablaCliente = new Cliente();
        public CrearCliente()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {

            //Comprobacion de Campos vacion
            if (txtNumeroDeIdentidad.Text == string.Empty)
            {
                MessageBox.Show(" Campos de la identidad vacio");
                return;
            }
            if (txtNombreCompleto.Text == string.Empty)
            {
                MessageBox.Show(" Campo del Nombre vacio");

                return;
            }
            if (txtCiudad.Text == string.Empty)
            {
                MessageBox.Show(" Campo de la ciudad vacio");
                return;
            }
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show(" Campo del pais vacio");

                return;
            }
            if (txtCorreo.Text == string.Empty)
            {
                MessageBox.Show(" Campo del correo vacio");

                return;
            }
            if (txtTelefono.Text == string.Empty)
            {
                MessageBox.Show(" Campo del telefono vacio");
                return;
            }
            if (!txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("El numero de telefono contiene caracteres no validos.");
                return;
            }

            try
            {
                //Preparacion de parametros
                tablaCliente.NumeroDeIdentidad = txtNumeroDeIdentidad.Text;
                tablaCliente.NombreCompleto = txtNombreCompleto.Text;
                tablaCliente.Ciudad = txtCiudad.Text;
                tablaCliente.Pais = txtPais.Text;
                tablaCliente.Telefono = Convert.ToInt32(txtTelefono.Text);
                tablaCliente.Correo = txtCorreo.Text;

                //Guardar cambios
                entity.Cliente.Add(tablaCliente);
                entity.SaveChanges();

                //Limpieza de campos
                txtNumeroDeIdentidad.Text = txtNombreCompleto.Text = txtCiudad.Text =
                txtPais.Text = txtTelefono.Text = txtCorreo.Text = string.Empty;

                MessageBox.Show("Se guardaron los datos del cliente.");
            }
            catch (Exception ex)
            {
                entity.Cliente.Remove(tablaCliente);
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNumeroDeIdentidad.Text = txtNombreCompleto.Text = txtCiudad.Text =
            txtPais.Text = txtTelefono.Text = txtCorreo.Text = string.Empty;

        }
    }
}
