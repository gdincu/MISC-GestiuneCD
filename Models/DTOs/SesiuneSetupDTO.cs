using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestiuneCD.Models.DTOs
{
    public class SesiuneSetupDTO
    {
        [Required]
        public int idCD { get; set; }
        [Required]
        public TipSesiune tipSesiune { get; set; }
        public VitezaInscriptionare? vitezaInscriptionare { get; set; }
    }
}
