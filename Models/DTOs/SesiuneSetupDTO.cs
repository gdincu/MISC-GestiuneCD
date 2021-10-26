using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestiuneCD.Models.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class SesiuneSetupDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int IdCD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public TipSesiune TipSesiune { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VitezaInscriptionare? VitezaInscriptionare { get; set; }
    }
}
