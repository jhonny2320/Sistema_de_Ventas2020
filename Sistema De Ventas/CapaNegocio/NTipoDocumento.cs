using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NTipoDocumento
    {
        public static string insertar(string TD_nombre)
        {
            DTipoDocumento obj = new DTipoDocumento();
            obj.TD_Nombre = TD_nombre;      
            return obj.Insertar(obj);

        }
        public static string Editar(int TD_id, string TD_nombre)
        {
            DTipoDocumento obj = new DTipoDocumento();
            obj.TD_Id = TD_id;
            obj.TD_Nombre = TD_nombre;
            return obj.Editar(obj);

        }
        public static string Eliminar(int TD_id)
        {
            DTipoDocumento obj = new DTipoDocumento();
            obj.TD_Id = TD_id;

            return obj.Eliminar(obj);

        }
        public static DataTable Mostrar()
        {
            return new DTipoDocumento().Mostrar();
        }
    }
}
