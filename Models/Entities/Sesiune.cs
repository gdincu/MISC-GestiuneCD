using GestiuneCD.Models.Enums;

namespace GestiuneCD.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Sesiune : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Sesiune()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="idCD"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="tipSesiune"></param>
        /// <param name="vitezaInscriptionare"></param>
        /// <param name="statusSesiune"></param>
        public Sesiune(CD cd, int idCD, DateTime? startDateTime, DateTime? endDateTime, TipSesiune tipSesiune, VitezaInscriptionare? vitezaInscriptionare, StatusSesiune statusSesiune)
        {
            this.Cd = cd;
            this.IdCD = idCD;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.TipSesiune = tipSesiune;
            this.StatusSesiune = statusSesiune;
            this.VitezaInscriptionare = vitezaInscriptionare;
        }

        /// <summary>
        /// Cd
        /// </summary>
        public CD Cd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdCD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDateTime { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDateTime { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public TipSesiune TipSesiune { get;set; }
        /// <summary>
        /// 
        /// </summary>
        public StatusSesiune StatusSesiune { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VitezaInscriptionare? VitezaInscriptionare { get; set; }

    }
}
