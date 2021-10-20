﻿using GestiuneCD.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiuneCD.Domain
{
    public class CD : BaseEntity
    {

        public string nume { get; set; }
        public int dimensiuneMB { get; set; }
        public int vitezaDeInscriptionare { get; set; }
        public TipCD tip { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal spatiuOcupat { get; set; }
        public int nrDeSesiuni { get; set; } = 0;

        public CD()
        {
        }

        public CD(string nume, int dimensiuneMB, int vitezaDeInscriptionare, TipCD tip, decimal spatiuOcupat, int nrDeSesiuni)
        {
            this.nume = nume;
            this.dimensiuneMB = dimensiuneMB;
            this.vitezaDeInscriptionare = vitezaDeInscriptionare;
            this.tip = tip;
            this.spatiuOcupat = spatiuOcupat;
            this.nrDeSesiuni = nrDeSesiuni;
        }

        public static implicit operator CD(ActionResult<CD> v)
        {
            throw new NotImplementedException();
        }
    }       
}