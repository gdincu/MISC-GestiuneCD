using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Domain
{
    public class CD
    {
        public int id { get; set; }
        public string nume { get; set; }
        public int dimensiuneMB { get; set; }
        public int vitezaDeInscriptionare { get; set; }
        public Tip tip { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupat { get; set; }
        public int nrDeSesiuni { get; set; }
        public string tipSesiune { get; set; }

    }       
}