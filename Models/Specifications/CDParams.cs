using GestiuneCD.Domain;

namespace GestiuneCD.Models.Specifications
{
    public class CDParams
    {
        public bool? orderedByName { get; set; } = false;
        public int? minSpatiuLiber { get; set; } = 0;
        public int? vitezaDeInscriptionare { get; set; } = 0;
        public TipCD? tipCD { get; set; } = null;
        public bool? cuSesiuniDeschise { get; set; } = null;
    }
}