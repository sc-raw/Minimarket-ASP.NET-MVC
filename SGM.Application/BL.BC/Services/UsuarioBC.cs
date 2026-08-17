using System;
using System.Collections.Generic;
using System.Text;
using SGM.Application.BL.BE;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;

namespace SGM.Application.BL.BC.Service
{
    public class UsuarioBC : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioBC(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public List<Usuario> Listar() => _repo.Listar();

        public Usuario? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public LoginResponse Login(LoginRequest request)
        {
            var usuario = _repo.ObtenerPorUsername(request.Username);

            if (usuario == null || !usuario.Estado)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos"
                };
            }

            // Por ahora comparación directa (luego se cambia por hash)
            if (usuario.Password != request.Password)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos"
                };
            }

            return new LoginResponse
            {
                Success = true,
                Message = "Login exitoso",
                IdUsuario = usuario.Id,
                Username = usuario.Username,
                Rol = usuario.Rol,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos
            };
        }

        public bool Registrar(Usuario usuario) => _repo.Registrar(usuario);

        public bool Actualizar(Usuario usuario) => _repo.Actualizar(usuario);

        public bool Eliminar(int id) => _repo.Eliminar(id);
    }
}
