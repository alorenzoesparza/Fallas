using System;

namespace Fallas.API.Models
{
    public class ActResponse
    {
        public int IdAct { get; set; }

        public string Titular { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaActo { get; set; }

        public string HoraActo { get; set; }

        public string Precio { get; set; }

        public string PrecioInfantiles { get; set; }

        public bool ActoOficial { get; set; }

        public string Imagen { get; set; }

        //[NotMapped]
        //[Display(Name = "Imagen")]
        //public HttpPostedFileBase ImagenFile { get; set; }

        public string Imagen500 { get; set; }

        public bool PagInicio { get; set; }

        public bool YaEfectuado { get; set; }

        //public List<ActAssistanceResponse> ActAssistances { get; set; }
        //public List<DependentResponse> Dependents { get; set; }

        //public List<ActSupporter> ActSupporters { get; set; }

    }
}