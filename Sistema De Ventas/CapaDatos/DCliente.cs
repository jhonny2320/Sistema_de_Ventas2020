using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DCliente
    {
        //variable
        private int _Cli_Id;
        private string _Cli_Nombre;
        private string _Cli_Apellido;
        private int _Cli_idTipoDocumento;
        private string _Cli_Documento;
        private string _Cli_Telefono;
        private string _Cli_Direccion;
        private string _TextoBuscar;

        //metodos set and get
        public int Cli_Id
        {
            get  { return _Cli_Id;}
            set {_Cli_Id = value;}
        }
        public string Cli_Nombre
        {
            get
            {
                return _Cli_Nombre;
            }

            set
            {
                _Cli_Nombre = value;
            }
        }
        public string Cli_Apellido
        {
            get
            {
                return _Cli_Apellido;
            }

            set
            {
                _Cli_Apellido = value;
            }
        }
        public int Cli_idTipoDocumento
        {
            get
            {
                return _Cli_idTipoDocumento;
            }

            set
            {
                _Cli_idTipoDocumento = value;
            }
        }
        public string Cli_Documento
        {
            get
            {
                return _Cli_Documento;
            }

            set
            {
                _Cli_Documento = value;
            }
        }
        public string Cli_Telefono
        {
            get
            {
                return _Cli_Telefono;
            }

            set
            {
                _Cli_Telefono = value;
            }
        }
        public string Cli_Direccion
        {
            get
            {
                return _Cli_Direccion;
            }

            set
            {
                _Cli_Direccion = value;
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


        //constructores
        public DCliente()
        {

        }
        public DCliente(int Cli_id, string Cli_nombre, string Cli_apellido, int Cli_idTipoDocumento, string Cli_documneto, string Cli_telefono, string Cli_direccion, string textoBuscar)
        {
            this.Cli_Id = Cli_id;
            this.Cli_Nombre = Cli_nombre;
            this.Cli_Apellido = Cli_apellido;
            this.Cli_idTipoDocumento = Cli_idTipoDocumento;
            this.Cli_Documento = Cli_documneto;
            this.Cli_Telefono = Cli_telefono;
            this.Cli_Direccion = Cli_direccion;
            this.TextoBuscar = textoBuscar;
        }

        public string Insertar(DCliente Cliente)
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
                sqlCmd.CommandText = "Cliente_Agregar";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //parametro de conexion
                SqlParameter parCli_id = new SqlParameter();
                parCli_id.ParameterName = "@Cli_id";
                parCli_id.SqlDbType = SqlDbType.Int;
                parCli_id.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(parCli_id);

                //segundo parametro conexion
                SqlParameter parCli_nombre = new SqlParameter();
                parCli_nombre.ParameterName = "@Cli_nombre";
                parCli_nombre.SqlDbType = SqlDbType.VarChar;
                parCli_nombre.Size = 50;
                parCli_nombre.Value = Cliente.Cli_Nombre; ;
                sqlCmd.Parameters.Add(parCli_nombre);

                //tercer parametro de conexion
                SqlParameter parCli_apellido = new SqlParameter();
                parCli_apellido.ParameterName = "@Cli_apellido";
                parCli_apellido.SqlDbType = SqlDbType.VarChar;
                parCli_apellido.Size = 50;
                parCli_apellido.Value = Cliente.Cli_Apellido;
                sqlCmd.Parameters.Add(parCli_apellido);

                //cuarto parametro de conexion
                SqlParameter parCli_idTipoDocumento = new SqlParameter();
                parCli_idTipoDocumento.ParameterName = "@Cli_idTIpoDocumento";
                parCli_idTipoDocumento.SqlDbType = SqlDbType.VarChar;
                parCli_idTipoDocumento.Value = Cliente.Cli_idTipoDocumento;
                sqlCmd.Parameters.Add(parCli_idTipoDocumento);

                //conexion documento
                SqlParameter parCli_documento = new SqlParameter();
                parCli_documento.ParameterName = "@Cli_documento";
                parCli_documento.SqlDbType = SqlDbType.VarChar;
                parCli_documento.Size = 20;
                parCli_documento.Value = Cliente.Cli_Documento;
                sqlCmd.Parameters.Add(parCli_documento);

                //conexion direccion 
                SqlParameter parCli_direccion = new SqlParameter();
                parCli_direccion.ParameterName = "@Cli_direccion";
                parCli_direccion.SqlDbType = SqlDbType.VarChar;
                parCli_direccion.Size = 50;
                parCli_direccion.Value = Cliente.Cli_Direccion;
                sqlCmd.Parameters.Add(parCli_direccion);

                //conexxion telefono
                SqlParameter parCli_telefono = new SqlParameter();
                parCli_telefono.ParameterName = "@Cli_telefono";
                parCli_telefono.SqlDbType = SqlDbType.VarChar;
                parCli_telefono.Size = 20;
                parCli_telefono.Value = Cliente.Cli_Telefono;
                sqlCmd.Parameters.Add(parCli_telefono);

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


        public string Editar(DCliente Cliente)
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
                sqlCmd.CommandText = "Cliente_Editar";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //parametro de conexion
                SqlParameter parCli_id = new SqlParameter();
                parCli_id.ParameterName = "@Cli_id";
                parCli_id.SqlDbType = SqlDbType.Int;
                parCli_id.Value = Cliente.Cli_Id;
                sqlCmd.Parameters.Add(parCli_id);
                //segundo parametro conexion
                SqlParameter parCli_nombre = new SqlParameter();
                parCli_nombre.ParameterName = "@Cli_nombre";
                parCli_nombre.SqlDbType = SqlDbType.VarChar;
                parCli_nombre.Size = 50;
                parCli_nombre.Value = Cliente.Cli_Nombre; ;
                sqlCmd.Parameters.Add(parCli_nombre);
                //tercer parametro de conexion
                SqlParameter parCli_apellido = new SqlParameter();
                parCli_apellido.ParameterName = "@Cli_apellido";
                parCli_apellido.SqlDbType = SqlDbType.VarChar;
                parCli_apellido.Size = 50;
                parCli_apellido.Value = Cliente.Cli_Apellido;
                sqlCmd.Parameters.Add(parCli_apellido);
                //cuarto parametro de conexion
                SqlParameter parCli_idTipoDocumento = new SqlParameter();
                parCli_idTipoDocumento.ParameterName = "@Cli_idTIpoDocumento";
                parCli_idTipoDocumento.SqlDbType = SqlDbType.VarChar;
                parCli_idTipoDocumento.Value = Cliente.Cli_idTipoDocumento;
                sqlCmd.Parameters.Add(parCli_idTipoDocumento);
                //conexion documento
                SqlParameter parCli_documento = new SqlParameter();
                parCli_documento.ParameterName = "@Cli_documento";
                parCli_documento.SqlDbType = SqlDbType.VarChar;
                parCli_documento.Size = 20;
                parCli_documento.Value = Cliente.Cli_Documento;
                sqlCmd.Parameters.Add(parCli_documento);
                //conexion direccion 
                SqlParameter parCli_direccion = new SqlParameter();
                parCli_direccion.ParameterName = "@Cli_direccion";
                parCli_direccion.SqlDbType = SqlDbType.VarChar;
                parCli_direccion.Size = 50;
                parCli_direccion.Value = Cliente.Cli_Direccion;
                sqlCmd.Parameters.Add(parCli_direccion);
                //conexxion telefono
                SqlParameter parCli_telefono = new SqlParameter();
                parCli_telefono.ParameterName = "@Cli_telefono";
                parCli_telefono.SqlDbType = SqlDbType.VarChar;
                parCli_telefono.Size = 20;
                parCli_telefono.Value = Cliente.Cli_Telefono;
                sqlCmd.Parameters.Add(parCli_telefono);

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


        public string Eliminar(DCliente Cliente)
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
                sqlCmd.CommandText = "Cliente_Eliminar";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //parametro de conexion
                SqlParameter parCli_id = new SqlParameter();
                parCli_id.ParameterName = "@Cli_id";
                parCli_id.SqlDbType = SqlDbType.Int;
                parCli_id.Value = Cliente.Cli_Id;
                sqlCmd.Parameters.Add(parCli_id);

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
            DataTable DtMostrar = new DataTable("Cliente");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Conectar;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlcon;
                sqlCmd.CommandText = "Clientes_Mostrar";
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


        public DataTable BuscarCliente(DCliente Cliente)

        {
            
            DataTable DtMostrar = new DataTable("Cliente");
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon.ConnectionString = Conexion.Conectar;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlcon;
                sqlCmd.CommandText = "Cliente_Buscar";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@Buscar_Cliente";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 20;
                parTextoBuscar.Value = Cliente.TextoBuscar;
                sqlCmd.Parameters.Add(parTextoBuscar);

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
