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
    public partial class FRMIngreso : Form
    {
        public int idEmpleado;
        private bool IsNuevo;
        private DataTable dtDetalle;
        private decimal totalpagado = 0;

        private static FRMIngreso _instancia;
        public static FRMIngreso GetInsancia()
        {
            if (_instancia == null)
            {
                _instancia = new FRMIngreso();
            }
            return _instancia;
        }
        public void setArticulo(string idArticulo, string Articulo)
        {
            this.txtidArticulo.Text = idArticulo;
            this.txtArticulo.Text = Articulo;
        }
        public FRMIngreso()
        {
            InitializeComponent();
            this.MensajeAyuda.SetToolTip(this.txtArticulo, "seleccione un articulo");
            this.MensajeAyuda.SetToolTip(this.txtStockInicial, "ingrese la cantidad de compra");
            this.MensajeAyuda.SetToolTip(this.txtSerie, "ingrese la serie de la factura de ingreso");
            this.txtidArticulo.Visible = false;
            this.txtidIngreso.Visible = false;
            this.txtArticulo.ReadOnly = true;

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
            this.txtidIngreso.Text = string.Empty;
            this.txtArticulo.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.lblTotalPagado.Text = "0,0";

            this.CrearTabla();
        }
        private void limpiarDetalle()
        {

            this.txtidArticulo.Text = string.Empty;
            this.txtArticulo.Text = string.Empty;
            this.txtPrecioCompra.Text = string.Empty;
            this.txtPrecioVenta.Text = string.Empty;
            this.txtStockInicial.Text = string.Empty;
            this.lblTotalPagado.Text = "0,0";
        }

        //habilitar cajas de texto
        private void Habilitar(bool valor)
        {
            this.txtidIngreso.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtArticulo.ReadOnly = !valor;
            this.txtStockInicial.ReadOnly = !valor;
            this.txtPrecioCompra.ReadOnly = !valor;
            this.txtPrecioVenta.ReadOnly = !valor;
            this.dtFechaIngreso.Enabled = valor;
            this.btnBuscarArticulo.Enabled = valor;
            this.btnAgregar.Enabled = valor;
            this.btnQuitar.Enabled = valor;

        }

        //habilitar botones
        private void Botones()
        {
            if (this.IsNuevo)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            this.dgIngreso.Columns[0].Visible = false;
            this.dgIngreso.Columns[1].Visible = false;

        }

        private void mostrar()
        {
            this.dgIngreso.DataSource = NIngreso.Mostrar();
            this.OcultarColumnas();
            lblTotalArticulos.Text = "Total de registros: " + Convert.ToString(dgIngreso.Rows.Count);
        }

        private void BuscarFechas()
        {
            this.dgIngreso.DataSource = NIngreso.FechaIngreso(this.dtFechaInicio.Value.ToString("dd/MM/yyyy"), this.dtFechaFinal.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotalArticulos.Text = "Total de registros: " + Convert.ToString(dgIngreso.Rows.Count);
        }
        private void MostrarDetalle()
        {
            this.dgDetalle.DataSource = NIngreso.MostrarDetalle(this.txtidIngreso.Text);

        }

        private void CrearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("idArticulo", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("ARTICULO", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("PRECIO DE COMPRA", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("PRECIO DE VENTA", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("STOCK INICIAL", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("SUBTOTAL", System.Type.GetType("System.Decimal"));

            this.dgDetalle.DataSource = this.dtDetalle;

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmVistaArticulo_Ingreso vistaArticulo = new FrmVistaArticulo_Ingreso();
            vistaArticulo.ShowDialog();

        }

        private void FRMIngreso_Load(object sender, EventArgs e)
        {
            this.mostrar();
            this.Habilitar(false);
            this.Botones();
            this.CrearTabla();
        }

        private void FRMIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarFechas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Desea Anular El Registro De La Base de Datos", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string llave;
                    string eliminar = "";

                    foreach (DataGridViewRow row in dgIngreso.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            llave = Convert.ToString(row.Cells[1].Value);
                            eliminar = NIngreso.Anular(Convert.ToInt32(llave));

                            if (eliminar.Equals("OK"))
                            {
                                this.mensajeOk(" SE ANULO EL INGRESO");

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

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dgIngreso.Columns[0].Visible = true;
            }
            else
            {
                this.dgIngreso.Columns[0].Visible = false;

            }
        }

        private void dgIngreso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgIngreso.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgIngreso.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.Botones();
            this.limpiar();
            this.Habilitar(true);
            this.txtSerie.Focus();
            this.limpiarDetalle();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(false);
            this.limpiarDetalle();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";

                if (this.txtSerie.Text == string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtSerie, "ingrese el numero de factura de ingreso");

                }
                else
                {


                    if (this.IsNuevo)
                    {
                        rpta = NIngreso.Insertar(this.idEmpleado, dtFechaIngreso.Value,this.txtSerie.Text, "EMITIDO", dtDetalle);

                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.mensajeOk("se registro el Articulo");
                        }

                    }
                    else
                    {
                        this.mensajeError(rpta);
                    }

                    this.IsNuevo = false;
                    this.Botones();
                    this.limpiar();
                    this.limpiarDetalle();
                    this.mostrar();
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {


                if (this.txtArticulo.Text == string.Empty || this.txtStockInicial.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                {
                    mensajeError("falta ingresar algunos datos");
                    errorIcono.SetError(txtArticulo, "ingrese un datos");
                    errorIcono.SetError(txtStockInicial, "Seleccione un datos");
                    errorIcono.SetError(txtPrecioCompra, "ingrese un datos");
                    errorIcono.SetError(txtPrecioVenta, "ingrese un datos");

                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idArticulo"]) == Convert.ToInt32(this.txtArticulo))
                        {
                            registrar = false;
                            this.mensajeError("YA se encuentra el Articulo En la Tabla");
                        }
                    }
                    if (registrar)
                    {
                        decimal subTotal = Convert.ToDecimal(this.txtStockInicial.Text) * Convert.ToDecimal(this.txtPrecioCompra.Text);
                        totalpagado = totalpagado + subTotal;
                        //this.lblTotalPagado.Text = totalpagado.ToString("#0.00#");
                        DataRow row = this.dtDetalle.NewRow();
                        row["idArticulo"] = Convert.ToInt32(this.txtidArticulo.Text);
                        row["ARTICULO"] = this.txtArticulo.Text;
                        row["PRECIO DE COMPRA"] = Convert.ToDecimal(this.txtPrecioCompra.Text);
                        row["PRECIO DE VENTA"] = Convert.ToDecimal(this.txtPrecioVenta.Text);
                        row["STOCK INICIAL"] = Convert.ToInt32(this.txtStockInicial.Text);
                        row["SUBTOTAL"] = subTotal;

                        this.dtDetalle.Rows.Add(row);
                        this.limpiarDetalle();

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }


        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = this.dgDetalle.CurrentCell.RowIndex;
                DataRow row = this.dtDetalle.Rows[indiceFila];
                this.totalpagado = this.totalpagado - Convert.ToDecimal(row["SUBTOTAL"].ToString());
                this.lblTotalPagado.Text = totalpagado.ToString("#0.00#");
                this.dtDetalle.Rows.Remove(row);
            }
            catch(Exception ex)
            {
                mensajeError("No Hay Fila Para Remover");
            }
        }

        private void dgIngreso_DoubleClick(object sender, EventArgs e)
        {
            this.txtidIngreso.Text=Convert.ToString( this.dgIngreso.CurrentRow.Cells["Ing_id"].Value);
            this.dtFechaIngreso.Value = Convert.ToDateTime(this.dgIngreso.CurrentRow.Cells["FECHA"].Value);
            this.lblTotalPagado.Text = Convert.ToString(this.dgIngreso.CurrentRow.Cells["total"].Value);
            this.MostrarDetalle();
            this.tabControl1.SelectedIndex = 1;
        }

        private void dgDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
