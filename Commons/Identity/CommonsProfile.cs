using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Commons.Identity.Profile;
using Commons.Models;

namespace Commons.Identity
{
    /// <summary>
    /// Inner use for commons.
    /// </summary>
    public abstract class CommonsProfile : Documental
    {
        /*
         *
         *                                          INFORMACION DEL PERFIL
         *
         */

        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Segundo Nombre")]
        public string MiddleName { get; set; }

        [DisplayName("Apellidos")]
        public string LastName { get; set; }

        [DisplayName("Documento")]
        public virtual Passport Passport { get; set; } = new Passport();

        [DisplayName("Nacimiento")]
        public virtual Birth Birth { get; set; } = new Birth();

        [DisplayName("Teléfono")]
        public string CellPhoneNumber { get; set; }

        [DisplayName("Domicilio")]
        public virtual Address Address { get; set; } = new Address();

        [DisplayName("Avatar")]
        public virtual ProfilePicture ProfilePicture { get; set; }

        [DisplayName("Género")]
        public virtual Gender Gender { get; set; }

        [DisplayName("Religión")]
        public virtual Religion Religion { get; set; }

        [DisplayName("Observaciones")]
        public string Notes { get; set; }
        

        /*
         *
         *                                          RELACIONES
         *
         */
        ////public virtual List<ProfileMail<TUser, CommonsProfile<TUser, TWorkSpace>, TWorkSpace>> ReceivedMails { get; set; } = new List<ProfileMail<TUser, CommonsProfile<TUser, TWorkSpace>, TWorkSpace>>();
        ////public virtual List<Mail.Mail<TUser, CommonsProfile<TUser, TWorkSpace>, TWorkSpace>> SentMails { get; set; } = new List<Mail.Mail<TUser, CommonsProfile<TUser, TWorkSpace>, TWorkSpace>>();


        /*
         *
         *                                          METODOS
         *
         */

        /// <summary>
        /// Obtener la edad actual a partir de la fecha de nacimiento.
        /// </summary>
        public int Age => GetAge();
            
        /// <summary>
        /// Obtener la edad actual a partir de la fecha de nacimiento.
        /// </summary>
        /// <returns></returns>
        public int GetAge()
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - Birth.Date.Year;
            // Go back to the year the person was born in case of a leap year
            if (Birth.Date > today.AddYears(-age)) age--;
            // Return age
            return age;
        }

        /// <summary>
        /// Obtener los nombres concatenados sin el MiddleName.
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return $"{LastName}, {FirstName} {MiddleName}";
        }

        /// <summary>
        /// Returns a string represenntinng the profiel type showed in the user quick menu.
        /// </summary>
        /// <returns></returns>
        public string GetRoleString()
        {
            return "User";
        }

        /*
         *
         *                                          RELACIONES COMPLEJAS
         *
         */

        /// <summary>
        /// Returns Profile related institutes.
        /// </summary>
        /// <returns></returns>
        public List<IWorkSpace> GetRelatedWorkSpaces()
        {
            return new List<IWorkSpace>();
        }
    }
}

