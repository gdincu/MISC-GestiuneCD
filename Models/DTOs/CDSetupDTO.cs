using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class CDSetupDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0} este obligatoriu")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Numele trebuie sa contina minim 3 caractere si maxim 50 de caractere")]
        [DataType(DataType.Text)]
        public string? Nume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0} este obligatorie")]
        public VitezaInscriptionare VitezaMaxInscriptionare { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public TipCD Tip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal SpatiuOcupat { get; set; }
    }
}
