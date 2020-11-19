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
    public partial class FRMClientes : Form
    {
        private bool Nuevo = false;
        private bool Editar = false;
        public FRMClientes()
        {
            InitializeComponent();
            this.MensajeAyuda.SetToolTip(this.txtNombre,"ingrese el nombre del cliente");
            this.MensajeAyuda.SetToolTip(this.txtBuscarCliente, "ingrese el nombre del cliente a buscar");
            this.MensajeAyuda.SetToolTip(this.cbidTD, "seleccione el tipo de documento");

            this.txtCli_id.Visible = false;
            
            this.CBTipoDocumento();
        }
        //informacion
        private void mensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //error
        private void mensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //limpiar cajas de texto
        private void limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtDocumento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
        }
        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.cbidTD.Enabled= valor;
            this.txtDocumento.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;

        }

        //habilitar botones
        private void Botones()
        {
            if(this.Nuevo || this.Editar)
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
        //ocultar columnas 
        private void OcultarColumnas()
        {
            this.dgClientes.Columns[0].Visible = false;
            this.dgClientes.Columns[1].Visible = false;
            this.dgClientes.Columns[4].Visible = false;

        }

        //mostrar clientes registrados
        private void mostrar()
        {
            this.dgClientes.DataSource = NCliente.Mostrar();
            this.OcultarColumnas();
            lblTotalClientes.Text = "Total de registros: " + Convert.ToString(dgClientes.Rows.Count);
        }

        private void BuscarCliente()
        {
            this.dgClientes.DataSource = NCliente.BuscarCliente(this.txtBuscarCliente.Text);
            this.OcultarColumnas();
            lblTotalClientes.Text = "Total de registros: " + Convert.ToString(dgClientes.Rows.Count);
        }

        //llenar tipo de documento en el combo box
        private void CBTipoDocumento()
        {
            cbidTD.DataSource = NTipoDocumento.Mostrar();
            cbidTD.ValueMember = "TD_id";
            cbidTD.DisplayMember = "TD_Nombre";
        }
        private void FRMClientes_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            this.mostrar();
            this.Habilitar(false);
            this.Botones();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarCliente();
        }

        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            this.BuscarCliente();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            this.Nuevo = true;
            this.Editar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";

                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtDocumento.Text == string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtNombre, "ingrese un datos");
                    errorIcono.SetError(txtApellidos, "ingrese un datos");
                    errorIcono.SetError(txtDocumento, "ingrese un datos");
                }
                else
                {

                    if(this.Nuevo)
                    {
                        rpta = NCliente.insertar(this.txtNombre.Text.ToUpper(), 
                            this.txtApellidos.Text.ToUpper(), 
                            Convert.ToInt32(this.cbidTD.SelectedValue),
                            Convert.ToString(this.txtDocumento.Text.ToUpper()), 
                            this.txtTelefono.Text.ToUpper(), 
                            this.txtDireccion.Text.ToUpper());
                            
                    }
                    else
                    {
                        rpta = NCliente.Editar(Convert.ToInt32(txtCli_id.Text),
                            this.txtNombre.Text.ToUpper(),
                            this.txtApellidos.Text.ToUpper(),
                            Convert.ToInt32(this.cbidTD.SelectedValue),
                            Convert.ToString(this.txtDocumento.Text.ToUpper()),
                            this.txtTelefono.Text.ToUpper(),
                            this.txtDireccion.Text.ToUpper());
                    }
                    if(rpta.Equals("OK"))
                    {
                        if(this.Nuevo)
                        {
                            this.mensajeOk("se registro el cliente");
                        }
                        else
                        {
                            this.mensajeOk("se actualizaron los datos del cliente");
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgClientes_DoubleClick(object sender, EventArgs e)
        {
            this.txtCli_id.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["Cli_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["NOMBRES"].Value);
            this.txtApellidos.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["APELLIDOS"].Value);
            this.cbidTD.SelectedText = Convert.ToString(this.dgClientes.CurrentRow.Cells["TIPO_DOCUMENTOS"]);
            this.txtDocumento.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["DOCUMENTOS"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["TELEFONOS"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dgClientes.CurrentRow.Cells["DIRECCIONES"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(chkEliminar.Checked)
            {
                this.dgClientes.Columns[0].Visible = true;
            }
            else
            {
                this.dgClientes.Columns[0].Visible = false;

            }
        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgClientes.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgClientes.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Eliminar El Cliente De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(opcion==DialogResult.OK)
                {
                    string llave;
                    string eliminar="";

                    foreach (DataGridViewRow row in dgClientes.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NCliente.Eliminar(Convert.ToInt32(llave));

                            if(eliminar.Equals("OK"))
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        

        private void cbidTD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if ( !this.txtCli_id.Text.Equals("") )
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

        private void cbidTD_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
