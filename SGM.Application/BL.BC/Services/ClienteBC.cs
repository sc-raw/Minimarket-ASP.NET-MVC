using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;
using SGM.Domain.Interfaces;

namespace SGM.Application.BL.BC.Service
{
    public class ClienteBC : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteBC(IClienteRepository repo)
        {
            _repo = repo;
        }

        public List<Cliente> Listar() => _repo.Listar();
        public Cliente? ObtenerPorId(int id) => _repo.ObtenerPorId(id);
        public bool Registrar(Cliente cliente) => _repo.Registrar(cliente);
        public bool Actualizar(Cliente cliente) => _repo.Actualizar(cliente);
        public bool Eliminar(int id) => _repo.Eliminar(id);
    }
}