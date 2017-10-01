using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Neo4jClient;
using System.Web;
using static TurismoCR.Controllers.ImageController;

namespace TurismoCR.Models
{
    public class Imagenes
    {

        public HttpPostedFileBase img1 { get; set; }
        public HttpPostedFileBase img2 { get; set; }
        public HttpPostedFileBase img3 { get; set; }
        public HttpPostedFileBase img4 { get; set; }
        public HttpPostedFileBase img5 { get; set; }

        public Imagenes() { }

        public Imagenes(HttpPostedFileBase ni1, HttpPostedFileBase ni2, HttpPostedFileBase ni3, HttpPostedFileBase ni4, HttpPostedFileBase ni5)
        {
            img1 = ni1;
            img2 = ni2;
            img3 = ni3;
            img4 = ni4;
            img5 = ni5;
        }


    }
}
