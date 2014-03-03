using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AventyrligaKontakter.Model;


namespace AventyrligaKontakter.Pages.ContactPages
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ContactFormView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service service = new Service();
                    service.SaveContact(contact);

                    // Spara (rätt)meddelande och dirigera om klienten till lista med kunder.
                    // (Meddelandet sparas i en "temporär" sessionsvariabel som kapslas 
                    // in av en "extension method" i App_Infrastructure/PageExtensions.)
                    // Del av designmönstret Post-Redirect-Get (PRG, http://en.wikipedia.org/wiki/Post/Redirect/Get).
                    Page.SetTempData("Message", String.Format("Kunden '{0}' lades till.", contact.FirstName));
                    Response.RedirectToRoute("Customers");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle läggas till.");
                }
            }
        }

        protected void ContactFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                // Dirigera om klienten till lista med kunder.
                Response.RedirectToRoute("Contact");
            }
        }
    }
}