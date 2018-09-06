using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Falla.API.Models
{
    [Table(name: "AsistenciaEventos")]
    public class AsistenciaEvento
    {
        [Key]
        public int AsistenciaEventoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Evento")]
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Componente")]
        public int ComponenteId { get; set; }

        [Display(Name = "¿ Es Infantil ?")]
        public bool EsInfantil { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }
    }
}
