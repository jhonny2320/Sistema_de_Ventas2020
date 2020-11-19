using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DCategoria
    {
        private int _Cat_id;
        private string _Cat_Nombre;
        private string _Cat_Descripcion;
        private string _TextoBuscar;

        public int Cat_id
        {
            get
            {
                return _Cat_id;
            }

            set
            {
                _Cat_id = value;
            }
        }
        public string Cat_Nombre
        {
            get
            {
                return _Cat_Nombre;
            }

            set
            {
                _Cat_Nombre = value;
            }
        }
        public string Cat_Descripcion
        {
            get
            {
                return _Cat_Descripcion;
            }

            set
            {
                _Cat_Descripcion = value;
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

        public DCategoria()
        {

        }

        public DCategoria(int Cat_id, string Cat_nombre, string Cat_descripcion, string textoBuscar)
        {
            this.Cat_id = Cat_id;
            this.Cat_Nombre = Cat_nombre;
            this.Cat_Descripcion = Cat_descripcion;
            this.TextoBuscar = textoBuscar;
        }

        public string Insertar(DCategoria Categoria)
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
                sqlcmd.CommandText = "Categoria_Agregar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parCat_id = new SqlParameter();
                parCat_id.ParameterName = "@Cat_id";
                parCat_id.SqlDbType = SqlDbType.Int;
                parCat_id.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parCat_id);

                //conexion nombre

                SqlParameter parCat_nombre = new SqlParameter();
                parCat_nombre.ParameterName = "@Cat_nombre";
                parCat_nombre.SqlDbType = SqlDbType.VarChar;
                parCat_nombre.Size = 50;
                parCat_nombre.Value = Categoria.Cat_Nombre;
                sqlcmd.Parameters.Add(parCat_nombre);

                //conexion apellido
                SqlParameter parCat_descripcion = new SqlParameter();
                parCat_descripcion.ParameterName = "@Cat_descripcion";
                parCat_descripcion.SqlDbType = SqlDbType.VarChar;
                parCat_descripcion.Size = 200;
                parCat_descripcion.Value = Categoria.Cat_Descripcion;
                sqlcmd.Parameters.Add(parCat_descripcion);

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

        public string Editar(DCategoria Categoria)
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
                sqlcmd.CommandText = "Categoria_Editar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parCat_id = new SqlParameter();
                parCat_id.ParameterName = "@Cat_id";
                parCat_id.SqlDbType = SqlDbType.Int;
                parCat_id.Value = Categoria.Cat_id;
                sqlcmd.Parameters.Add(parCat_id);

                //conexion nombre

                SqlParameter parCat_nombre = new SqlParameter();
                parCat_nombre.ParameterName = "@Cat_nombre";
                parCat_nombre.SqlDbType = SqlDbType.VarChar;
                parCat_nombre.Size = 50;
                parCat_nombre.Value = Categoria.Cat_Nombre;
                sqlcmd.Parameters.Add(parCat_nombre);

                //conexion apellido
                SqlParameter parCat_descripcion = new SqlParameter();
                parCat_descripcion.ParameterName = "@Cat_descripcion";
                parCat_descripcion.SqlDbType = SqlDbType.VarChar;
                parCat_descripcion.Size = 200;
                parCat_descripcion.Value = Categoria.Cat_Descripcion;
                sqlcmd.Parameters.Add(parCat_descripcion);

                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACRUALIZO EL REGISTRO";

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

        public string Eliminar(DCategoria Categoria)
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
                sqlcmd.CommandText = "Categoria_Eliminar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parCat_id = new SqlParameter();
                parCat_id.ParameterName = "@Cat_id";
                parCat_id.SqlDbType = SqlDbType.Int;
                parCat_id.Value = Categoria.Cat_id;
                sqlcmd.Parameters.Add(parCat_id);

                //conexion nombre

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
            DataTable DtMostrar = new DataTable("Categoria");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Categoria_Mostrar";
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

        public DataTable BuscarCategotia(DCategoria Categoria)
        {
            DataTable DtMostrar = new DataTable("Categoria");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Categoria_Buscar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textoBuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 20;
                parTextoBuscar.Value = Categoria.TextoBuscar;
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
