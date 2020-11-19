using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NArticulo
    {

        public static string Insertar(string Art_codigo, string Art_nombre,string Art_descripcion,int Art_idCategoria)
        {
            DArticulo obj = new DArticulo();
            obj.Art_Codigo = Art_codigo;
            obj.Art_Nombre = Art_nombre;
            obj.Art_Descripcion = Art_descripcion;
            
            obj.Art_IdCategoria = Art_idCategoria;

            return obj.Insertar(obj);
        }

        public static string Editar(int Art_id, string Art_codigo, string Art_nombre, string Art_descripcion, int Art_idCategoria)
        {
            DArticulo obj = new DArticulo();
            obj.Art_Id = Art_id;
            obj.Art_Codigo = Art_codigo;
            obj.Art_Nombre = Art_nombre;
            obj.Art_Descripcion = Art_descripcion;            
            obj.Art_IdCategoria = Art_idCategoria;

            return obj.Editar(obj);
        }

        public static string Eliminar(int Art_id)
        {
            DArticulo obj = new DArticulo();
            obj.Art_Id = Art_id;

            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DArticulo().Mostrar();
        }

        public static DataTable BuscarArticulo(string textoBuscar)
        {
            DArticulo obj = new DArticulo();
            obj.TextoBuscar = textoBuscar;

            return obj.BuscarArticulo(obj);
        }

    }
}
