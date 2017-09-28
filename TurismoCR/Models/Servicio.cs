using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId _id;
        String fechaInicio;
        String fechaFinal;
        String categoria;
        String descripcion;
        String tarifa;
        String provincia;
        String canton;
        String distrito;
        String nombreUsuarioPropietario;
        internal readonly int Id;

		#endregion

		#region Propiedades

        [Display(Name = "Fecha de inicio del servicio")]
        public String FechaInicial
        {
            get { return this.fechaInicio; }
            set { this.fechaInicio = value; }
        }

        [Display(Name = "Fecha de finalización del servicio")]
        public String FechaFinal
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

		public String NombreUsuarioPropietario
		{
			get { return this.nombreUsuarioPropietario; }
			set { this.nombreUsuarioPropietario = value; }
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

        public Servicio(String nFechIni, String nFechFin, String nCat, 
                        String nDesc, String nTar, String nProv, 
                        String nCant, String nDist, String nNombreUsuarioPropietario)
        {
            FechaInicial = nFechIni;
            FechaFinal = nFechFin;
            Categoria = nCat;
            Descripcion = nDesc;
            Tarifa = nTar;
            Provincia = nProv;
            Canton = nCant;
            Distrito = nDist;
            NombreUsuarioPropietario = nNombreUsuarioPropietario;
        }

        #endregion 

    }
}
