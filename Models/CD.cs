using GestiuneCD.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Domain
{
    public class CD
    {

        public int id { get; set; }
        public string nume { get; set; }
        public int dimensiuneMB { get; set; }
        public int vitezaDeInscriptionare { get; set; }
        public TipCD tip { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupat { get; set; }
        public int nrDeSesiuni { get; set; }
        public TipSesiune tipSesiune { get; set; }

        public CD()
        {
        }

        public CD(string nume, int dimensiuneMB, int vitezaDeInscriptionare, TipCD tip, decimal spatiuOcupat, int nrDeSesiuni, TipSesiune tipSesiune)
        {
            this.nume = nume;
            this.dimensiuneMB = dimensiuneMB;
            this.vitezaDeInscriptionare = vitezaDeInscriptionare;
            this.tip = tip;
            this.spatiuOcupat = spatiuOcupat;
            this.nrDeSesiuni = nrDeSesiuni;
            this.tipSesiune = tipSesiune;
        }

        public static implicit operator CD(ActionResult<CD> v)
        {
            throw new NotImplementedException();
        }
    }       
}