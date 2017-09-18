using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TurismoCR.Models
{
    public class User
    {
		[Display(Name = "Nombre")]
        [Required(ErrorMessage = "¡Campo Vacío!")]
        public String Name { get; set; }

		[Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "¡Campo Vacío!")]
        public String LastName1 { get; set; }

		[Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "¡Campo Vacío!")]
        public String LastName2 { get; set; }

		[Display(Name = "Cédula")]
        [Required(ErrorMessage = "¡Campo Vacío!")]
        public String ID { get; set; }

		[Display(Name = "Fecha de nacimiento")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        public String BirthDate { get; set; }

		[Display(Name = "Género")]
        [Required(ErrorMessage = "¡Campo Vacío!")]
        public String Genre { get; set; }

		[Display(Name = "Número de teléfono")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        public String PhoneNumber { get; set; }

		[Display(Name = "Correo Electrónico")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        public String Email { get; set; }

		[DisplayName(displayName: "Nombre de Usuario")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        public String UserName { get; set; }

		[Display(Name = "Contraseña")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

		[Display(Name = "Rol")]
		[Required(ErrorMessage = "¡Campo Vacío!")]
        public String Rol { get; set; }

        public User() { }

        public User(String name, String lastName1, 
                    String id, String birthDate, String genre, String phone, 
                    String email, String user, String password, String rol)
        {
            Name = name;
            LastName1 = lastName1;
            ID = id;
            BirthDate = birthDate;
            Genre = genre;
            PhoneNumber = phone;
            Email = email;
            UserName = user;
            Password = password;
            Rol = rol;
        }
    }
}
