using Fallas.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fallas.Backend.Models
{
    [NotMapped]
    public class ComponenteView : Componente
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"(^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{6,20}$)",
            ErrorMessage = "Las contraseñas deben tener al menos un carácter que no sea una letra ni un dígito. Las contraseñas deben tener al menos una letra en minúscula ('a'-'z'). Las contraseñas deben tener al menos una letra en mayúscula ('A'-'Z').")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        [Display(Name = "Confirmación Contraseña")]
        public string PasswordConfirm { get; set; }
    }
}