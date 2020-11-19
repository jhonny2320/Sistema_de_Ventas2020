using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DIngreso
    {
        private int _IdIngreso;
        private int _IdEmpleado;
        private DateTime _Fecha;
        private string _Serie;
        private string _Estado;

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
        public int IdEmpleado
        {
            get
            {
                return _IdEmpleado;
            }

            set
            {
                _IdEmpleado = value;
            }
        }
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }

            set
            {
                _Fecha = value;
            }
        }
        public string Serie
        {
            get
            {
                return _Serie;
            }

            set
            {
                _Serie = value;
            }
        }
        public string Estado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }


        public DIngreso()
        {

        }
        public DIngreso(int idIngreso, int idEmpleado, DateTime fecha,string serie, string estado)
        {
            this.IdIngreso = idIngreso;
            this.IdEmpleado = idEmpleado;
            this.Fecha = fecha;
            this.Serie = serie;
            this.Estado = estado;
        }

        public string Insertar(DIngreso Ingresos, List<DDetalle_Ingreso> Detalle)
        {
            string rpta = "";
            SqlConnection Sqlconexion = new SqlConnection();
            try
            {
                //abrir conexion
                Sqlconexion.ConnectionString = Conexion.Conectar;
                Sqlconexion.Open();
                //conexion al procedimiento almacenado
                SqlTransaction SqlTra = Sqlconexion.BeginTransaction();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = Sqlconexion;
                sqlcmd.Transaction = SqlTra;
                sqlcmd.CommandText = "Ingreso_Insertar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter paridIngreso = new SqlParameter();
                paridIngreso.ParameterName = "@idIngreso";
                paridIngreso.SqlDbType = SqlDbType.Int;
                paridIngreso.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(paridIngreso);


                SqlParameter paridEmpleado = new SqlParameter();
                paridEmpleado.ParameterName = "@idEmpleado";
                paridEmpleado.SqlDbType = SqlDbType.Int;
                paridEmpleado.Value = Ingresos.IdEmpleado;
                sqlcmd.Parameters.Add(paridEmpleado);

                SqlParameter parFecha = new SqlParameter();
                parFecha.ParameterName = "@fecha";
                parFecha.SqlDbType = SqlDbType.Date;
                parFecha.Value = Ingresos.Fecha;
                sqlcmd.Parameters.Add(parFecha);

                SqlParameter parSerie = new SqlParameter();
                parSerie.ParameterName = "@serie";
                parSerie.SqlDbType = SqlDbType.VarChar;
                parSerie.Size = 4;
                parSerie.Value = Ingresos.Serie;
                sqlcmd.Parameters.Add(parSerie);

                SqlParameter parEstado = new SqlParameter();
                parEstado.ParameterName = "@estado";
                parEstado.SqlDbType = SqlDbType.VarChar;
                parEstado.Size = 7;
                parEstado.Value = Ingresos.Estado;
                sqlcmd.Parameters.Add(parEstado);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE REALIZO EL REGISTRO  DE INGRESO";

                if (rpta.Equals("OK"))
                {
                    //obtener codigo del ingreso generado
                    this.IdIngreso = Convert.ToInt32(sqlcmd.Parameters["@idIngreso"].Value);
                    foreach (DDetalle_Ingreso det in Detalle)
                    {

                        det.IdIngreso = this.IdIngreso;
                        rpta = det.Insertar(det, ref Sqlconexion, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
                    }
                }
                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (Sqlconexion.State == ConnectionState.Open)
                {
                    Sqlconexion.Close();
                    Sqlconexion.Dispose();
                }
            }
            return rpta;
        }
        public string Anular(DIngreso Ingreso)
        {
            string rpta = "";
            SqlConnection Sqlconexion = new SqlConnection();
            try
            {
                //abrir conexion
                Sqlconexion.ConnectionString = Conexion.Conectar;
                Sqlconexion.Open();
                //conexion al procedimiento almacenado
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = Sqlconexion;
                sqlcmd.CommandText = "Ingreso_Anular";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter paridIngreso = new SqlParameter();
                paridIngreso.ParameterName = "@idIngreso";
                paridIngreso.SqlDbType = SqlDbType.Int;
                paridIngreso.Value = Ingreso.IdIngreso;
                sqlcmd.Parameters.Add(paridIngreso);

                //conexion nombre

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ANULO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (Sqlconexion.State == ConnectionState.Open)
                {
                    Sqlconexion.Close();
                    Sqlconexion.Dispose();
                }
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable DtMostrar = new DataTable("Ingreso");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Ingreso_Mostrar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(DtMostrar);
            }
            catch (Exception ex)
            {
                DtMostrar = null;
            }
            return DtMostrar;
        }

        public DataTable FechaIngreso(string FechaInicial,string FechaFinal)
        {
            DataTable DtMostrar = new DataTable("Ingreso");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Ingreso_Buscar_Fecha";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parFechaInicial = new SqlParameter();
                parFechaInicial.ParameterName = "@fechaInicial";
                parFechaInicial.SqlDbType = SqlDbType.VarChar;
                parFechaInicial.Size = 20;
                parFechaInicial.Value = FechaInicial;
                sqlcmd.Parameters.Add(parFechaInicial);

                SqlParameter parFechaFinal = new SqlParameter();
                parFechaFinal.ParameterName = "@@fechaFinal";
                parFechaFinal.SqlDbType = SqlDbType.VarChar;
                parFechaFinal.Size = 20;
                parFechaFinal.Value = FechaFinal;
                sqlcmd.Parameters.Add(parFechaFinal);

                SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(DtMostrar);
            }
            catch (Exception ex)
            {
                DtMostrar = null;
            }
            return DtMostrar;
        }

        public DataTable MostrarDetalle(string textoBuscar)
        {
            DataTable DtMostrar = new DataTable("Detalle_Ingreso");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "DetalleIngreso_Mostrar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textoBuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = textoBuscar;
                sqlcmd.Parameters.Add(parTextoBuscar);

               SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(DtMostrar);
            }
            catch (Exception ex)
            {
                DtMostrar = null;
            }
            return DtMostrar;
        }

    }


}
