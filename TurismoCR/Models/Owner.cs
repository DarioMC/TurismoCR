using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TurismoCR.Models
{
    public class Owner: User 
    {
        [Display(Name = "Latitud de su empresa (geolocalización)")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Latitude { get; set; }

        [Display(Name = "Longitud de su empresa (geolocalización)")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Longitude { get; set; }

		[Display(Name = "Descripción de su empresa")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Description { get; set; }

		[Display(Name = "Dirección de su empresa")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Address { get; set; }

        public Owner():base() {}

		public Owner(String name, String lastName1, String lastName2,
					 String id, String birthDate, String genre, String phone,
                     String email, String user, String password, String rol, Boolean enabled,
                     String latitude, String longitude, String description, String address)
                    :base(name, lastName1, lastName2, id, birthDate, 
                          genre, phone, email, user, password, rol, enabled) {
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Address = address;
        }

        public Owner(String userName, String password):base(userName, password) { }

    }
}