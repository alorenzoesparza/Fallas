using System;
using System.Collections.Generic;

namespace Falla.API.Models
{
    public class EventosApi
    {
        public int IdEvento { get; set; }

        public string Titular { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaEvento { get; set; }

        public string HoraEvento { get; set; }

        public decimal Precio { get; set; }

        public decimal PrecioInfantiles { get; set; }

        public bool EventoOficial { get; set; }

        public string Imagen { get; set; }

        public string Imagen500 { get; set; }

        public bool PagInicio { get; set; }

        public bool YaEfectuado { get; set; }

        public bool Apuntado { get; set; }

        public int AsistenciaEventoId { get; set; }
    }
}