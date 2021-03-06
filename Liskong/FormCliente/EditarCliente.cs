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
    public partial class EditarCliente : Form
    {
        LiskongEntities entity = new LiskongEntities();
        long idCliente = 0;
        bool editar = false;
        public EditarCliente()
        {
            InitializeComponent();
        }

        private void EditarCliente_Load(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void SelectAll()
        {
            var tablaCliente = from c in entity.Cliente
                               select new
                               {
                                   c.IdCliente,
                                   c.NumeroDeIdentidad,
                                   c.NombreCompleto,
                                   c.Ciudad,
                                   c.Pais,
                                   c.Telefono,
                                   c.Correo
                               };
            dataGridCliente.DataSource = tablaCliente.CopyAnonymusToDataTable();
            dataGridCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtNumeroDeIdentidad.ReadOnly = txtNombreCompleto.ReadOnly = txtCiudad.ReadOnly =
            txtPais.ReadOnly = txtTelefono.ReadOnly = txtCorreo.ReadOnly = false;

            btnEditar.Visible = false;
            btnCancelar.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNumeroDeIdentidad.ReadOnly = txtNombreCompleto.ReadOnly = txtCiudad.ReadOnly =
            txtPais.ReadOnly = txtTelefono.ReadOnly = txtCorreo.ReadOnly = true;

            btnEditar.Visible = true;
            btnCancelar.Visible = false;
        }

        //Validar fila seleccionada del data grid
        private void dataGridCliente_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridCliente.RowCount > 0)
            {
                try
                {
                    idCliente = Convert.ToInt64(dataGridCliente.SelectedCells[0].Value);
                    var rowCliente = entity.Cliente.FirstOrDefault(x => x.IdCliente == idCliente);
                    txtNumeroDeIdentidad.Text = rowCliente.NumeroDeIdentidad;
                    txtNombreCompleto.Text = rowCliente.NombreCompleto;
                    txtCiudad.Text = rowCliente.Ciudad;
                    txtPais.Text = rowCliente.Pais;
                    txtTelefono.Text = Convert.ToString(rowCliente.Telefono);
                    txtCorreo.Text = rowCliente.Correo;

                    editar = true; 
                    
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (editar)
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

                var rowCliente = entity.Cliente.FirstOrDefault(x => x.IdCliente == idCliente);
                rowCliente.NumeroDeIdentidad = txtNumeroDeIdentidad.Text;
                rowCliente.NombreCompleto = txtNombreCompleto.Text;
                rowCliente.Ciudad = txtCiudad.Text;
                rowCliente.Pais = txtPais.Text;
                rowCliente.Telefono = Convert.ToInt32(txtTelefono.Text);
                rowCliente.Correo = txtCorreo.Text;

                entity.SaveChanges();

                MessageBox.Show("Se guardaron los datos");
            }

            txtNumeroDeIdentidad.ReadOnly = txtNombreCompleto.ReadOnly = txtCiudad.ReadOnly =
            txtPais.ReadOnly = txtTelefono.ReadOnly = txtCorreo.ReadOnly = true;

            btnEditar.Visible = true;
            btnCancelar.Visible = false;

            idCliente = 0;
            editar = false;

            SelectAll();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridCliente.RowCount > 0)
            {
                //Obtener nombre del elemento seleccionado
                var rowCliente = entity.Cliente.FirstOrDefault(x => x.IdCliente == idCliente);
                var clienteQueEliminara = rowCliente.NombreCompleto;

                //Elementos para el MessageBox
                string message = "¿Seguro que quiere eliminar " + clienteQueEliminara + "?";
                string title = "Eliminar cliente";

                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        //Eliminar el departamento de la base de datos
                        entity.Cliente.RemoveRange(entity.Cliente.Where(x => x.IdCliente == idCliente));
                        entity.SaveChanges();
                        SelectAll();

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione el cliente que desea eliminar.");
            }
        }
    }
}
