using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DArticulo
    {
        private int _Art_Id;
        private string _Art_Codigo;
        private string _Art_Nombre;
        private string _Art_Descripcion;
        private int _Art_IdCategoria;
        private string _TextoBuscar;


        public int Art_Id
        {
            get
            {
                return _Art_Id;
            }

            set
            {
                _Art_Id = value;
            }
        }
        public string Art_Codigo
        {
            get
            {
                return _Art_Codigo;
            }

            set
            {
                _Art_Codigo = value;
            }
        }
        public string Art_Nombre
        {
            get
            {
                return _Art_Nombre;
            }

            set
            {
                _Art_Nombre = value;
            }
        }
        public int Art_IdCategoria
        {
            get
            {
                return _Art_IdCategoria;
            }

            set
            {
                _Art_IdCategoria = value;
            }
        }
        public string Art_Descripcion
        {
            get
            {
                return _Art_Descripcion;
            }

            set
            {
                _Art_Descripcion = value;
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

        

        public DArticulo()
        {

        }

        public DArticulo(int Art_id,string Art_codigo, string Art_nombre, string Art_descripcion, int Art_idcategoria, string textoBuscar)
        {
            this.Art_Id = Art_id;
            this.Art_Codigo = Art_codigo;
            this.Art_Nombre = Art_nombre;
            this.Art_Descripcion = Art_descripcion;
            this.Art_IdCategoria = Art_idcategoria;
            this.TextoBuscar = textoBuscar;
        }


        public string Insertar(DArticulo Articulo)
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
                sqlcmd.CommandText = "Articulo_Insertar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parCat_id = new SqlParameter();
                parCat_id.ParameterName = "@Art_id";
                parCat_id.SqlDbType = SqlDbType.Int;
                parCat_id.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parCat_id);

                SqlParameter parArt_codigo = new SqlParameter();
                parArt_codigo.ParameterName = "@Art_codigo";
                parArt_codigo.SqlDbType = SqlDbType.VarChar;
                parArt_codigo.Size = 50;
                parArt_codigo.Value = Articulo.Art_Codigo;
                sqlcmd.Parameters.Add(parArt_codigo);


                SqlParameter parArt_nombre = new SqlParameter();
                parArt_nombre.ParameterName = "@Art_nombre";
                parArt_nombre.SqlDbType = SqlDbType.VarChar;
                parArt_nombre.Size = 50;
                parArt_nombre.Value = Articulo.Art_Nombre;
                sqlcmd.Parameters.Add(parArt_nombre);


                SqlParameter parArt_descripcion = new SqlParameter();
                parArt_descripcion.ParameterName = "@Art_descripcion";
                parArt_descripcion.SqlDbType = SqlDbType.VarChar;
                parArt_descripcion.Size = 200;
                parArt_descripcion.Value = Articulo.Art_Descripcion;
                sqlcmd.Parameters.Add(parArt_descripcion);


                SqlParameter parArt_idCategoria = new SqlParameter();
                parArt_idCategoria.ParameterName = "@Art_idCategoria";
                parArt_idCategoria.SqlDbType = SqlDbType.VarChar;
                parArt_idCategoria.Value = Articulo.Art_IdCategoria;
                sqlcmd.Parameters.Add(parArt_idCategoria);

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


        public string Editar(DArticulo Articulo)
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
                sqlcmd.CommandText = "Articulo_Editar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parArt_id = new SqlParameter();
                parArt_id.ParameterName = "@Art_id";
                parArt_id.SqlDbType = SqlDbType.Int;
                parArt_id.Value = Articulo.Art_Id;
                sqlcmd.Parameters.Add(parArt_id);


                SqlParameter parArt_codigo = new SqlParameter();
                parArt_codigo.ParameterName = "@Art_codigo";
                parArt_codigo.SqlDbType = SqlDbType.VarChar;
                parArt_codigo.Size = 50;
                parArt_codigo.Value = Articulo.Art_Codigo;
                sqlcmd.Parameters.Add(parArt_codigo);


                SqlParameter parArt_nombre = new SqlParameter();
                parArt_nombre.ParameterName = "@Art_nombre";
                parArt_nombre.SqlDbType = SqlDbType.VarChar;
                parArt_nombre.Size = 50;
                parArt_nombre.Value = Articulo.Art_Nombre;
                sqlcmd.Parameters.Add(parArt_nombre);


                SqlParameter parArt_descripcion = new SqlParameter();
                parArt_descripcion.ParameterName = "@Art_descripcion";
                parArt_descripcion.SqlDbType = SqlDbType.VarChar;
                parArt_descripcion.Size = 200;
                parArt_descripcion.Value = Articulo.Art_Descripcion;
                sqlcmd.Parameters.Add(parArt_descripcion);


                SqlParameter parArt_idCategoria = new SqlParameter();
                parArt_idCategoria.ParameterName = "@Art_idCategoria";
                parArt_idCategoria.SqlDbType = SqlDbType.VarChar;
                parArt_idCategoria.Value = Articulo.Art_IdCategoria;
                sqlcmd.Parameters.Add(parArt_idCategoria);

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

        public string Eliminar (DArticulo Articulo)
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
                sqlcmd.CommandText = "Articulo_Eliminar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parArt_id = new SqlParameter();
                parArt_id.ParameterName = "@Art_id";
                parArt_id.SqlDbType = SqlDbType.Int;
                parArt_id.Value = Articulo.Art_Id;
                sqlcmd.Parameters.Add(parArt_id);

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
            DataTable DtMostrar = new DataTable("Articulos");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Articulo_Mostrar";
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

        public DataTable BuscarArticulo(DArticulo Articulo)
        {
            DataTable DtMostrar = new DataTable("Articulos");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Articulo_Buscar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textoBuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 20;
                parTextoBuscar.Value = Articulo.TextoBuscar;
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
