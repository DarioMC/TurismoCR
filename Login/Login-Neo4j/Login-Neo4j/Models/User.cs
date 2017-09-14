using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Login_Neo4j.Models
{
    public partial class User
    {
        public String Nombre { get; set; }
        
        public String Apellidos { get; set; }

        [DisplayName(displayName: "Usuario")]
        [Required(ErrorMessage ="Campo Vacio ")]
        public String UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Vacio")]
        public String Password { get; set; }

        public int Telefono { get; set; }

        public String Correo { get; set; }
        
        public String Genero { get; set; }

        public int Cedula { get; set; }

        public DateTime FechaN { get; set; }



        
    }
}