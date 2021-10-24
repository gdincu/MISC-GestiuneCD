using GestiuneCD.Models.Enums;

namespace GestiuneCD.Models.Entities
{
    public class Sesiune : BaseEntity
    {
        public Sesiune()
        {
        }

        public Sesiune(CD cd, int idCD, DateTime? startDateTime, DateTime? endDateTime, TipSesiune tipSesiune, VitezaInscriptionare? vitezaInscriptionare, StatusSesiune statusSesiune)
        {
            this.cd = cd;
            this.idCD = idCD;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.tipSesiune = tipSesiune;
            this.statusSesiune = statusSesiune;
            this.VitezaInscriptionare = vitezaInscriptionare;
        }

        public CD cd { get; set; }
        public int idCD { get; set; }
        public DateTime? startDateTime { get; set; } = null;
        public DateTime? endDateTime { get; set; } = null;
        public TipSesiune tipSesiune { get;set; }
        public StatusSesiune statusSesiune { get; set; }
        public VitezaInscriptionare? VitezaInscriptionare { get; set; }

    }
}
