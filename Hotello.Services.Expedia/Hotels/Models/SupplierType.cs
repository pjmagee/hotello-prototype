using System.ComponentModel;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public enum SupplierType
    {
        [Description("Expedia")] E,
        [Description("Venere")] V,
        [Description("Expedia And Venere")] EV,
        [Description("Sabre")] S,
        [Description("Worldspan")] W
    }
}