using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Reseña
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdResenia { get; set; }
        public int IdServicio { get; set; }
        public String Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public String Comentario { get; set; }

        #region Constructor

        public Reseña() { }

        public Reseña(int nIdRes, String nUser, int nServicio, String nComment, int nCalif, DateTime nFecha)
        {
            IdResenia = nIdRes;
            Usuario = nUser;
            IdServicio = nServicio;
            Comentario = nComment;
            Calificacion = nCalif;
            Fecha = nFecha;
        }

        #endregion
    }
}