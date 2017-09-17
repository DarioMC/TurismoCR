using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Reseña
    {

        #region Atributos

        int idReseña;
        String usuario;
        String servicioId;
        String comentario;
        int calificacion;
        DateTime fecha;

        #endregion

        #region Propiedades

        public int IdReseña
        {
            get { return this.idReseña; }
            set { this.idReseña = value; }
        }

        public String Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public String IdServicio
        {
            get { return this.IdServicio; }
            set { this.IdServicio = value; }
        }

        public String Comentario
        {
            get { return this.comentario; }
            set { this.comentario = value; }
        }

        public int Calificacion
        {
            get { return this.calificacion; }
            set { this.calificacion = value; }
        }

        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; }
        }

        #endregion

        #region Constructor

        public Reseña() { }

        public Reseña(int nId, String nUser, String nServicio, String nComment, int nCalif, DateTime nFecha)
        {
            IdReseña = nId;
            Usuario = nUser;
            IdServicio = nServicio;
            Comentario = nComment;
            Calificacion = nCalif;
            Fecha = nFecha;
        }

        #endregion

    }
}
