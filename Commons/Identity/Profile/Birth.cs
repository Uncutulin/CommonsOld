using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Commons.Identity.Profile
{
    [Owned]
    public class Birth
    {
        [Required(ErrorMessage = "La Fecha de Nacimiento es un dato requerido")]
        [DisplayName("Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Lugar de nacimiento")]
        public string Place { get; set; }

        [DisplayName("Nacionalidad")]
        public string Nationality { get; set; }

    }
}
