using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AventyrligaKontakter.Model
{
    public class Contact
    {
        public int ContactId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "En Email adress måste anges.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email adressen verkar inte vara korrekt.")]
        public string EmailAdress
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Ett Förnamn måste anges.")]
        [StringLength(50, ErrorMessage = "Förnamnet kan bestå av som mest 50 tecken.")]
        public string FirstName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Ett Efternamn måste anges.")]
        [StringLength(50, ErrorMessage = "Efternamnet kan bestå av som mest 50 tecken.")]
        public string LastName
        {
            get;
            set;
        }
    }
}