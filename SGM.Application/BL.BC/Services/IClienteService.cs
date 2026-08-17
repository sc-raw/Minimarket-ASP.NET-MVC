using System;
using System.Collections.Generic;
using System.Text;
using SGM.Domain.Entities;

namespace SGM.Application.BL.BC.Service
{
    public interface IClienteService
    {
        List<Cliente> Listar();
        Cliente? ObtenerPorId(int id);
        bool Registrar(Cliente cliente);
        bool Actualizar(Cliente cliente);
        bool Eliminar(int id);
    }
}
