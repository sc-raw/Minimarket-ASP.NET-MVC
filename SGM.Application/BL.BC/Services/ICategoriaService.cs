using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Application.BL.BC.Service
{
    public interface ICategoriaService
    {
        List<Categoria> Listar();
        Categoria? ObtenerPorId(int id);
        bool Registrar(Categoria categoria);
        bool Actualizar(Categoria categoria);
        bool Eliminar(int id);
    }
}