using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TurismoCR.Models
{
    [Serializable]
    public class Service
    {
		#region Properties

		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }

		public String OwnerUsername { get; set; }

		public String Name { get; set; }

		public String Description { get; set; }

		public String Category { get; set; }

        public String Province { get; set; }

        public String Canton { get; set; }

        public String District { get; set; }

		public String Latitude { get; set; }

		public String Longitude { get; set; }

		public String StartDate { get; set; }

		public String EndDate { get; set; }

        public String Price { get; set; }

		public Boolean Enabled { get; set; }

        public String Picture { get; set; }

        

        #endregion

        #region Constructor & Destructor

        public Service() { }

        public Service(String ownerUsername, String name, 
                   String description,String category, String province, String canton, 
                   String district, String latitude, String longitude, String startDate, 
                   String endDate, String price, Boolean enabled, String picture)
        {
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
            Picture = picture;
		}

        public static implicit operator UpdateDefinition<object>(Service v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
