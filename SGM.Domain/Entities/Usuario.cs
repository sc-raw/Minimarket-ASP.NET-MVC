using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;   // "Administrador" o "Cajero"
        public bool Estado { get; set; } = true;
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
    }
}
