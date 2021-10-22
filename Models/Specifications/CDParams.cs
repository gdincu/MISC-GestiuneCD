using GestiuneCD.Domain;
using GestiuneCD.Models.Enums;

namespace GestiuneCD.Models.Specifications
{
    public class CDParams
    {
        public bool? orderedByName { get; set; } = false;
        public bool? orderedBySize { get; set; } = false;
        public int? minSpatiuLiber { get; set; } = 0;
        public VitezaInscriptionare? vitezaMaxInscriptionare { get; set; } = null;
        public TipCD? tipCD { get; set; } = null;
        public bool? cuSesiuniDeschise { get; set; } = null;
    }
}