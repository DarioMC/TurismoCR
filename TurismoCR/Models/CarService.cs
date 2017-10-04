using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class CarService:Service
    {
        #region Attribute
        String Quantity;

        #endregion

        #region Properties
        public String Quanty
        {
            get { return this.Quantity; }
            set { this.Quantity = value; }
        }
        #endregion

        #region Constructor
        public CarService() : base()
            {
        }

        public CarService(Service ser, String Quant) : base(ser)
        {
            Quantity = Quant;
        }
        #endregion

    }
}
