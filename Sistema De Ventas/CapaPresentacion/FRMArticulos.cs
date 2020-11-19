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
    public partial class FRMArticulo : Form
    {
        private bool Nuevo = false;
        private bool Editar = false;

        
        
        
        public FRMArticulo()
        {
            InitializeComponent();
            this.MensajeAyuda.SetToolTip(this.txtNombre, "ingrese el nombre del Articulo");
            this.MensajeAyuda.SetToolTip(this.txtBuscar, "ingrese el nombre del Articulo a buscar");
            this.MensajeAyuda.SetToolTip(this.txtDescripcion, "caracteristicas del Articulo");
            this.MensajeAyuda.SetToolTip(this.txtCodigo, "ingrese el codigo del Articulo");
            this.MensajeAyuda.SetToolTip(this.cbCategoria, "Seleccione la Categoria");
            this.MensajeAyuda.SetToolTip(this.btnAgregar, "Agregar Una Nueva Categoria");

            this.txtArt_id.Visible = false;
            this.ListadoCategoria();          

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
            this.txtArt_id.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.cbCategoria.Text = string.Empty;
            this.txtCodigo.Text = string.Empty;
        }
        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.cbCategoria.Enabled = valor;
            this.txtCodigo.ReadOnly = !valor;
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
                this.btnAgregar.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnAgregar.Enabled = false;

            }
        }

        private void OcultarColumnas()
        {
            this.dgArticulos.Columns[0].Visible = false;
            this.dgArticulos.Columns[1].Visible = false;
            this.dgArticulos.Columns[5].Visible = false;
        }

        private void mostrar()
        {
            this.dgArticulos.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotalArticulos.Text = "Total de registros: " + Convert.ToString(dgArticulos.Rows.Count);
        }

        private void BuscarArticulo()
        {
            this.dgArticulos.DataSource = NArticulo.BuscarArticulo(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotalArticulos.Text = "Total de registros: " + Convert.ToString(dgArticulos.Rows.Count);
        }

        private void ListadoCategoria()
        {
            cbCategoria.DataSource = NCategoria.Mostrar();
            cbCategoria.ValueMember = "Cat_id";
            cbCategoria.DisplayMember = "NOMBRE";

        }
            private void ARTICULOS_Load(object sender, EventArgs e)
        {
            //this.Top = 0;
            //this.Left = 0;
            this.mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarArticulo();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarArticulo();
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

                if (this.txtCodigo.Text==string.Empty || this.txtNombre.Text == string.Empty || this.cbCategoria.ValueMember== string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtCodigo, "ingrese un datos");
                    errorIcono.SetError(txtNombre, "ingrese un datos");
                    errorIcono.SetError(cbCategoria, "Seleccione un datos");

                }
                else
                {
                    

                    if (this.Nuevo)
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text.Trim(), this.txtNombre.Text.ToUpper(), this.txtDescripcion.Text.ToUpper() ,Convert.ToInt32(this.cbCategoria.SelectedValue));

                    }
                    else
                    {
                        rpta = NArticulo.Editar(Convert.ToInt32( this.txtArt_id.Text), this.txtCodigo.Text.Trim(), this.txtNombre.Text.ToUpper(), this.txtDescripcion.Text.ToUpper(), Convert.ToInt32(this.cbCategoria.SelectedValue));
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.Nuevo)
                        {
                            this.mensajeOk("se registro el Articulo");
                        }
                        else
                        {
                            this.mensajeOk("se actualizaron los datos del Articulo");
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtArt_id.Text.Equals(""))
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

        private void dgArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dgArticulos.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgArticulos.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void dgArticulos_DoubleClick(object sender, EventArgs e)
        {
            this.txtArt_id.Text = Convert.ToString(this.dgArticulos.CurrentRow.Cells["Art_id"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dgArticulos.CurrentRow.Cells["CODIGO"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgArticulos.CurrentRow.Cells["NOMBRE"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dgArticulos.CurrentRow.Cells["DESCRIPCION"].Value);
            this.cbCategoria.SelectedValue = Convert.ToString(this.dgArticulos.CurrentRow.Cells["Art_idCategoria"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dgArticulos.Columns[0].Visible = true;
            }
            else
            {
                this.dgArticulos.Columns[0].Visible = false;

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Eliminar El Articulo De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string llave;
                    string eliminar = "";

                    foreach (DataGridViewRow row in dgArticulos.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NArticulo.Eliminar(Convert.ToInt32(llave));

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

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblTotalArticulos_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FRMCategoria categoria = new FRMCategoria();
            categoria.TableroControl();
            categoria.ShowDialog();
            this.ListadoCategoria();

        }
    }
}