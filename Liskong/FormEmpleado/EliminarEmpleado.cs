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
    public partial class EliminarEmpleado : Form
    {
        LiskongEntities entity = new LiskongEntities();
        string empleadoIdentidad = "";
        public EliminarEmpleado(string _identidadEmpleado)
        {
            InitializeComponent();
            empleadoIdentidad = _identidadEmpleado;
        }

        private void EliminarEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelarPassword_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnGuardaPassword_Click(object sender, EventArgs e)
        {
            //Obtener nombre del elemento seleccionado
            //var rowEmpleado = entity.Empleado.FirstOrDefault(x => x.NumeroDeIdentidad == empleadoIdentidad);
            try
            {
                //Eliminar el departamento de la base de datos
                entity.Empleado.RemoveRange(entity.Empleado.Where(x => x.NumeroDeIdentidad == empleadoIdentidad));
                entity.SaveChanges();
                MessageBox.Show("Su cuenta fue eliminada.");

                //Cirre de los forms abiertos
                // y abrir el login
                List<Form> openForms = new List<Form>();

                foreach (Form f in Application.OpenForms)
                {
                    openForms.Add(f);
                }

                foreach (Form f in openForms)
                {
                    if (f.Name != "Login")
                    {
                        f.Dispose();
                    }
                    else
                    {
                        Login login = new Login();
                        login.Show();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
