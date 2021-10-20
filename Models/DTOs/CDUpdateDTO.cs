using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models
{
    public class CDUpdateDTO
    {
        [Required]
        public string nume { get; set; }
        public int vitezaDeInscriptionare { get; set; }
    }
}
