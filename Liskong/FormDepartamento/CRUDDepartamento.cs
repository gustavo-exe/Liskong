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
            btnCancelar.Visible = txtNombre.Enabled = true;
            dataGridDepartamento.ClearSelection();
            editar = false;
            txtNombre.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (editar)
            {
                //Guardado de cambios al editar
                var rowDepartamento = entity.Departamento.FirstOrDefault(x => x.IdDepartamento == idDepartamento);
                rowDepartamento.Nombre = txtNombre.Text ;
                entity.SaveChanges();
                SelectAllDepartamento();
            }
            else
            {
                try
                {
                    //Ingresar un nuevo departamento
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
            //Estados de los componentes
            btnCancelar.Visible = txtNombre.Enabled = false;
            editar = false;
            btnNuevo.Visible = true;
            txtNombre.ReadOnly  = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = btnCancelar.Visible = false;
            txtNombre.Text = string.Empty;
            txtNombre.ReadOnly =  btnNuevo.Visible = true;
        }

        private void dataGridDepartamento_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridDepartamento.RowCount >0)
            {
                try
                {
                    //Habiltar la edicion del elemento seleccionado
                    idDepartamento = Convert.ToInt64(dataGridDepartamento.SelectedCells[0].Value);
                    var rowDepartamento = entity.Departamento.FirstOrDefault(x => x.IdDepartamento == idDepartamento);
                    txtNombre.Text = rowDepartamento.Nombre;
                    editar = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnNuevo.Visible = txtNombre.ReadOnly = false;
            txtNombre.Enabled = btnCancelar.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Obtener nombre del elemento seleccionado
            var rowDepartamento = entity.Departamento.FirstOrDefault(x => x.IdDepartamento == idDepartamento);
            var departamentoQueEliminara = rowDepartamento.Nombre;

            //Elementos para el MessageBox
            string message = "Seguro que quiere eliminar " + departamentoQueEliminara;
            string title = "Eliminar departamento";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                try
                {
                    //Eliminar el departamento de la base de datos
                    entity.Departamento.RemoveRange(entity.Departamento.Where(x => x.IdDepartamento == idDepartamento));
                    entity.SaveChanges();
                    SelectAllDepartamento();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
