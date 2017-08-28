using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Servicio
    {

        #region Atributos

        String idServicio;
        DateTime fechaInicio;
        DateTime fechaFinal;
        String categoria;
        String descripcion;
        String tarifa;
        String provincia;
        String canton;
        String distrito;

        #endregion

        #region Propiedades

        public String IdServicio
        {
            get { return this.idServicio; }
            set { this.idServicio = value; }
        }

        public DateTime FechaInicial
        {
            get { return this.fechaInicio; }
            set { this.fechaInicio = value; }
        }

        public DateTime FechaFinal
        {
            get { return this.fechaFinal; }
            set { this.fechaFinal = value; }
        }

        public String Categoria
        {
            get { return this.categoria; }
            set { this.categoria = value; }
        }

        public String Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public String Tarifa
        {
            get { return this.tarifa; }
            set { this.tarifa = value; }
        }

        public String Provincia
        {
            get { return this.provincia; }
            set { this.provincia = value; }
        }

        public String Canton
        {
            get { return this.canton; }
            set { this.canton = value; }
        }

        public String Distrito
        {
            get { return this.distrito; }
            set { this.distrito = value; }
        }

        #endregion

        #region Constructor

        public Servicio() { }

        public Servicio(String nId, DateTime nFechIni, DateTime nFechFin, String nCat, String nDesc, String nTar, String nProv, String nCant, String nDist)
        {
            IdServicio = nId;
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
