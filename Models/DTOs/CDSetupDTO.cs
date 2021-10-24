using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models.DTOs
{
    public class CDSetupDTO
    {
        [Required(ErrorMessage = "{0} este obligatoriu")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Numele trebuie sa contina minim 3 caractere si maxim 50 de caractere")]
        [DataType(DataType.Text)]
        public string? nume { get; set; }
        [Required(ErrorMessage = "{0} este obligatorie")]
        public VitezaInscriptionare vitezaMaxInscriptionare { get; set; }
        [Required]
        public TipCD tip { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupat { get; set; }
    }
}
