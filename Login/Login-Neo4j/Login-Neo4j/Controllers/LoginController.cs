using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_Neo4j.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Authorize(Login_Neo4j.Models.User Userlog)
        {
            using (var driver = GraphDatabase.Driver("bolt://127.0.0.1:7687", AuthTokens.Basic("neo4j", "chavacampos14")))
            using (var session = driver.Session())
            {
                //var result = session.Run("CREATE (n) RETURN n");
                var result = session.Run("MATCH(a: Persona) -[:Es]->(b: Usuario) WHERE a.Usuario = "+
                    "'"+Userlog.UserName+"' and a.Contrasena = '"+Userlog.Password+ "' RETURN a.Usuario , a.Contrasena ",
                            new Dictionary<string, object> { { "Usuario", "Contrasena" } });
                //
                object salida = null; 
                foreach (var record in result)
                {
                    if (record.Values.TryGetValue("a.Usuario", out salida) )
                    {
                        if (salida != null)
                        {
                            return View();
                        }
                    }
                }
                return View("Index");
                
                
                
            }

            
        }
    }
}