using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestiuneCD.Models
{
    public class CDUpdateDTO
    {
        [Required]
        public string? nume { get; set; }
        [Required]
        public VitezaInscriptionare vitezaMaxInscriptionare { get; set; }
    }
}
