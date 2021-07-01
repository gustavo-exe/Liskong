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
        //Variables
        int empleadoId = 0;
        int idCliente = 0;

        public CrearReporte( int _idEmpleado)
        {
            InitializeComponent();
            empleadoId = _idEmpleado;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                //Llenado del data grid dependiendo del numero de identidad
                var tablaCliente = from p in entity.Cliente
                                   where p.NumeroDeIdentidad == txtNumeroDeIdentidad.Text
                                   select new
                                   {
                                       p.IdCliente,
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

        private void CrearReporte_Load(object sender, EventArgs e)
        {
            cmbDepartamento.SelectedIndex = -1;
            //Llenado del combobox con los departamentos disponibles
            var tablaDepartamento = from p in entity.Departamento
                                    select new
                                    {
                                        p.IdDepartamento,
                                        p.Nombre
                                    };
            DataTable dataTableDepartamento = tablaDepartamento.CopyAnonymusToDataTable();
            cmbDepartamento.DataSource = dataTableDepartamento;
            cmbDepartamento.ValueMember = dataTableDepartamento.Columns[0].ColumnName;
            cmbDepartamento.DisplayMember = dataTableDepartamento.Columns[1].ColumnName;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dataGridCliente.SelectedRows.Count > 0)
            {
                idCliente = Convert.ToInt32(dataGridCliente.SelectedCells[0].Value);
                //Preparacion de parametos
                Reporte tablaReporte = new Reporte();
                tablaReporte.Tipo = cmbTipo.Text;
                tablaReporte.Estado = cbEstado.Checked;
                tablaReporte.FechaDeCreacion = DateTime.Now;
                tablaReporte.Detalle = txtDetalle.Text;
                tablaReporte.FK_IdDepartamento = Convert.ToInt32(cmbDepartamento.SelectedValue);
                tablaReporte.FK_IdCliente = idCliente;
                tablaReporte.FK_IdEmpleado = empleadoId;

                //Guardado
                entity.Reporte.Add(tablaReporte);
                entity.SaveChanges();

                //Limpieza de parametros
                txtDetalle.Text = txtNumeroDeIdentidad.Text = string.Empty;
                cmbDepartamento.SelectedIndex = cmbTipo.SelectedIndex = -1;
                idCliente = 0;

            }
            else
            {
                MessageBox.Show("Seleccione el cliente en el visualizador de cliente buscado.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDetalle.Text = txtNumeroDeIdentidad.Text = string.Empty;
            cmbDepartamento.SelectedIndex = cmbTipo.SelectedIndex = -1;
            idCliente = 0;
            
        }
    }
}
