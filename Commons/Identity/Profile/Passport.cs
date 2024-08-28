using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Commons.Identity.Profile
{
    [Owned]
    public class Passport
    {
        [DisplayName("Tipo de documento")] public PassportType PassportType { get; set; } = PassportType.DNI;
        [DisplayName("Documento")] public string Value { get; set; }
        [DisplayName("Tramite del documento")] public PassportState PassportState { get; set; } = PassportState.Possess;
        [DisplayName("Condicion del documento")] public bool IsInGoodCondition { get; set; } = true;
    }
    
    public enum PassportType
    {
        DNI,
        CI,
        [Display(Name = "Pasaporte")]
        Passport,
        [Display(Name = "Otro")]
        Other
    }

    public enum PassportState
    {
        [Display(Name = "Posee")]
        Possess,
        [Display(Name = "No Posee")]
        DontPossess,
        [Display(Name = "En Proceso")]
        InProcess,
    }

}
