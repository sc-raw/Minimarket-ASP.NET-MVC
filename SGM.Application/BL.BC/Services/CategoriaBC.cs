using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;

namespace SGM.Application.BL.BC.Service
{
    public class CategoriaBC : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaBC(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public List<Categoria> Listar() => _repo.Listar();
        public Categoria? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
        public bool Registrar(Categoria categoria) => _repo.Registrar(categoria);
        public bool Actualizar(Categoria categoria) => _repo.Actualizar(categoria);
        public bool Eliminar(int id) => _repo.Eliminar(id);
    }
}
