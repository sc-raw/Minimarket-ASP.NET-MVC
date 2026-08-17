using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;

namespace SGM.Application.BL.BC.Service
{
    public class ProductoBC : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoBC(IProductoRepository repo)
        {
            _repo = repo;
        }

        public List<Producto> Listar() => _repo.Listar();
        public Producto? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
        public bool Registrar(Producto producto) => _repo.Registrar(producto);
        public bool Actualizar(Producto producto) => _repo.Actualizar(producto);
        public bool Eliminar(int id) => _repo.Eliminar(id);
    }
}  

