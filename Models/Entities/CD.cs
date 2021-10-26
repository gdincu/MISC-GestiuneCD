using GestiuneCD.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class CD : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DimensiuneMB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VitezaInscriptionare VitezaMaxInscriptionare { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TipCD Tip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,4)")]
        public decimal SpatiuOcupat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NrDeSesiuni { get; set; } = 0;
        
        /// <summary>
        /// 
        /// </summary>
        public CD()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nume"></param>
        /// <param name="dimensiuneMB"></param>
        /// <param name="vitezaMaxInscriptionare"></param>
        /// <param name="tip"></param>
        /// <param name="spatiuOcupat"></param>
        /// <param name="nrDeSesiuni"></param>
        public CD(string nume, int dimensiuneMB, VitezaInscriptionare vitezaMaxInscriptionare, TipCD tip, decimal spatiuOcupat, int nrDeSesiuni)
        {
            this.Nume = nume;
            this.DimensiuneMB = dimensiuneMB;
            this.VitezaMaxInscriptionare = vitezaMaxInscriptionare;
            this.Tip = tip;
            this.SpatiuOcupat = spatiuOcupat;
            this.NrDeSesiuni = nrDeSesiuni;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator CD(ActionResult<CD> v)
        {
            throw new NotImplementedException();
        }
    }       
}