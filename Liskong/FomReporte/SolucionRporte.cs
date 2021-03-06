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
    public partial class SolucionRporte : Form
    {
        LiskongEntities entity = new LiskongEntities();
        int idReporte = 0;
        public SolucionRporte()
        {
            InitializeComponent();
        }

        private void SolucionRporte_Load(object sender, EventArgs e)
        {
            
            //Llenado del combobox con los departamentos disponibles
            var tablaDepartamento = from p in entity.Departamento
                                    select new
                                    {
                                        p.IdDepartamento,
                                        p.Nombre
                                    };
            DataTable dataTableDepartamento = tablaDepartamento.CopyAnonymusToDataTable();
            listDepartamento.DataSource = dataTableDepartamento;
            listDepartamento.ValueMember = dataTableDepartamento.Columns[0].ColumnName;
            listDepartamento.DisplayMember = dataTableDepartamento.Columns[1].ColumnName;
        }

        private void SelectAllReporte()
        {
            int idDepartamento = Convert.ToInt32(listDepartamento.SelectedValue);
            //MessageBox.Show("cambio");
            var tablaReporte = from p in entity.Reporte
                               where p.FK_IdDepartamento == idDepartamento
                               select new
                               {
                                   p.IdReporte,
                                   p.Tipo,
                                   p.Estado,
                                   p.FechaDeCreacion,
                                   p.Detalle,
                                   p.Solucion
                               };
            dataGridReporte.DataSource = tablaReporte.CopyAnonymusToDataTable();
            dataGridReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void listDepartamento_Click(object sender, EventArgs e)
        {
            SelectAllReporte();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(listDepartamento.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un departamento de la lista.");
                return;
            }
            if (!checkBox1.Checked)
            {
                MessageBox.Show("Marque la casilla del estado de la solucion.");
                return;
            }
            if (textBox1.Text ==string.Empty)
            {
                MessageBox.Show("Los detalles de la solucion no pueden ir vacios.");
                return;
            }
            if(dataGridReporte.SelectedRows.Count > 0)
            {
                try
                {
                    //Actulizacion del reporte
                    idReporte = Convert.ToInt32(dataGridReporte.SelectedCells[0].Value);
                    var tablaReporte = (from p in entity.Reporte
                                        where p.IdReporte == idReporte
                                        select p).FirstOrDefault();

                    tablaReporte.Estado = checkBox1.Checked;
                    tablaReporte.Solucion = textBox1.Text;

                    entity.SaveChanges();
                    SelectAllReporte();
                    LimpiezaDeCampos();
                    MessageBox.Show("Los datos se actualizaron.");

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
            else
            {
                MessageBox.Show("Seleccione el reporte que quiere solucionar.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiezaDeCampos();
        }
        private void LimpiezaDeCampos()
        {
            textBox1.Text = string.Empty;
            checkBox1.Checked = false;
        }
    }
}
