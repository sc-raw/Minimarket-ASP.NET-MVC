using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorUsername(string username);
        bool Registrar(Usuario usuario);
        bool Actualizar(Usuario usuario);
        bool Eliminar(int id);
    }
}