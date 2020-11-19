using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DEmpleados
    {
        //variables
        private int _Emp_Id;
        private string _Emp_Nombre;
        private string _Emp_Apellido;
        private int _Emp_idTipoDocumento;
        private string _Emp_Documento;
        private string _Emp_Telefono;
        private string _Emp_Direccion;
        private string _Emp_Acceso;
        private string _Emp_Usuario;
        private string _Emp_Contraseña;
        private string _TextoBuscar;


        //metodos set and get
        public int Emp_Id
        {
            get
            {
                return _Emp_Id;
            }

            set
            {
                _Emp_Id = value;
            }
        }
        public string Emp_Nombre
        {
            get
            {
                return _Emp_Nombre;
            }

            set
            {
                _Emp_Nombre = value;
            }
        }
        public string Emp_Apellido
        {
            get
            {
                return _Emp_Apellido;
            }

            set
            {
                _Emp_Apellido = value;
            }
        }
        public int Emp_idTipoDocumento
        {
            get
            {
                return _Emp_idTipoDocumento;
            }

            set
            {
                _Emp_idTipoDocumento = value;
            }
        }
        public string Emp_Documento
        {
            get
            {
                return _Emp_Documento;
            }

            set
            {
                _Emp_Documento = value;
            }
        }
        public string Emp_Telefono
        {
            get
            {
                return _Emp_Telefono;
            }

            set
            {
                _Emp_Telefono = value;
            }
        }
        public string Emp_Direccion
        {
            get
            {
                return _Emp_Direccion;
            }

            set
            {
                _Emp_Direccion = value;
            }
        }
        public string Emp_Acceso
        {
            get
            {
                return _Emp_Acceso;
            }

            set
            {
                _Emp_Acceso = value;
            }
        }
        public string Emp_Usuario
        {
            get
            {
                return _Emp_Usuario;
            }

            set
            {
                _Emp_Usuario = value;
            }
        }
        public string Emp_Contraseña
        {
            get
            {
                return _Emp_Contraseña;
            }

            set
            {
                _Emp_Contraseña = value;
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
        public DEmpleados()
        {

        }

        public DEmpleados(int Emp_id, string Emp_nombre, string Emp_apellido, int Emp_idTipoDocumento,
            string Emp_documento, string Emp_telefono, string Emp_direccion, string Emp_acceso, string Emp_usuario, string Emp_contraseña, string textoBuscar)

        {
            this.Emp_Id = Emp_id;
            this.Emp_Nombre = Emp_nombre;
            this.Emp_Apellido = Emp_apellido;
            this.Emp_idTipoDocumento = Emp_idTipoDocumento;
            this.Emp_Documento = Emp_documento;
            this.Emp_Telefono = Emp_telefono;
            this.Emp_Direccion = Emp_direccion;
            this.Emp_Acceso = Emp_acceso;
            this.Emp_Usuario = Emp_usuario;
            this.Emp_Contraseña = Emp_contraseña;
            this.TextoBuscar = textoBuscar;
        }

        public string Insertar(DEmpleados Empleado)
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
                sqlcmd.CommandText = "Empleado_Agregar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parEmp_id = new SqlParameter();
                parEmp_id.ParameterName = "@Emp_id";
                parEmp_id.SqlDbType = SqlDbType.Int;
                parEmp_id.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parEmp_id);

                //conexion nombre

                SqlParameter parEmp_nombre = new SqlParameter();
                parEmp_nombre.ParameterName = "@Emp_nombre";
                parEmp_nombre.SqlDbType = SqlDbType.VarChar;
                parEmp_nombre.Size = 50;
                parEmp_nombre.Value = Empleado.Emp_Nombre;
                sqlcmd.Parameters.Add(parEmp_nombre);

                //conexion apellido
                SqlParameter parEmp_apellido = new SqlParameter();
                parEmp_apellido.ParameterName = "@Emp_apellido";
                parEmp_apellido.SqlDbType = SqlDbType.VarChar;
                parEmp_apellido.Size = 50;
                parEmp_apellido.Value = Empleado.Emp_Apellido;
                sqlcmd.Parameters.Add(parEmp_apellido);

                //conexion tipo de documento
                SqlParameter parEmp_idTipoDocumento = new SqlParameter();
                parEmp_idTipoDocumento.ParameterName = "@Emp_idTipoDocumento";
                parEmp_idTipoDocumento.SqlDbType = SqlDbType.VarChar;
                parEmp_idTipoDocumento.Value = Empleado.Emp_idTipoDocumento;
                sqlcmd.Parameters.Add(parEmp_idTipoDocumento);

                //conexion documento
                SqlParameter parEmp_documento = new SqlParameter();
                parEmp_documento.ParameterName = "@Emp_documento";
                parEmp_documento.SqlDbType = SqlDbType.VarChar;
                parEmp_documento.Size = 20;
                parEmp_documento.Value = Empleado.Emp_Documento;
                sqlcmd.Parameters.Add(parEmp_documento);

                //conexion direccion
                SqlParameter parEmp_direccion = new SqlParameter();
                parEmp_direccion.ParameterName = "@Emp_direccion";
                parEmp_direccion.SqlDbType = SqlDbType.VarChar;
                parEmp_direccion.Size = 50;
                parEmp_direccion.Value = Empleado.Emp_Direccion;
                sqlcmd.Parameters.Add(parEmp_direccion);

                //conexion telefono
                SqlParameter parEmp_telefono = new SqlParameter();
                parEmp_telefono.ParameterName = "@Emp_telefono";
                parEmp_telefono.SqlDbType = SqlDbType.VarChar;
                parEmp_telefono.Size = 20;
                parEmp_telefono.Value = Empleado.Emp_Telefono;
                sqlcmd.Parameters.Add(parEmp_telefono);

                //conexion acceso
                SqlParameter parEmp_acceso = new SqlParameter();
                parEmp_acceso.ParameterName = "@Emp_acceso";
                parEmp_acceso.SqlDbType = SqlDbType.VarChar;
                parEmp_acceso.Size = 20;
                parEmp_acceso.Value = Empleado.Emp_Acceso;
                sqlcmd.Parameters.Add(parEmp_acceso);

                //conexion usuario
                SqlParameter parEmp_usuario = new SqlParameter();
                parEmp_usuario.ParameterName = "@Emp_usuario";
                parEmp_usuario.SqlDbType = SqlDbType.VarChar;
                parEmp_usuario.Size = 20;
                parEmp_usuario.Value = Empleado.Emp_Usuario;
                sqlcmd.Parameters.Add(parEmp_usuario);

                //conexioncontraseña
                SqlParameter parEmp_contraseña = new SqlParameter();
                parEmp_contraseña.ParameterName = "@Emp_contraseña";
                parEmp_contraseña.SqlDbType = SqlDbType.VarChar;
                parEmp_contraseña.Size = 20;
                parEmp_contraseña.Value = Empleado.Emp_Contraseña;
                sqlcmd.Parameters.Add(parEmp_contraseña);

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

        public string Editar(DEmpleados Empleado)
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
                sqlcmd.CommandText = "Empleado_Editar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parEmp_id = new SqlParameter();
                parEmp_id.ParameterName = "@Emp_id";
                parEmp_id.SqlDbType = SqlDbType.Int;
                parEmp_id.Value = Empleado.Emp_Id;
                sqlcmd.Parameters.Add(parEmp_id);

                //conexion nombre

                SqlParameter parEmp_nombre = new SqlParameter();
                parEmp_nombre.ParameterName = "@Emp_nombre";
                parEmp_nombre.SqlDbType = SqlDbType.VarChar;
                parEmp_nombre.Size = 50;
                parEmp_nombre.Value = Empleado.Emp_Nombre;
                sqlcmd.Parameters.Add(parEmp_nombre);

                //conexion apellido
                SqlParameter parEmp_apellido = new SqlParameter();
                parEmp_apellido.ParameterName = "@Emp_apellido";
                parEmp_apellido.SqlDbType = SqlDbType.VarChar;
                parEmp_apellido.Size = 50;
                parEmp_apellido.Value = Empleado.Emp_Apellido;
                sqlcmd.Parameters.Add(parEmp_apellido);

                //conexion tipo de documento
                SqlParameter parEmp_idTipoDocumento = new SqlParameter();
                parEmp_idTipoDocumento.ParameterName = "@Emp_idTipoDocumento";
                parEmp_idTipoDocumento.SqlDbType = SqlDbType.VarChar;
                parEmp_idTipoDocumento.Value = Empleado.Emp_idTipoDocumento;
                sqlcmd.Parameters.Add(parEmp_idTipoDocumento);

                //conexion documento
                SqlParameter parEmp_documento = new SqlParameter();
                parEmp_documento.ParameterName = "@Emp_documento";
                parEmp_documento.SqlDbType = SqlDbType.VarChar;
                parEmp_documento.Size = 20;
                parEmp_documento.Value = Empleado.Emp_Documento;
                sqlcmd.Parameters.Add(parEmp_documento);

                //conexion direccion
                SqlParameter parEmp_direccion = new SqlParameter();
                parEmp_direccion.ParameterName = "@Emp_direccion";
                parEmp_direccion.SqlDbType = SqlDbType.VarChar;
                parEmp_direccion.Size = 50;
                parEmp_direccion.Value = Empleado.Emp_Direccion;
                sqlcmd.Parameters.Add(parEmp_direccion);

                //conexion telefono
                SqlParameter parEmp_telefono = new SqlParameter();
                parEmp_telefono.ParameterName = "@Emp_telefono";
                parEmp_telefono.SqlDbType = SqlDbType.VarChar;
                parEmp_telefono.Size = 20;
                parEmp_telefono.Value = Empleado.Emp_Telefono;
                sqlcmd.Parameters.Add(parEmp_telefono);

                //conexion acceso
                SqlParameter parEmp_acceso = new SqlParameter();
                parEmp_acceso.ParameterName = "@Emp_acceso";
                parEmp_acceso.SqlDbType = SqlDbType.VarChar;
                parEmp_acceso.Size = 20;
                parEmp_acceso.Value = Empleado.Emp_Acceso;
                sqlcmd.Parameters.Add(parEmp_acceso);

                //conexion usuario
                SqlParameter parEmp_usuario = new SqlParameter();
                parEmp_usuario.ParameterName = "@Emp_usuario";
                parEmp_usuario.SqlDbType = SqlDbType.VarChar;
                parEmp_usuario.Size = 20;
                parEmp_usuario.Value = Empleado.Emp_Usuario;
                sqlcmd.Parameters.Add(parEmp_usuario);

                //conexioncontraseña
                SqlParameter parEmp_contraseña = new SqlParameter();
                parEmp_contraseña.ParameterName = "@Emp_contraseña";
                parEmp_contraseña.SqlDbType = SqlDbType.VarChar;
                parEmp_contraseña.Size = 20;
                parEmp_contraseña.Value = Empleado.Emp_Contraseña;
                sqlcmd.Parameters.Add(parEmp_contraseña);

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

        public string Eliminar(DEmpleados Empleado)
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
                sqlcmd.CommandText = "Empleado_Eliminar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //conexion con variables del SP
                SqlParameter parEmp_id = new SqlParameter();
                parEmp_id.ParameterName = "@Emp_id";
                parEmp_id.SqlDbType = SqlDbType.Int;
                parEmp_id.Value = Empleado.Emp_Id;
                sqlcmd.Parameters.Add(parEmp_id);

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
            DataTable DtMostrar = new DataTable("Empleados");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Empleados_Mostrar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqldata = new SqlDataAdapter(sqlcmd);
                sqldata.Fill(DtMostrar);
            }
            catch(Exception ex)
            {
                DtMostrar = null;
            }
            return DtMostrar;
        }

        public DataTable BuscarEmpleado(DEmpleados Empleado)
        {
            DataTable DtMostrar = new DataTable("Empleados");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Empleado_Buscar";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@BuscarEmpleado";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 20;
                parTextoBuscar.Value = Empleado.TextoBuscar;
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


        public DataTable Login(DEmpleados Empleado)
        {
            DataTable DtMostrar = new DataTable("Empleados");
            SqlConnection sqlconexion = new SqlConnection();
            try
            {
                sqlconexion.ConnectionString = Conexion.Conectar;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlconexion;
                sqlcmd.CommandText = "Login_Empleado";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parUsuario = new SqlParameter();
                parUsuario.ParameterName = "@usuario";
                parUsuario.SqlDbType = SqlDbType.VarChar;
                parUsuario.Size = 50;
                parUsuario.Value = Empleado.Emp_Usuario;
                sqlcmd.Parameters.Add(parUsuario);
                
                SqlParameter parContraseña = new SqlParameter();
                parContraseña.ParameterName = "@contraseña";
                parContraseña.SqlDbType = SqlDbType.VarChar;
                parContraseña.Size = 50;
                parContraseña.Value = Empleado.Emp_Contraseña;
                sqlcmd.Parameters.Add(parContraseña);


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
