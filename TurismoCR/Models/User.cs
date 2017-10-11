using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neo4jClient;

namespace TurismoCR.Models
{
    [Serializable]
    public class User
    {
		#region Properties

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Name { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String LastName1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String LastName2 { get; set; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String ID { get; set; }

		[Display(Name = "Fecha de nacimiento")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String BirthDate { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Genre { get; set; }

        [Display(Name = "Número de teléfono")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String PhoneNumber { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Email { get; set; }

        [DisplayName(displayName: "Nombre de Usuario")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Rol { get; set; }

        [Display(Name = "Habilitado")]
        public Boolean Enabled { get; set; }

        #endregion

        #region Constructor & Destructor

        public User() {}

        public User(String name, String lastName1, String lastName2,
                    String id, String birthDate, String genre, String phone,
                    String email, String user, String password, String rol, Boolean enabled)
        {
            Name = name;
            LastName1 = lastName1;
            LastName2 = lastName2;
            ID = id;
            BirthDate = birthDate;
            Genre = genre;
            PhoneNumber = phone;
            Email = email;
            UserName = user;
            Password = password;
            Rol = rol;
            Enabled = enabled;
        }

        public User(String userName, String password)
        {
            UserName = userName;
            Password = password;
        }

		#endregion

		#region Methods

        public Boolean UserFollowsMe(String username)
		{
            try
            {
                var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
                client.Connect();
                bool follow = (
                    client.Cypher
                          .Match("(a: User { UserName: '" + username + "'})-[:Sigue]->(b: User { UserName: '" + UserName + "'})")
                          .Return<Node<User>>("a")
                          .Results
                          .Count() == 1
                );
                return follow;
            }
            catch
            {
                return false;
            }
		}

		#endregion
	}
}
