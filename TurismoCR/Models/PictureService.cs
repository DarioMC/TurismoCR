using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TurismoCR.Models
{
    public class PictureService
    {
		#region Properties

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public String _id { get; set; }

        public String Picture { get; set; }

        public String RandID { get; set; }

		#endregion

		#region Constructor & Destructor
		
        public PictureService() {}

        public PictureService(String picture, String randID)
        {
            Picture = picture;
            RandID = randID;
        }

		#endregion
	}
}
