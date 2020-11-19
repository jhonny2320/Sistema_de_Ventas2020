using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DTipoDocumento
    {
        private int _TD_Id;
        private string _TD_Nombre;

        public int TD_Id
        {
            get
            {
                return _TD_Id;
            }

            set
            {
                _TD_Id = value;
            }
        }
        public string TD_Nombre
        {
            get
            {
                return _TD_Nombre;
            }

            set
            {
                _TD_Nombre = value;
            }
        }

        public DTipoDocumento()
        {

        }
        public DTipoDocumento(int TD_id, string TD_nombre)
        {
            this.TD_Id = TD_id;
            this.TD_Nombre = TD_nombre;

        }

        public string Insertar(DTipoDocumento TipoDocumento)
        {
            string rpta = "";
            SqlConnection SqlConexion = new SqlConnection();
            try
            {
                //codigo
                SqlConexion.ConnectionString = Conexion.Conectar;
                SqlConexion.Open();
                //establecer el comando ejecutar sentencias
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = SqlConexion;
                sqlCmd.CommandText = "TipoDocumento_Agregar";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //parametro de conexion
                SqlParameter parTD_id = new SqlParameter();
                parTD_id.ParameterName = "@TD_id";
                parTD_id.SqlDbType = SqlDbType.Int;
                parTD_id.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(parTD_id);

                //segundo parametro conexion
                SqlParameter parTD_nombre = new SqlParameter();
                parTD_nombre.ParameterName = "@TD_nombre";
                parTD_nombre.SqlDbType = SqlDbType.VarChar;
                parTD_nombre.Size = 50;
                parTD_nombre.Value = TipoDocumento.TD_Nombre; ;
                sqlCmd.Parameters.Add(parTD_nombre);

                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                    SqlConexion.Dispose();
                }
            }
            return rpta;
        }
        public string Editar(DTipoDocumento TipoDocumento)
        {
            string rpta = "";
            SqlConnection SqlConexion = new SqlConnection();
            try
            {
                //codigo
                SqlConexion.ConnectionString = Conexion.Conectar;
                SqlConexion.Open();
                //establecer el comando ejecutar sentencias
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = SqlConexion;
                sqlCmd.CommandText = "TipoDocumento_Editar";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //parametro de conexion
                SqlParameter parTD_id = new SqlParameter();
                parTD_id.ParameterName = "@TD_id";
                parTD_id.SqlDbType = SqlDbType.Int;
                parTD_id.Value = TipoDocumento.TD_Id;
                sqlCmd.Parameters.Add(parTD_id);
                //segundo parametro conexion
                SqlParameter parTD_nombre = new SqlParameter();
                parTD_nombre.ParameterName = "@TD_nombre";
                parTD_nombre.SqlDbType = SqlDbType.VarChar;
                parTD_nombre.Size = 50;
                parTD_nombre.Value = TipoDocumento.TD_Nombre; ;
                sqlCmd.Parameters.Add(parTD_nombre);

                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZO EL REGISTRO";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                    SqlConexion.Dispose();
                }
            }
            return rpta;
        }

        public string Eliminar(DTipoDocumento TipoDocumento)
        {
            string rpta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                //codigo
                sqlcon.ConnectionString = Conexion.Conectar;
                sqlcon.Open();
                //establecer el comando ejecutar sentencias
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlcon;
                sqlCmd.CommandText = "TipoDocumento_Eliminarr";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //parametro de conexion
                SqlParameter parTD_id = new SqlParameter();
                parTD_id.ParameterName = "@TD_id";
                parTD_id.SqlDbType = SqlDbType.Int;
                parTD_id.Value = TipoDocumento.TD_Id;
                sqlCmd.Parameters.Add(parTD_id);

                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se pudo Eliminar Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            return rpta;
        }


        public DataTable Mostrar()
        {
            DataTable DtMostrar = new DataTable("Tipo de Documento");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Conectar;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlcon;
                sqlCmd.CommandText = "TipoDocumento_Mostrar";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlData = new SqlDataAdapter(sqlCmd);
                sqlData.Fill(DtMostrar);


            }
            catch (Exception ex)
            {
                DtMostrar = null;
            }

            return DtMostrar;

        }
    }
}
