using System.ComponentModel.DataAnnotations;

namespace PromotionsCRM.Common.Enums
{
    public enum StatusName
    {
        [Display(Name = "Being processed")]
        BeingProcessed = 0,
        
        Valid = 1,
        
        Invalid = 2,
        
        [Display(Name = "Missing info")]
        MissingInfo = 3,
        
        Cancelled = 4,
    }
}
