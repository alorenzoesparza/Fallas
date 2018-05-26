using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fallas.Domain
{
    public class Act
    {
        [Key]
        public int IdAct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(128, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Titular")]
        public string Titular { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del acto")]
        public DateTime FechaActo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del acto")]
        public string HoraActo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:##0.#0} €", ApplyFormatInEditMode = false)]
        [MaxLength(10, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "El {0} es invalido.")]
        public string Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:##0.#0} €", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio Infantil")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "El {0} es invalido.")]
        [MaxLength(10, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        public string PrecioInfantiles { get; set; }

        [Display(Name = "Acto oficial")]
        public bool ActoOficial { get; set; }

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

        //[JsonIgnore]
        //public virtual ICollection<ActAssistance> ActAssistances { get; set; }

        //[JsonIgnore]
        //public ICollection<ActSupporter> ActSupporters { get; set; }
    }
}
