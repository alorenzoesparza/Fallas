using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Falla.API.Models
{
    [Table(name: "Eventos")]
    public class Evento
    {
        [Key]
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(128, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Titular")]
        public string Titular { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha del evento")]
        public DateTime FechaEvento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del evento")]
        public string HoraEvento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio Infantil")]
        public decimal PrecioInfantiles { get; set; }

        [Display(Name = "Evento oficial")]
        public bool EventoOficial { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagenFile { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagen500 { get; set; }

        [Display(Name = "Enviar a pág. inicio")]
        public bool PagInicio { get; set; }

        [Display(Name = "Ya efectuado")]
        public bool YaEfectuado { get; set; }

        [NotMapped]
        public bool Apuntado { get; set; }

        //public List<AsistenciaEvento> AsistenciaEvento { get; set; }
    }
}
