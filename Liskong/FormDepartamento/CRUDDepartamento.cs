using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liskong.FormDepartamento
{
    public partial class CRUDDepartamento : Form
    {
        LiskongEntities entity = new LiskongEntities();
        long idDepartamento = 0;
        bool editar = false;
        public CRUDDepartamento()
        {
            InitializeComponent();
        }
        private void SelectAllDepartamento()
        {
            var tablaDepartamento = from p in entity.Departamento
                                    select new
                                    {
                                        p.IdDepartamento,
                                        p.Nombre
                                    };
            dataGridDepartamento.DataSource = tablaDepartamento.CopyAnonymusToDataTable();
            dataGridDepartamento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void CRUDDepartamento_Load(object sender, EventArgs e)
        {
            SelectAllDepartamento();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.ReadOnly = btnNuevo.Visible = false;
            btnCancelar.Visible = true;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (editar)
            {
                var rowDepartamento = entity.Departamento.FirstOrDefault(x => x.IdDepartamento == idDepartamento);
                rowDepartamento.Nombre = txtNombre.Text ;
                entity.SaveChanges();
                SelectAllDepartamento();
            }
            else
            {
                try
                {
                    Departamento rowDepartamento = new Departamento();
                    rowDepartamento.Nombre = txtNombre.Text;

                    entity.Departamento.Add(rowDepartamento);
                    entity.SaveChanges();
                    SelectAllDepartamento();
                    MessageBox.Show("Departamento guardado");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            btnCancelar.Visible = false;
            btnNuevo.Visible = true;
            txtNombre.ReadOnly = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtNombre.ReadOnly = btnNuevo.Visible = true;
        }

        private void dataGridDepartamento_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridDepartamento.RowCount >0)
            {
                try
                {
                    idDepartamento = Convert.ToInt64(dataGridDepartamento.SelectedCells[0].Value);
                    var rowDepartamento = entity.Departamento.FirstOrDefault(x => x.IdDepartamento == idDepartamento);
                    txtNombre.Text = rowDepartamento.Nombre;
                    editar = true;
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnNuevo.Visible = txtNombre.ReadOnly = false;
            btnCancelar.Visible = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //entity.Departamento.RemoveRange(entity.Departamento.Where(x => x.IdDepartamento == idDepartamento));
            //entity.SaveChanges();
            //SelectAllDepartamento();
        }
    }
}
