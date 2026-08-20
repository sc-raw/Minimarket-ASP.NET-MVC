using BCrypt.Net;
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

            bool passwordValida;


            if (usuario.Password.StartsWith("$2"))
            {
                passwordValida = BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password);
            }
            else
            {
                passwordValida = usuario.Password == request.Password;
            }

            if (!passwordValida)
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

        public bool Registrar(Usuario usuario)
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            return _repo.Registrar(usuario);
        }

        public bool Actualizar(Usuario usuario)
        {
            if (!usuario.Password.StartsWith("$2"))
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            }

            return _repo.Actualizar(usuario);
        }

        public bool Eliminar(int id) => _repo.Eliminar(id);
    }
}