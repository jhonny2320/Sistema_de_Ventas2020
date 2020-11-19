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
    public partial class FRMServicios : Form
    {
        private bool Nuevo = false;
        private bool Editar = false;
        public FRMServicios()
        {
            InitializeComponent();


            this.MensajeAyuda.SetToolTip(this.txtNombre, "ingrese el nombre del Servicio");
            this.MensajeAyuda.SetToolTip(this.txtBuscar, "ingrese el nombre del Servicio a buscar");
            this.MensajeAyuda.SetToolTip(this.txtCosto, "ingrese el Costo del Servicio");

            this.txtSer_id.Visible = false;
            
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
            this.txtSer_id.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtCosto.Text = string.Empty;


        }
        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtCosto.ReadOnly = !valor;
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
            this.dgServicios.Columns[0].Visible = false;
        }
        private void mostrar()
        {
            this.dgServicios.DataSource = NServicios.Mostrar();
            this.OcultarColumnas();
            lblTotalServicios.Text = "Total de registros: " + Convert.ToString(dgServicios.Rows.Count);
        }

        private void BuscarServicio()
        {
            this.dgServicios.DataSource = NServicios.BuscarServicio(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotalServicios.Text = "Total de registros: " + Convert.ToString(dgServicios.Rows.Count);
        }

        private void FRMServicios_Load(object sender, EventArgs e)
        {
            //this.Top = 0;
            //this.Left = 0;
            this.mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarServicio();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarServicio();
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

                if (this.txtNombre.Text == string.Empty || this.txtCosto.Text==string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtNombre, "ingrese un datos");
                    errorIcono.SetError(txtCosto, "ingrese un valor");

                }
                else
                {

                    if (this.Nuevo)
                    {
                        rpta = NServicios.Insertar(this.txtNombre.Text.ToUpper(), Convert.ToDecimal(this.txtCosto.Text));

                    }
                    else
                    {
                        rpta = NServicios.Editar(Convert.ToInt32(txtSer_id.Text), this.txtNombre.Text.ToUpper(), Convert.ToDecimal(this.txtCosto.Text));
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.Nuevo)
                        {
                            this.mensajeOk("se registro El Servicio");
                        }
                        else
                        {
                            this.mensajeOk("se actualizaron los datos del Servicio");
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

        private void dgServicios_DoubleClick(object sender, EventArgs e)
        {
            this.txtSer_id.Text = Convert.ToString(this.dgServicios.CurrentRow.Cells["Ser_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgServicios.CurrentRow.Cells["SERVICIO"].Value);
            this.txtCosto.Text = Convert.ToString(this.dgServicios.CurrentRow.Cells["COSTO"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dgServicios.Columns[0].Visible = true;
            }
            else
            {
                this.dgServicios.Columns[0].Visible = false;

            }
        }

        private void dgServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgServicios.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgServicios.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Eliminar El Servicio De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string llave;
                    string eliminar = "";

                    foreach (DataGridViewRow row in dgServicios.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NServicios.Eliminar(Convert.ToInt32(llave));

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Nuevo = false;
            this.Editar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(false);
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtSer_id.Text.Equals(""))
            {
                this.Editar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.mensajeError("Debe Exportar El Registro A Modificar");
                this.tabControl1.SelectedIndex = 0;
            }
        }

       
    }
}


