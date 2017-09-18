using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Orden
    {

        #region Atributos

        int idOrden;
        String usuario;
        DateTime fecha;
        String descripcion;
        String precio;

        #endregion

        #region Propiedades

        public int IdOrden
        {
            get { return this.idOrden; }
            set { this.idOrden = value; }
        }

        public String Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; }
        }
        
        public String Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public String Precio
        {
            get { return this.precio; }
            set { this.precio = value; }
        }

        #endregion

        #region Constructor

        public Orden() { }

        public Orden(int nId, String nUser, DateTime nFecha, String nDesc, String nPrecio)
        {
            IdOrden = nId;
            Usuario = nUser;
            Fecha = nFecha;
            Descripcion = nDesc;
            Precio = nPrecio;
        }

        #endregion 

    }
}
