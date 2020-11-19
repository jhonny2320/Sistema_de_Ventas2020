using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FRMSisVentas : Form
    {
        private int childFormNumber = 0;

        public string idEmpleado = "";
        public string Emp_Nombre = "";
        public string Emp_Apellido = "";
        public string Emp_Acceso = "";


        public FRMSisVentas()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMIngreso frmIngreso = FRMIngreso.GetInsancia();
            frmIngreso.MdiParent = this;
            frmIngreso.Show();
            frmIngreso.idEmpleado = Convert.ToInt32(this.idEmpleado);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin_Empleado login = new frmLogin_Empleado();
            login.Show();
            this.Hide();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMCategoria categorias = new FRMCategoria();
            categorias.MdiParent = this;
            categorias.Show();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMArticulo articulo = new FRMArticulo();
            articulo.MdiParent = this;
            articulo.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMClientes clientes = new FRMClientes();
            clientes.MdiParent = this;
            clientes.Show();
        }

        private void trabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMPLEADOS empleados = new EMPLEADOS();
            empleados.MdiParent = this;
            empleados.Show();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMServicios servicios = new FRMServicios();
            servicios.MdiParent = this;
            servicios.Show();
        }

        private void FRMSisVentas_Load(object sender, EventArgs e)
        {
            GestioUsuario();
        }
        private void GestioUsuario()
        {
            if (Emp_Acceso == "ADMINISTRADOR")
            {
                this.MnAlmacen.Enabled = true;
                this.MnCompras.Enabled = true;
                this.MnVentas.Enabled = true;
                this.MnMantenimiento.Enabled = true;
                this.MnConsultas.Enabled = true;
                this.MnHerramientas.Enabled = true;
                this.TsCompras.Enabled = true;
                this.TsVentas.Enabled = true;
            }
            else
            {
                if (Emp_Acceso == "USUARIO")
                {
                    this.MnAlmacen.Enabled = false;
                    this.MnCompras.Enabled = false;
                    this.MnVentas.Enabled = true;
                    this.MnMantenimiento.Enabled = false;
                    this.MnConsultas.Enabled = false;
                    this.MnHerramientas.Enabled = true;
                    this.TsCompras.Enabled = false;
                    this.TsVentas.Enabled = true;
                }
                else
                {
                    this.MnAlmacen.Enabled = false;
                    this.MnCompras.Enabled = false;
                    this.MnVentas.Enabled = false;
                    this.MnMantenimiento.Enabled = false;
                    this.MnConsultas.Enabled = false;
                    this.MnHerramientas.Enabled = false;
                    this.TsCompras.Enabled = false;
                    this.TsVentas.Enabled = false;
                }
            }
        }

    }
}
