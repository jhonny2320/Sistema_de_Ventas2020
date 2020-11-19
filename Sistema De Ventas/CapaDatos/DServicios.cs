using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DServicios
    {
        private int _Ser_Id;
        private string _Ser_Servicio;
        private decimal _Ser_Costo;
        private string _TextoBuscar;

        public int Ser_Id
        {
            get
            {
                return _Ser_Id;
            }

            set
            {
                _Ser_Id = value;
            }
        }
        public string Ser_Servicio
        {
            get
            {
                return _Ser_Servicio;
            }

            set
            {
                _Ser_Servicio = value;
            }
        }
        public decimal Ser_Costo
        {
            get
            {
                return _Ser_Costo;
            }

            set
            {
                _Ser_Costo = value;
            }
        }
        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }

        public DServicios()
        {

        }

        public DServicios(int Ser_id, string Ser_sevicio, decimal Ser_costo, string textoBuscar)
        {
            this.Ser_Id = Ser_id;
            this.Ser_Servicio = Ser_sevicio;
            this.Ser_Costo = Ser_costo;
            this.TextoBuscar = textoBuscar;
        }


        public string Insertar(DServicios Servicio)
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
                sqlcmd.CommandText = "Servicio_Insertar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parSer_id = new SqlParameter();
                parSer_id.ParameterName = "@idServicio";
                parSer_id.SqlDbType = SqlDbType.Int;
                parSer_id.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parSer_id);


                SqlParameter parSer_servicio = new SqlParameter();
                parSer_servicio.ParameterName = "@servicio";
                parSer_servicio.SqlDbType = SqlDbType.VarChar;
                parSer_servicio.Size = 50;
                parSer_servicio.Value = Servicio.Ser_Servicio;
                sqlcmd.Parameters.Add(parSer_servicio);

                SqlParameter parSer_costo = new SqlParameter();
                parSer_costo.ParameterName = "@ser_costo";
                parSer_costo.SqlDbType = SqlDbType.Money;
                parSer_costo.Value = Servicio.Ser_Costo;
                sqlcmd.Parameters.Add(parSer_costo);


                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE REALIZO EL REGISTRO";

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

        public string Editar(DServicios Servicio)
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
                sqlcmd.CommandText = "Servicio_Editar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parSer_id = new SqlParameter();
                parSer_id.ParameterName = "@idservicio";
                parSer_id.SqlDbType = SqlDbType.Int;
                parSer_id.Value = Servicio.Ser_Id;
                sqlcmd.Parameters.Add(parSer_id);


                SqlParameter parSer_servicio = new SqlParameter();
                parSer_servicio.ParameterName = "@servicio";
                parSer_servicio.SqlDbType = SqlDbType.VarChar;
                parSer_servicio.Size = 50;
                parSer_servicio.Value = Servicio.Ser_Servicio;
                sqlcmd.Parameters.Add(parSer_servicio);

                SqlParameter parSer_costo = new SqlParameter();
                parSer_costo.ParameterName = "@ser_costo";
                parSer_costo.SqlDbType = SqlDbType.Money;
                parSer_costo.Value = Servicio.Ser_Costo;
                sqlcmd.Parameters.Add(parSer_costo);


                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZO EL REGISTRO";

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

        public string Eliminar(DServicios Servicio)
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
                sqlcmd.CommandText = "Servicio_eliminar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parSer_id = new SqlParameter();
                parSer_id.ParameterName = "@idservicio";
                parSer_id.SqlDbType = SqlDbType.Int;
                parSer_id.Value = Servicio.Ser_Id;
                sqlcmd.Parameters.Add(parSer_id);


                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ELIMINO EL REGISTRO";

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
            DataTable DtMostrar = new DataTable("servicios");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Servicio_Mostrar";
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

        public DataTable BuscarServicio(DServicios Servicio)
        {
            DataTable DtMostrar = new DataTable("servicios");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Servicio_Buscar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@BuscarServicio";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 20;
                parTextoBuscar.Value = Servicio.TextoBuscar;
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