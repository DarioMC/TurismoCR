using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Reseña
    {

        private Guid _id;
        [System.ComponentModel.DataAnnotations.Key]
        public Guid IdResenia
        {
            get { return _id; }
            set { _id = new Guid(); }
        }
        public int IdServicio { get; set; }
        public String Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public String Comentario { get; set; }

        #region Constructor

        public Reseña() { }

        public Reseña(String nUser, int nServicio, String nComment, int nCalif, DateTime nFecha)
        {
            IdResenia = new Guid();
            Usuario = nUser;
            IdServicio = nServicio;
            Comentario = nComment;
            Calificacion = nCalif;
            Fecha = nFecha;
        }

        #endregion

    }
}