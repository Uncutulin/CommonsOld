using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Commons.Identity.Profile
{
    [Owned]
    public class Address
    {

        [DisplayName("Calle")] public string Street { get; set; }

        [DisplayName("Número")] public int Number { get; set; }
        [DisplayName("Piso")] public string Floor { get; set; }

        [DisplayName("Torre")] public string Tower { get; set; }

        [DisplayName("Departamento")] public string Department { get; set; }

        [DisplayName("Entre calles")] public string BetweennStreets { get; set; }

        [DisplayName("Observaciones")] public string Notes { get; set; }

        [DisplayName("Provincia")] public string Province { get; set; }

        [DisplayName("Distrito")] public string District { get; set; }

        [DisplayName("Localidad")] public string Location { get; set; }

        [DisplayName("Código Postal")] public string PostalCode { get; set; }


    }
}
