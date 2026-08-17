using System;
using System.Collections.Generic;
using System.Text;
using SGM.Application.BL.BE;
using SGM.Domain.Entities;

namespace SGM.Application.BL.BC.Service
{
    public interface IUsuarioService
    {
        List<Usuario> Listar();
        Usuario? ObtenerPorId(int id);
        LoginResponse Login(LoginRequest request);
        bool Registrar(Usuario usuario);
        bool Actualizar(Usuario usuario);
        bool Eliminar(int id);
    }
}
