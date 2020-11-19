using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NEmpleados

    {
        //metodo para comunicacion con la capa datos

        public static string Insertar(string Emp_nombre,string Emp_apellido,int Emp_idTipoDocumento
            ,string Emp_documento,string Emp_direccion,string Emp_telefono,string Emp_acceso,string Emp_usuario,string Emp_contraseña)
        {
            DEmpleados obj = new DEmpleados();
            obj.Emp_Nombre = Emp_nombre;
            obj.Emp_Apellido = Emp_apellido;
            obj.Emp_idTipoDocumento = Emp_idTipoDocumento;
            obj.Emp_Documento = Emp_documento;
            obj.Emp_Direccion = Emp_direccion;
            obj.Emp_Telefono = Emp_telefono;
            obj.Emp_Acceso = Emp_acceso;
            obj.Emp_Usuario = Emp_usuario;
            obj.Emp_Contraseña = Emp_contraseña;

            return obj.Insertar(obj);
            
        }

        public static string Editar(int Emp_id, string Emp_nombre, string Emp_apellido, int Emp_idTipoDocumento
            , string Emp_documento, string Emp_direccion, string Emp_telefono, string Emp_acceso, string Emp_usuario, string Emp_contraseña)
        {
            DEmpleados obj = new DEmpleados();
            obj.Emp_Id = Emp_id;
            obj.Emp_Nombre = Emp_nombre;
            obj.Emp_Apellido = Emp_apellido;
            obj.Emp_idTipoDocumento = Emp_idTipoDocumento;
            obj.Emp_Documento = Emp_documento;
            obj.Emp_Direccion = Emp_direccion;
            obj.Emp_Telefono = Emp_telefono;
            obj.Emp_Acceso = Emp_acceso;
            obj.Emp_Usuario = Emp_usuario;
            obj.Emp_Contraseña = Emp_contraseña;

            return obj.Editar(obj);

        }
        public static string Eliminar(int Emp_id)
        {
            DEmpleados obj = new DEmpleados();
            obj.Emp_Id = Emp_id;
            
            return obj.Eliminar(obj);

        }
        public static DataTable Mostrar()
        {
            return new DEmpleados().Mostrar();
        }

        public static DataTable BuscarEmpleado(string textoBuscar)
        {
            DEmpleados obj = new DEmpleados();
            obj.TextoBuscar = textoBuscar;

            return obj.BuscarEmpleado(obj);
        }

        public static DataTable Login(string usuario,string contraseña)
        {
            DEmpleados obj = new DEmpleados();
            obj.Emp_Usuario = usuario;
            obj.Emp_Contraseña = contraseña;

            return obj.Login(obj);
        }
    }

}
