using GestiuneCD.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models
{
    public class CDSetupDTO
    {
        [Required]
        public string nume { get; set; }
        [Required]
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
