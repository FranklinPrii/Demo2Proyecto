using System;

namespace Proyecto2Demo.Models
{
    public class Registro
    {

        public string NotaCodigo { get; set; }
        public bool Activo { get; set; }
        public int ProcesoMatricula { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public bool Visible { get; set; }

    }
}