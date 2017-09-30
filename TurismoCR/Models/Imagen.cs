
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurismoCR.Models
{
    [Serializable]
    public class Imagen
    {
        public ObjectId _id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
        public String idServicio { get; set; }
    }
}