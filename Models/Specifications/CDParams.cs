using GestiuneCD.Models.Enums;

namespace GestiuneCD.Models.Specifications
{
    /// <summary>
    /// CDParams
    /// </summary>
    public class CDParams
    {
        /// <summary>
        /// 
        /// </summary>
        public bool? OrderedByName { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public bool? OrderedBySize { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public int? MinSpatiuLiber { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public VitezaInscriptionare? VitezaMaxInscriptionare { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public TipCD? TipCD { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public bool? CuSesiuniDeschise { get; set; } = null;
    }
}