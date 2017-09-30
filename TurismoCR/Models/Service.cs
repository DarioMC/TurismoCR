﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TurismoCR.Models
{
    [Serializable]
    public class Service
    {
		#region Properties

		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public String BackupID { get; set; }

		[Display(Name = "Nombre de usuario del propietario")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String OwnerUsername { get; set; }

		[Display(Name = "Nombre")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Name { get; set; }

		[Display(Name = "Descripción")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Description { get; set; }

		[Display(Name = "Categoría")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Category { get; set; }

        [Display(Name = "Provincia")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Province { get; set; }

        [Display(Name = "Cantón")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Canton { get; set; }

        [Display(Name = "Distrito")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
        public String District { get; set; }

		[Display(Name = "Latitud del paquete (geolocalización)")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Latitude { get; set; }

		[Display(Name = "Longitud del paquete (geolocalización)")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String Longitude { get; set; }

		[Display(Name = "Fecha de inicio")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String StartDate { get; set; }

		[Display(Name = "Fecha de finalización")]
		[Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String EndDate { get; set; }

		[Display(Name = "Precio")]
        [Required(ErrorMessage = "¡Campo Vacío!", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public String Price { get; set; }

		[Display(Name = "Habilitado")]
		public Boolean Enabled { get; set; }

		#endregion

		#region Methods

		public override string ToString()
        {
            return StartDate
                + "-"
                + EndDate
                + "-"
                + Category
                + "-"
                + Description
                + "-"
                + Price
                + "-"
                + Province
                + "-"
                + Canton
                + "-"
                + District; 
        }

		#endregion

		#region Constructor & Destructor

		public Service() { }

        public Service(ObjectId id, String ownerUsername, String name, String description,
                    String category, String province, String canton, String district,
                    String latitude, String longitude, String startDate, String endDate, 
                    String price, Boolean enabled) 
        {
            _id = id;
            BackupID = "bckid";
            OwnerUsername = ownerUsername;
            Name = name;
            Description = description;
            Category = category;
            Province = province;
            Canton = canton;
            District = district;
			Latitude = latitude;
			Longitude = longitude;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Enabled = enabled;
		}

		#endregion
	}
}
