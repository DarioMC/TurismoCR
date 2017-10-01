using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Carrito
    {

        #region Atributos

        int idCarrito;
        String usercar;
        List<Models.Service> products;

        #endregion

        #region Propiedades

        public int IdCarrito
        {
            get { return this.idCarrito; }
            set { this.idCarrito = value; }
        }

        public String UserCar
        {
            get { return this.usercar; }
            set { this.usercar = value; }
        }

        public List<Service> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        #endregion

        #region Constructor

        public Carrito() { }

        public Carrito(int nId, String nUser, List<Service> nProds)
        {
            IdCarrito = nId;
            UserCar = nUser;
            Products = nProds;
        }

        #endregion 

    }
}
