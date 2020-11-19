using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FRMCategoria : Form
    {

        private bool Nuevo = false;
        private bool Editar = false;
        public FRMCategoria()
        {
            InitializeComponent();


            this.MensajeAyuda.SetToolTip(this.txtNombre, "ingrese el nombre de la categoria");
            this.MensajeAyuda.SetToolTip(this.txtBuscar, "ingrese el nombre de la categoria a buscar");
            this.MensajeAyuda.SetToolTip(this.txtDescripcion, "caracteristicas de  la categoria");

            this.txtCat_id.Visible = false;
        }

        
        private void mensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //error
        private void mensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void limpiar()
        {
            this.txtCat_id.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
                      

        }
        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
        }

        //habilitar botones
        private void Botones()
        {
            if (this.Nuevo || this.Editar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            this.dgCategorias.Columns[0].Visible = false;
            this.dgCategorias.Columns[1].Visible = false;
        }

        private void mostrar()
        {
            this.dgCategorias.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotalCategorias.Text = "Total de registros: " + Convert.ToString(dgCategorias.Rows.Count);
        }

        private void BuscarCategoria()
        {
            this.dgCategorias.DataSource = NCategoria.BuscarCategoria(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotalCategorias.Text = "Total de registros: " + Convert.ToString(dgCategorias.Rows.Count);
        }

        private void FRMCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            this.mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.Nuevo = true;
            this.Editar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";

                if (this.txtNombre.Text == string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtNombre, "ingrese un datos");

                }
                else
                {

                    if (this.Nuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtNombre.Text.ToUpper(), this.txtDescripcion.Text.ToUpper());

                    }
                    else
                    {
                        rpta = NCategoria.Editar(Convert.ToInt32(txtCat_id.Text), this.txtNombre.Text.ToUpper(), this.txtDescripcion.Text.ToUpper());
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.Nuevo)
                        {
                            this.mensajeOk("se registro la Categoria");
                        }
                        else
                        {
                            this.mensajeOk("se actualizaron los datos de la Categoria");
                        }
                    }
                    else
                    {
                        this.mensajeError(rpta);
                    }

                    this.Nuevo = false;
                    this.Editar = false;
                    this.Botones();
                    this.limpiar();
                    this.mostrar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgCategorias_DoubleClick(object sender, EventArgs e)
        {
            this.txtCat_id.Text = Convert.ToString(this.dgCategorias.CurrentRow.Cells["Cat_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgCategorias.CurrentRow.Cells["NOMBRE"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dgCategorias.CurrentRow.Cells["DESCRIPCION"].Value);
           
            this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {

            if (chkEliminar.Checked)
            {
                this.dgCategorias.Columns[0].Visible = true;
            }
            else
            {
                this.dgCategorias.Columns[0].Visible = false;

            }

        }

        private void dgCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgCategorias.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgCategorias.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Eliminar LA CATEGORIA De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string llave;
                    string eliminar = "";

                    foreach (DataGridViewRow row in dgCategorias.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NCategoria.Eliminar(Convert.ToInt32(llave));

                            if (eliminar.Equals("OK"))
                            {
                                this.mensajeOk(" SE ELIMINO EL REGISTRO");

                            }
                            else
                            {
                                this.mensajeError(eliminar);
                            }
                        }
                    }
                    this.mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtCat_id.Text.Equals(""))
            {
                this.Editar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.mensajeError("Debe Selecionar El Registro A Modificar");
                this.tabControl1.SelectedIndex = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Nuevo = false;
            this.Editar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(false);
        }

        private void FRMCategoria_Resize(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void TableroControl()
        {
            this.tabControl1.SelectedIndex = 1;

        }
    }
}
