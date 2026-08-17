using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Application.BL.BE
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
    }
}
