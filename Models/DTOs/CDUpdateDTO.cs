using GestiuneCD.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestiuneCD.Models.DTOs
{
    /// <summary>
    ///     
    /// </summary>
    public class CDUpdateDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string? Nume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public VitezaInscriptionare VitezaMaxInscriptionare { get; set; }
    }
}
