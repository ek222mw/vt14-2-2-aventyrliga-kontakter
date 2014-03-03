using AventyrligaKontakter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AventyrligaKontakter.Model
{
    public class Service
    {   //fält
        private ContactDAL _contactDAL;

        //egenskaper
        private ContactDAL ContactDAL
        {
            get
            {
                return _contactDAL ?? (_contactDAL = new ContactDAL());
            }
        }

        //metoder
        public void DeleteContact(Contact contact)
        {

        }

        public void DeleteContact(int contactid)
        {
            ContactDAL.DeleteContacts(contactid);
        }

        public Contact GetContact(int contactid)
        {
            return ContactDAL.GetContactById(contactid);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

       public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void SaveContact(Contact contact)
        {
            /*  var validationContext = new ValidationContext(contact);
             var validationResults = new List<ValidationResult>();
             if (!Validator.TryValidateObject(contact, validationContext, validationResults, true))
             {
             //    // Uppfyller inte objektet affärsreglerna kastas ett undantag med
             //    // ett allmänt felmeddelande samt en referens till samlingen med
             //    // resultat av valideringen.
                 var ex = new ValidationException("Objektet klarade inte valideringen.");
               ex.Data.Add("ValidationResults", validationResults);
                 throw ex;
             //}*/

            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults)) // Använder "extension method" för valideringen!
            {                                              // Klassen finns under App_Infrastructure.
                // ...kastas ett undantag med ett allmänt felmeddelande samt en referens 
                // till samlingen med resultat av valideringen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
                if (contact.ContactId == 0) // Ny post om CustomerId är 0!
                {
                    ContactDAL.InsertContact(contact);
                }
                else
                {
                    ContactDAL.UpdateContact(contact);
                }
            }


        }
    }
