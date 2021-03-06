﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AventyrligaKontakter.Model;
using AventyrligaKontakter;

namespace AventyrligaKontakter
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            // Ett Service-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hämtar alla kunder som finns lagrade i databasen.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> ContactListView_GetData()
        {
            return Service.GetContacts();
        }

        /// <summary>
        /// Lägger till en kunds kunduppgifter i databasen.
        /// </summary>
        /// <param name="customer"></param>
        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                Service.SaveContact(contact);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle läggas till.");
            }
        }

        /// <summary>
        /// Uppdaterar en kontakts kontaktuppgifter i databasen.
        /// </summary>
        /// <param name="customerId"></param>
        public void ContactListView_UpdateItem(int ContactId) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            try
            {
                var contact = Service.GetContact(ContactId);
                if (contact == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Kunden med kontaktnummer {0} hittades inte.", ContactId));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    Service.SaveContact(contact);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle uppdateras.");
            }
        }

        /// <summary>
        /// Tar bort specifierad kund ur databasen.
        /// </summary>
        /// <param name="customerId"></param>
        public void ContactListView_DeleteItem(int ContactId) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            try
            {
                Service.DeleteContact(ContactId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle tas bort.");
            }
        }
    }
}