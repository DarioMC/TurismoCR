using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoCR.Models
{
    public class Usuario
    {

        #region Atributos

        String nombre;
        String apellido1;
        String apellido2;
        String cedula;
        String fechaNac;
        String genero;
        String telefono;
        String correo;
        String usuario;
        String contraseña;
        int rol;

        #endregion

        #region Propiedades

        public String Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public String Apellido1
        {
            get { return this.apellido1; }
            set { this.apellido1 = value; }
        }

        public String Apellido2
        {
            get { return this.apellido2; }
            set { this.apellido2 = value; }
        }

        public String Cedula
        {
            get { return this.cedula; }
            set { this.cedula = value; }
        }

        public String FechaNac
        {
            get { return this.fechaNac; }
            set { this.fechaNac = value; }
        }

        public String Genero
        {
            get { return this.genero; }
            set { this.genero = value; }
        }

        public String Telefono
        {
            get { return this.telefono; }
            set { this.telefono = value; }
        }

        public String Correo
        {
            get { return this.correo; }
            set { this.correo = value; }
        }

        public String NombreUsuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }

        }

        public String Contraseña
        {
            get { return this.contraseña; }
            set { this.contraseña = value; }
        }

        public int Rol
        {
            get { return this.rol; }
            set { this.rol = value; }
        }

        #endregion

        #region Constructor

        public Usuario() { }

        public Usuario(String nNombre, String nApll1, String nApll2, String nCed, String nfecNac, String nGen, String nTel, String nCorreo, String nUser, String nContra, int nRol)
        {

            Nombre = nNombre;
            Apellido1 = nApll1;
            Apellido2 = nApll2;
            Cedula = nCed;
            FechaNac = nfecNac;
            Genero = nGen;
            Telefono = nTel;
            Correo = nCorreo;
            NombreUsuario = nUser;
            Contraseña = nContra;
            Rol = nRol;

        }

        #endregion 

    }
}
