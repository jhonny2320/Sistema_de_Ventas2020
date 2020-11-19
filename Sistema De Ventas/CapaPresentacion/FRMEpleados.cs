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
    public partial class EMPLEADOS : Form
    {

        private bool Nuevo = false;
        private bool Editar = false;
        public EMPLEADOS()
        {
            InitializeComponent();
            this.MensajeAyuda.SetToolTip(this.txtNombre, "ingrese el nombre del Empleado");
            this.MensajeAyuda.SetToolTip(this.cbAcceso, "selecione el nivel de acceso al sistema");
            this.MensajeAyuda.SetToolTip(this.txtUsuario, "ingrese un nombre de usuario");
            this.MensajeAyuda.SetToolTip(this.txtBuscarEmpleado, "ingrese el nombre del Empleado a buscar");
            this.MensajeAyuda.SetToolTip(this.cbidTD, "seleccione el tipo de documento");

            this.txtEmp_id.Visible = false;

            this.CBTipoDocumento();
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
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtDocumento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtUsuario.Text = string.Empty;
            this.txtContraseña.Text = string.Empty;
            this.txtContraseñaC.Text = string.Empty;

        }
        
        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.cbidTD.Enabled = valor;
            this.txtDocumento.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.cbAcceso.Enabled = valor;
            this.txtUsuario.ReadOnly = !valor;
            this.txtContraseña.ReadOnly = !valor;
            this.txtContraseñaC.ReadOnly = !valor;
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
            this.dgEmpleados.Columns[0].Visible = false;
            this.dgEmpleados.Columns[1].Visible = false;
            this.dgEmpleados.Columns[4].Visible = false;
        }

        private void mostrar()
        {
            this.dgEmpleados.DataSource = NEmpleados.Mostrar();
            this.OcultarColumnas();
            lblTotalEmpleados.Text = "Total de registros: " + Convert.ToString(dgEmpleados.Rows.Count);
        }

        private void BuscarEmpleado()
        {
            this.dgEmpleados.DataSource = NEmpleados.BuscarEmpleado(this.txtBuscarEmpleado.Text);
            this.OcultarColumnas();
            lblTotalEmpleados.Text = "Total de registros: " + Convert.ToString(dgEmpleados.Rows.Count);
        }

        private void CBTipoDocumento()
        {
            cbidTD.DataSource = NTipoDocumento.Mostrar();
            cbidTD.ValueMember = "TD_id";
            cbidTD.DisplayMember = "TD_Nombre";
        }
        private void EMPLEADOS_Load(object sender, EventArgs e)

        {
            this.Top = 0;
            this.Left = 0;

            this.mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarEmpleado();
        }

        private void txtBuscarEmpleado_TextChanged(object sender, EventArgs e)
        {
            this.BuscarEmpleado();
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

                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtDocumento.Text == string.Empty || this.txtUsuario.Text == string.Empty || this.txtContraseña.Text == string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtNombre, "ingrese un datos");
                    errorIcono.SetError(txtApellidos, "ingrese un datos");
                    errorIcono.SetError(txtDocumento, "ingrese un datos");
                    errorIcono.SetError(txtUsuario, "ingrese un datos");
                    errorIcono.SetError(txtContraseña, "ingrese un datos");
                }
                else
                {
                    if (txtContraseñaC.Text == txtContraseña.Text)
                    {

                        if (this.Nuevo)
                        {
                            rpta = NEmpleados.Insertar(this.txtNombre.Text.ToUpper(), this.txtApellidos.Text.ToUpper(), Convert.ToInt32(this.cbidTD.SelectedValue),
                                Convert.ToString(this.txtDocumento.Text.ToUpper()), this.txtDireccion.Text.ToUpper(), this.txtTelefono.Text.ToUpper(),
                                this.cbAcceso.Text, this.txtUsuario.Text, this.txtContraseña.Text);

                        }
                        else
                        {
                            rpta = NEmpleados.Editar(Convert.ToInt32(txtEmp_id.Text), this.txtNombre.Text.ToUpper(), this.txtApellidos.Text.ToUpper(), Convert.ToInt32(this.cbidTD.SelectedValue),
                                Convert.ToString(this.txtDocumento.Text.ToUpper()), this.txtDireccion.Text.ToUpper(), this.txtTelefono.Text.ToUpper(),
                                this.cbAcceso.Text, this.txtUsuario.Text, this.txtContraseña.Text);
                        }
                        if (rpta.Equals("OK"))
                        {
                            if (this.Nuevo)
                            {
                                this.mensajeOk("se registro el Empleado");
                                
                                
                            }
                            else
                            {
                                this.mensajeOk("se actualizaron los datos del Empleado");
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
                    else
                    {
                        MessageBox.Show("LAS CONTRASEÑAS NO COINCIDEN");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgEmpleados_DoubleClick(object sender, EventArgs e)
        {
            this.txtEmp_id.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["Emp_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["NOMBRES"].Value);
            this.txtApellidos.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["APELLIDOS"].Value);
            this.cbidTD.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["TIPO_DE_DOCUMENTO"]);
            this.txtDocumento.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["DOCUMENTO"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["DIRECCION"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["TELEFONO"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["ACCESO"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dgEmpleados.CurrentRow.Cells["USUARIO"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dgEmpleados.Columns[0].Visible = true;
            }
            else
            {
                this.dgEmpleados.Columns[0].Visible = false;

            }
        }

        private void dgEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgEmpleados.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgEmpleados.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Eliminar El EMPLEADO De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string llave;
                    string eliminar = "";

                    foreach (DataGridViewRow row in dgEmpleados.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NEmpleados.Eliminar(Convert.ToInt32(llave));

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
            if (!this.txtEmp_id.Text.Equals(""))
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

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            
        }
        public void TableroControl()
        {
            this.tabControl1.SelectedIndex = 1;

            
        }

        private void tpListado_Click(object sender, EventArgs e)
        {

        }
    }
}
