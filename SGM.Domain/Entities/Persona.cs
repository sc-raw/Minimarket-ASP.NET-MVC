using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool Estado { get; set; } = true;
    }
}
