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

namespace Liskong.FormEmpleado
{
    public partial class CrearEmpleado : Form
    {
        LiskongEntities entity = new LiskongEntities();
        Empleado tablaEmpleado = new Empleado();
        public CrearEmpleado()
        {
            InitializeComponent();
        }

        private void CrearEmpleado_Load(object sender, EventArgs e)
        {


        }

        private void btnCrea_Click(object sender, EventArgs e)
        {

            //Comprobacion de Campos vacion
            if (txtNumeroDeIdentidad.Text == string.Empty)
            {
                MessageBox.Show(" Campos de la identidad vacio");
                return;
            }
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show(" Campo del Nombre vacio");

                return;
            }
            if (txtApellido.Text == string.Empty)
            {
                MessageBox.Show(" Campo de Appelido vacio");
                return;
            }
            if (txtCorreo.Text == string.Empty)
            {
                MessageBox.Show(" Campo del correo vacio");

                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show(" Campo de contraseña vacio");

                return;
            }
            if (txtDireccion.Text == string.Empty)
            {
                MessageBox.Show(" Campo de la direccion vacio");

                return;
            }
            if (txtDia.Text == string.Empty)
            {
                MessageBox.Show(" Campo del dia vacio");

                return;
            }
            if (txtDia.Text == string.Empty)
            {
                MessageBox.Show(" Campo del dia vacio");
                return;
            }
            if (txtMes.Text == string.Empty)
            {
                MessageBox.Show(" Campo del mes vacio");
                return;
            }
            if (txtYear.Text == string.Empty)
            {
                MessageBox.Show(" Campo del año vacio");
                return;
            }
            if (txtTelefono.Text == string.Empty)
            {
                MessageBox.Show(" Campo del telefono vacio");
                return;
            }

            //Inserccion de datos
            try
            {
                //Preparar parametros
                tablaEmpleado.NumeroDeIdentidad = txtNumeroDeIdentidad.Text;
                tablaEmpleado.Nombre = txtNombre.Text;
                tablaEmpleado.Apellido = txtApellido.Text;
                tablaEmpleado.Correo = txtCorreo.Text;

                //Encriptacion
                Hash hash = new Hash();
                tablaEmpleado.Password = hash.obtenerHash256(txtPassword.Text);
                tablaEmpleado.Direccion = txtDireccion.Text;

                string fechaDeNacimiento = txtMes.Text + '/' + txtDia.Text + '/' + txtYear.Text;
                tablaEmpleado.FechaDeNacimiento = Convert.ToDateTime(fechaDeNacimiento);

                tablaEmpleado.Telefono = Convert.ToInt32(txtTelefono.Text);
                //Guardar cambios
                entity.Empleado.Add(tablaEmpleado);
                entity.SaveChanges();

                //Liempieza de campos
                txtNumeroDeIdentidad.Text = txtNombre.Text = txtApellido.Text = txtCorreo.Text = txtPassword.Text =
                txtDireccion.Text = txtDia.Text = txtMes.Text = txtYear.Text = txtTelefono.Text = string.Empty;

                MessageBox.Show("Se creo el usuario, ahora tiene que iniciar sesion.");

                //Formulario del login
                Login login = new Login();
                this.Dispose();
                login.Show();

            }
            catch (Exception ex)
            {
                //Eliminacion de la clase que genero el problema
                entity.Empleado.Remove(tablaEmpleado);
                //Detalles completos de la excepcion
                MessageBox.Show(ex.InnerException.ToString());
                
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Dispose();
            login.Show();
        }

        private void txtYear_Click(object sender, EventArgs e)
        {
            txtYear.Text = string.Empty;
        }

        private void txtDia_Click(object sender, EventArgs e)
        {
            txtDia.Text = string.Empty;
        }

        private void txtMes_Click(object sender, EventArgs e)
        {
            txtMes.Text = string.Empty;
        }
    }
}
