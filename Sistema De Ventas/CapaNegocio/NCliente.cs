using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NCliente
    {
        //metodo para comunicarse con la capa datos

        public static string insertar(string Cli_nombre, string Cli_apellido, int Cli_idTipoDocumento, 
            string Cli_documneto, string Cli_telefono, string Cli_direccion)
        {
            DCliente obj = new DCliente();
            obj.Cli_Nombre = Cli_nombre;
            obj.Cli_Apellido = Cli_apellido;
            obj.Cli_idTipoDocumento = Cli_idTipoDocumento;
            obj.Cli_Documento = Cli_documneto;
            obj.Cli_Telefono = Cli_telefono;
            obj.Cli_Direccion = Cli_direccion;

            return obj.Insertar(obj);

        }
        public static string Editar(int Cli_id,string Cli_nombre, string Cli_apellido, int Cli_idTipoDocumento,
           string Cli_documento, string Cli_telefono, string Cli_direccion)
        {
            DCliente obj = new DCliente();
            obj.Cli_Id = Cli_id;
            obj.Cli_Nombre = Cli_nombre;
            obj.Cli_Apellido = Cli_apellido;
            obj.Cli_idTipoDocumento = Cli_idTipoDocumento;
            obj.Cli_Documento = Cli_documento;
            obj.Cli_Telefono = Cli_telefono;
            obj.Cli_Direccion = Cli_direccion;

            return obj.Editar(obj);

        }
        public static string Eliminar(int Cli_id)
        {
            DCliente obj = new DCliente();
            obj.Cli_Id = Cli_id;

            return obj.Eliminar(obj);

        }

        public static DataTable Mostrar()
        {
            return new DCliente().Mostrar();
        }

        public static DataTable BuscarCliente(string textoBuscar)
        {
            DCliente obj = new DCliente();
            obj.TextoBuscar = textoBuscar;

            return obj.BuscarCliente(obj);
        }
    }
}
