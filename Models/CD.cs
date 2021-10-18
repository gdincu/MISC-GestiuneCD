using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Domain
{
    public class CD
    {
        public CD()
        {
        }

        public CD(string nume, int dimensiuneMB, int vitezaDeInscriptionare, Tip tip, decimal spatiuOcupat, int nrDeSesiuni, string tipSesiune)
        {
            this.nume = nume;
            this.dimensiuneMB = dimensiuneMB;
            this.vitezaDeInscriptionare = vitezaDeInscriptionare;
            this.tip = tip;
            this.spatiuOcupat = spatiuOcupat;
            this.nrDeSesiuni = nrDeSesiuni;
            this.tipSesiune = tipSesiune;
        }

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