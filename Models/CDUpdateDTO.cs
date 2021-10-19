using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models
{
    public class CDUpdateDTO
    {
        [Required]
        public string nume { get; set; }
        public int vitezaDeInscriptionare { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupatAditional { get; set; }
        [Required]
        public TipSesiune tipSesiune { get; set; }
    }
}
