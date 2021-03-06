﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Orden
    {
        //ate Guid _id;
        [System.ComponentModel.DataAnnotations.Key]
        public int IdOrden { get; set; }
        public String IdCarrito { get; set; }
        public DateTime Fecha { get; set; }
        public String Usuario { get; set; }
        public int Tarifa { get; set; }
        public String Categoria { get; set; }
        public String Descripción { get; set; }

        #region Constructor

        public Orden() { }

        public Orden(String nIdCarrito, String nUser, DateTime nFecha, 
                     String nCat, String nDesc, int nPrecio)
        {
            IdCarrito = nIdCarrito;
            Usuario = nUser;
            Fecha = nFecha;
            Descripción = nDesc;
            Categoria = nCat;
            Tarifa = nPrecio;
        }

        #endregion 

    }
}
