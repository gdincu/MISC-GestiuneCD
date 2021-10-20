using GestiuneCD.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models
{
    public class CDSetupDTO
    {
        [Required(ErrorMessage = "{0} este obligatoriu")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Numele trebuie sa contina minim 3 caractere si maxim 50 de caractere")]
        [DataType(DataType.Text)]
        public string nume { get; set; }
        [Required(ErrorMessage = "{0} este obligatorie")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} poate fi doar un numar intreg pozitiv")]
        public int vitezaDeInscriptionare { get; set; }
        [Required]
        public TipCD tip { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupat { get; set; }
        [Required]
        public TipSesiune tipSesiune { get; set; }
    }
}
