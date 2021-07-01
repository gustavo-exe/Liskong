using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liskong.FomReporte
{
    public partial class CrearReporte : Form
    {
        LiskongEntities entity = new LiskongEntities();
        public CrearReporte()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                var tablaCliente = from p in entity.Cliente
                                   where p.NumeroDeIdentidad == txtNumeroDeIdentidad.Text
                                   select new
                                   {
                                       p.NumeroDeIdentidad,
                                       p.NombreCompleto,
                                       p.Ciudad,
                                       p.Pais,
                                       p.Telefono,
                                       p.Correo
                                   };
                dataGridCliente.DataSource = tablaCliente.CopyAnonymusToDataTable();
                dataGridCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
