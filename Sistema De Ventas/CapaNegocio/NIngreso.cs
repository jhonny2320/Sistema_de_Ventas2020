using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NIngreso
    {
        public static string Insertar(int idEmpleado,DateTime fecha,string serie,string estado,DataTable dtDetalle)
        {
            DIngreso obj = new DIngreso();
            obj.IdEmpleado = idEmpleado;
            obj.Fecha = fecha;
            obj.Serie = serie;
            obj.Estado = estado;

            List<DDetalle_Ingreso> Detalles = new List<DDetalle_Ingreso>();
            foreach(DataRow row in dtDetalle.Rows)
            {
                DDetalle_Ingreso objDetalle = new DDetalle_Ingreso();
                objDetalle.IdArticulo = Convert.ToInt32( row["idArticulo"].ToString());
                objDetalle.Precio_Compra = Convert.ToDecimal(row["PRECIO DE COMPRA"].ToString());
                objDetalle.Precio_Venta = Convert.ToDecimal(row["PRECIO DE VENTA"].ToString());
                objDetalle.Stock_Inicial = Convert.ToInt32(row["STOCK INICIAL"].ToString());
                objDetalle.Stock_Actual = Convert.ToInt32(row["STOCK INICIAL"].ToString());

                Detalles.Add(objDetalle);
            }
            return obj.Insertar(obj,Detalles);

        }

        public static string Anular(int idIngreso)
        {
            DIngreso obj = new DIngreso();
            obj.IdIngreso = idIngreso;

            return obj.Anular(obj);

        }

        public static DataTable Mostrar()
        {
            return new DIngreso().Mostrar();
        }

        public static DataTable FechaIngreso(string FechaInicial,string FechaFinal)
        {
            DIngreso obj = new DIngreso();
            return obj.FechaIngreso(FechaInicial, FechaFinal);                   

        }

        public static DataTable MostrarDetalle(string textoBuscar)
        {
            DIngreso obj = new DIngreso();
            return obj.MostrarDetalle(textoBuscar);

        }
    }
}
