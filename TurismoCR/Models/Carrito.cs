﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Carrito
    {

        #region Atributos

        int idCarrito;
        String usuario;
        List<Models.Servicio> productos;

        #endregion

        #region Propiedades

        public int IdCarrito
        {
            get { return this.idCarrito; }
            set { this.idCarrito = value; }
        }

        public String Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public List<Servicio> Productos
        {
            get { return this.productos; }
            set { this.productos = value; }
        }

        #endregion

        #region Constructor

        public Carrito() { }

        public Carrito(int nId, String nUser, List<Servicio> nProds)
        {
            IdCarrito = nId;
            Usuario = nUser;
            Productos = nProds;
        }

        #endregion 

    }
}