﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Servicio
    {

        #region Atributos

        DateTime fechaInicio;
        DateTime fechaFinal;
        String categoria;
        String descripcion;
        String tarifa;
        String provincia;
        String canton;
        String distrito;
        internal readonly int Id;

        #endregion

        #region Propiedades

        [Display(Name = "Fecha de inicio del servicio")]
        public DateTime FechaInicial
        {
            get { return this.fechaInicio; }
            set { this.fechaInicio = value; }
        }

        [Display(Name = "Fecha de finalización del servicio")]
        public DateTime FechaFinal
        {
            get { return this.fechaFinal; }
            set { this.fechaFinal = value; }
        }

        [Display(Name = "Categoría")]
        public String Categoria
        {
            get { return this.categoria; }
            set { this.categoria = value; }
        }

        [Display(Name = "Descripción")]
        public String Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        [Display(Name = "Tarifa")]
        public String Tarifa
        {
            get { return this.tarifa; }
            set { this.tarifa = value; }
        }

        [Display(Name = "Provincia")]
        public String Provincia
        {
            get { return this.provincia; }
            set { this.provincia = value; }
        }

        [Display(Name = "Cantón")]
        public String Canton
        {
            get { return this.canton; }
            set { this.canton = value; }
        }

        [Display(Name = "Distrito")]
        public String Distrito
        {
            get { return this.distrito; }
            set { this.distrito = value; }
        }

        public override string ToString()
        {
            return fechaInicio.ToString()
                +fechaFinal.ToString()
                +categoria 
                +descripcion 
                +tarifa
                +provincia
                +canton
                +distrito; 
        }
        #endregion

        #region Constructor

        public Servicio() { }

        public Servicio(DateTime nFechIni, DateTime nFechFin, String nCat, String nDesc, String nTar, String nProv, String nCant, String nDist)
        {
            FechaInicial = nFechIni;
            FechaFinal = nFechFin;
            Categoria = nCat;
            Descripcion = nDesc;
            Tarifa = nTar;
            Provincia = nProv;
            Canton = nCant;
            Distrito = nDist;
        }

        #endregion 

    }
}
