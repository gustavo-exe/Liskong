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
    public partial class EditarEmpleado : Form
    {
        LiskongEntities entity = new LiskongEntities();

        string empleadoIdentidad = "";
        public EditarEmpleado(string _empleadoIdentidad)
        {
            InitializeComponent();
            empleadoIdentidad = _empleadoIdentidad;

        }
        private void EditarEmpleado_Load(object sender, EventArgs e)
        {
            SelectAll();
        }

        //Obetner todos los datos del empleado
        private void SelectAll()
        {
            //Llenado de los componentes del form
            //con la informacion de la entida empleado
            var rowEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad);
            txtNumeroDeIdentidad.Text = rowEmpleado.NumeroDeIdentidad;
            txtNombre.Text = rowEmpleado.Nombre;
            txtApellido.Text = rowEmpleado.Apellido;
            txtCorreo.Text = rowEmpleado.Correo;
            txtDireccion.Text = rowEmpleado.Direccion;
            txtTelefono.Text = Convert.ToString(rowEmpleado.Telefono);
            
            //Fecha
            var fechaRecibida = Convert.ToString(rowEmpleado.FechaDeNacimiento);
            string[] separarFecha = fechaRecibida.Split('/');

            txtDia.Text = separarFecha[1];
            txtMes.Text = separarFecha[0];
            txtYear.Text = separarFecha[2].Substring(0,4);
            
        }

        //Informacion basica
        private void btnEditarBasica_Click(object sender, EventArgs e)
        {
            txtNumeroDeIdentidad.ReadOnly =  txtNombre.ReadOnly = txtApellido.ReadOnly =
            txtDia.ReadOnly =  txtMes.ReadOnly = txtYear.ReadOnly = false;

            panel2.BackColor = panel3.BackColor = panel4.BackColor = panel5.BackColor =
            panel6.BackColor = panel7.BackColor = Color.Black;

            btnCancelarBasica.Visible = true;
            btnEditarBasica.Visible = false;
        }
        private void btnCancelarBasica_Click(object sender, EventArgs e)
        {
            SelectAll();
            txtNumeroDeIdentidad.ReadOnly = txtNombre.ReadOnly = txtApellido.ReadOnly =
            txtDia.ReadOnly = txtMes.ReadOnly = txtYear.ReadOnly = true;
            
            panel2.BackColor = panel3.BackColor = panel4.BackColor = panel5.BackColor =
            panel6.BackColor = panel7.BackColor = Color.DarkGray;
            
            btnCancelarBasica.Visible = false;
            btnEditarBasica.Visible = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Preparar parametros
                var rowEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad);
                rowEmpleado.NumeroDeIdentidad = txtNumeroDeIdentidad.Text;
                rowEmpleado.Nombre = txtNombre.Text;
                rowEmpleado.Apellido = txtApellido.Text;

                string fechaDeNacimiento = txtMes.Text + '/' + txtDia.Text + '/' + txtYear.Text;
                rowEmpleado.FechaDeNacimiento = Convert.ToDateTime(fechaDeNacimiento);

                //Guardar cambios
                entity.SaveChanges();
                MessageBox.Show("Se guardaron los datos de informacion basica.");

                //Devolver al estado inicial los componentes del form
                txtNumeroDeIdentidad.ReadOnly = txtNombre.ReadOnly = txtApellido.ReadOnly =
                txtDia.ReadOnly = txtMes.ReadOnly = txtYear.ReadOnly = true;
                btnCancelarBasica.Visible = false;
                btnEditarBasica.Visible = true;
                panel2.BackColor = panel3.BackColor = panel7.BackColor = panel4.BackColor = panel5.BackColor =
                panel6.BackColor = Color.DarkGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        // Informacion de contacto
        private void btnEditarContacto_Click(object sender, EventArgs e)
        {
            txtCorreo.ReadOnly = txtTelefono.ReadOnly = txtDireccion.ReadOnly = false;
            panel10.BackColor = panel11.BackColor = txtDireccion.ForeColor = Color.Black;
            btnCancelarContacto.Visible = true;
            btnEditarContacto.Visible = false;
        }
        private void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                //Preparar parametros
                var rowEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad);
                rowEmpleado.Correo= txtCorreo.Text;
                rowEmpleado.Telefono = Convert.ToInt32(txtTelefono.Text);
                rowEmpleado.Direccion = txtDireccion.Text;

                //Guardar cambios
                entity.SaveChanges();
                MessageBox.Show("Se guardaron los datos de informacion de contacto.");

                //Devolver al estado inicial los componentes del form
                txtCorreo.ReadOnly = txtTelefono.ReadOnly = txtDireccion.ReadOnly = true;
                btnCancelarContacto.Visible = false;
                btnEditarContacto.Visible = true;
                panel10.BackColor = panel11.BackColor = txtDireccion.ForeColor = Color.DarkGray;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelarContacto_Click(object sender, EventArgs e)
        {
            SelectAll();
            panel10.BackColor = panel11.BackColor = txtDireccion.ForeColor = Color.DarkGray;
            txtCorreo.ReadOnly = txtTelefono.ReadOnly = txtDireccion.ReadOnly = true;
            btnCancelarContacto.Visible = false;
            btnEditarContacto.Visible = true;
        }

        //Cambios en la contraseña
        private void btnEditarPassword_Click(object sender, EventArgs e)
        {
            txtPasswordAntigua.ReadOnly = txtPasswordNueva.ReadOnly = false;
            btnCancelarPassword.Visible = true;
            btnEditarPassword.Visible = false;

            panel9.BackColor = panel8.BackColor =  Color.Black;
        }

        private void btnGuardaPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //Encriptacion
                Hash hash = new Hash();
                string password = hash.obtenerHash256(txtPasswordAntigua.Text);
                var tablaEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad && x.Password == password);

                if (tablaEmpleado == null)
                {
                    MessageBox.Show("Contraseña antigua incorrecta.");
                    return;
                }
                else
                {
                    string newPassword = hash.obtenerHash256(txtPasswordNueva.Text);
                    
                    //Preparar parametros
                    var rowEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad);
                    rowEmpleado.Password = newPassword;

                    //Guardar cambios
                    entity.SaveChanges();
                    MessageBox.Show("Se guardo la nueva contraseña.");
                }

                //Devolver al estado inicial los componentes del form
                txtPasswordAntigua.ReadOnly = txtPasswordNueva.ReadOnly = true;
                txtPasswordAntigua.Text = txtPasswordNueva.Text = String.Empty;
                btnCancelarPassword.Visible = false;
                btnEditarPassword.Visible = true;
                panel9.BackColor = panel8.BackColor = Color.DarkGray;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelarPassword_Click(object sender, EventArgs e)
        {
            txtPasswordAntigua.ReadOnly = txtPasswordNueva.ReadOnly = true;
            txtPasswordAntigua.Text = txtPasswordNueva.Text = String.Empty;
            btnCancelarPassword.Visible = false;
            btnEditarPassword.Visible = true;
            panel9.BackColor = panel8.BackColor = Color.DarkGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EliminarEmpleado ventana = new EliminarEmpleado(empleadoIdentidad);
            ventana.Show();
        }

        private void txtDia_Click(object sender, EventArgs e)
        {
            txtDia.Text = string.Empty;
        }

        private void txtMes_Click(object sender, EventArgs e)
        {
            txtMes.Text = string.Empty;
        }

        private void txtYear_Click(object sender, EventArgs e)
        {
            txtYear.Text = string.Empty;
        }
    }
}
