using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DDetalle_Ingreso
    {
        private int _IdDetalle_Ingreso;
        private int _IdIngreso;
        private int _IdArticulo;
        private decimal _Precio_Compra;
        private decimal _Precio_Venta;
        private int _Stock_Inicial;
        private int _Stock_Actual;

        public int IdDetalle_Ingreso
        {
            get
            {
                return _IdDetalle_Ingreso;
            }

            set
            {
                _IdDetalle_Ingreso = value;
            }
        }
        public int IdIngreso
        {
            get
            {
                return _IdIngreso;
            }

            set
            {
                _IdIngreso = value;
            }
        }
        public int IdArticulo
        {
            get
            {
                return _IdArticulo;
            }

            set
            {
                _IdArticulo = value;
            }
        }
        public decimal Precio_Compra
        {
            get
            {
                return _Precio_Compra;
            }

            set
            {
                _Precio_Compra = value;
            }
        }
        public decimal Precio_Venta
        {
            get
            {
                return _Precio_Venta;
            }

            set
            {
                _Precio_Venta = value;
            }
        }
        public int Stock_Inicial
        {
            get
            {
                return _Stock_Inicial;
            }

            set
            {
                _Stock_Inicial = value;
            }
        }
        public int Stock_Actual
        {
            get
            {
                return _Stock_Actual;
            }

            set
            {
                _Stock_Actual = value;
            }
        }

        //CONSTRUCTORES
        public DDetalle_Ingreso()
        {

        }
        public DDetalle_Ingreso(int idDetalle_ingreso, int idIngreso, int idArticulo,
            decimal precio_compra, decimal precio_venta, int stock_inicial, int stock_actual)
        {
            this.IdDetalle_Ingreso = idDetalle_ingreso;
            this.IdIngreso = idIngreso;
            this.IdArticulo = idArticulo;
            this.Precio_Compra = precio_compra;
            this.Precio_Venta = precio_venta;
            this.Stock_Inicial = stock_inicial;
            this.Stock_Actual = stock_actual;
        }

        //metodo de insertar

        public string Insertar(DDetalle_Ingreso Detalle_Ingreso,
            ref SqlConnection sqlCon,ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {
               
               
                //conexion al procedimiento almacenado
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlCon;
                sqlcmd.Transaction = SqlTra;
                sqlcmd.CommandText = "DetalleIngreso_Insertar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter paridDetalle_Ingreso = new SqlParameter();
                paridDetalle_Ingreso.ParameterName = "@idDetalle_ingreso";
                paridDetalle_Ingreso.SqlDbType = SqlDbType.Int;
                paridDetalle_Ingreso.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(paridDetalle_Ingreso);

                

                SqlParameter paridIngreso = new SqlParameter();
                paridIngreso.ParameterName = "@idIngreso";
                paridIngreso.SqlDbType = SqlDbType.Int;
                paridIngreso.Value = Detalle_Ingreso.IdIngreso;
                sqlcmd.Parameters.Add(paridIngreso);


                SqlParameter paridArticulo = new SqlParameter();
                paridArticulo.ParameterName = "@idArticulo";
                paridArticulo.SqlDbType = SqlDbType.Int;
                paridArticulo.Value = Detalle_Ingreso.IdArticulo;
                sqlcmd.Parameters.Add(paridArticulo);

                SqlParameter parPrecio_Compra = new SqlParameter();
                parPrecio_Compra.ParameterName = "@precio_Compra";
                parPrecio_Compra.SqlDbType = SqlDbType.Money;
                parPrecio_Compra.Value = Detalle_Ingreso.Precio_Compra;
                sqlcmd.Parameters.Add(parPrecio_Compra);


                SqlParameter parPrecio_Venta = new SqlParameter();
                parPrecio_Venta.ParameterName = "@precio_Venta";
                parPrecio_Venta.SqlDbType = SqlDbType.Money;
                parPrecio_Venta.Value = Detalle_Ingreso.Precio_Venta;
                sqlcmd.Parameters.Add(parPrecio_Venta);

                SqlParameter parStock_Inicial = new SqlParameter();
                parStock_Inicial.ParameterName = "@stock_Inicial";
                parStock_Inicial.SqlDbType = SqlDbType.Int;
                parStock_Inicial.Value = Detalle_Ingreso.Stock_Inicial;
                sqlcmd.Parameters.Add(parStock_Inicial);

                SqlParameter parStock_Actual = new SqlParameter();
                parStock_Actual.ParameterName = "@stock_Actual";
                parStock_Actual.SqlDbType = SqlDbType.Int;
                parStock_Actual.Value = Detalle_Ingreso.Stock_Actual;
                sqlcmd.Parameters.Add(parStock_Actual);
                                              


                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE REALIZO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            
            return rpta;
        }

    }
}
