using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Domain.Entities
{
    public class Cliente : Persona
    {
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
