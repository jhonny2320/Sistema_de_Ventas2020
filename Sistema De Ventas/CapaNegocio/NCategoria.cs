using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NCategoria
    {
        public static string Insertar(string Cat_nombre,string Cat_descripcion)
        {
            DCategoria obj = new DCategoria();
            obj.Cat_Nombre = Cat_nombre;
            obj.Cat_Descripcion = Cat_descripcion;

            return obj.Insertar(obj);

        }

        public static string Editar(int Cat_id,string Cat_nombre, string Cat_descripcion)
        {
            DCategoria obj = new DCategoria();
            obj.Cat_id = Cat_id;
            obj.Cat_Nombre = Cat_nombre;
            obj.Cat_Descripcion = Cat_descripcion;

            return obj.Editar(obj);

        }

        public static string Eliminar(int Cat_id)
        {
            DCategoria obj = new DCategoria();
            obj.Cat_id = Cat_id;
            
            return obj.Eliminar(obj);

        }

        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }

        public static DataTable BuscarCategoria(string textoBuscar)
        {
            DCategoria obj = new DCategoria();
            obj.TextoBuscar = textoBuscar;

            return obj.BuscarCategotia(obj);

        }
    }
}
