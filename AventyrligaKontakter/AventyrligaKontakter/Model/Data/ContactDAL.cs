using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AventyrligaKontakter.Model;

namespace AventyrligaKontakter.Data
{
    public class ContactDAL : DALBase
    {

        private static readonly List<Contact> _spellingLetters;

       /* static ContactDAL()
        {
            _spellingLetters = new List<Contact>
            {
                new Contact{ Letter = 'A', Swedish = "Adam"},    new Contact{ Letter = 'B', Swedish = "Bertil"}, 
                new Contact{ Letter = 'C', Swedish = "Cesar"},   new Contact{ Letter = 'D', Swedish = "David"}, 
                new Contact{ Letter = 'E', Swedish = "Erik"},    new Contact{ Letter = 'F', Swedish = "Filip"}, 
                new Contact{ Letter = 'G', Swedish = "Gustav"},  new Contact{ Letter = 'H', Swedish = "Helge"},
                new Contact{ Letter = 'I', Swedish = "Ivar"},    new Contact{ Letter = 'J', Swedish = "Johan"}, 
                new Contact{ Letter = 'K', Swedish = "Kalle"},   new Contact{ Letter = 'L', Swedish = "Ludvig"}, 
                new Contact{ Letter = 'M', Swedish = "Martin"},  new Contact{ Letter = 'N', Swedish = "Niklas"}, 
                new Contact{ Letter = 'O', Swedish = "Olof"},    new Contact{ Letter = 'P', Swedish = "Petter"}, 
                new Contact{ Letter = 'Q', Swedish = "Qvintus"}, new Contact{ Letter = 'R', Swedish = "Rudolf"},
                new Contact{ Letter = 'S', Swedish = "Sigurd"},  new Contact{ Letter = 'T', Swedish = "Tore"}, 
                new Contact{ Letter = 'U', Swedish = "Urban"},   new Contact{ Letter = 'V', Swedish = "Viktor"}, 
                new Contact{ Letter = 'W', Swedish = "Wilhelm"}, new Contact{ Letter = 'X', Swedish = "Xerxes"},
                new Contact{ Letter = 'Y', Swedish = "Yngve"},   new Contact{ Letter = 'Z', Swedish = "Zäta"}, 
                new Contact{ Letter = 'Å', Swedish = "Åke"},     new Contact{ Letter = 'Ä', Swedish = "Ärlig"}, 
                new Contact{ Letter = 'Ö', Swedish = "Östen"}
            };
        }
        */
        

        //metoder
        public void DeleteContacts(int contactID)
        {


            using (var conn = CreateConnection())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch
                {

                    throw new ApplicationException("An error occured while getting contact from the database.");
                }

            }
        }

        public Contact GetContactById(int contactid)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver. Använder här det MINDRE effektiva 
                    // sätttet att göra det på - enkelt, men ASP.NET behöver "jobba" rätt mycket.
                    cmd.Parameters.AddWithValue("@ContactId", contactid);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returner en post varför
                    // ett SqlDataReader-objekt måste ta hand om posten. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Om det finns en post att läsa returnerar Read true. Finns ingen post returnerar
                        // Read false.
                        if (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har. Genom att använda 
                            // GetOrdinal behöver du inte känna till i vilken ordning de olika 
                            // kolumnerna kommer, bara vad de heter.
                            int ContactIdIndex = reader.GetOrdinal("ContactId");
                            int FirstNameIndex = reader.GetOrdinal("FirstName");
                            int LastNameIndex = reader.GetOrdinal("LastName");
                            int EmailIndex = reader.GetOrdinal("EmailAddress");
                            

                            // Returnerar referensen till de skapade Contact-objektet.
                            return new Contact
                            {
                                ContactId = reader.GetInt32(ContactIdIndex),
                                FirstName = reader.GetString(FirstNameIndex),
                                LastName = reader.GetString(LastNameIndex),
                                EmailAdress = reader.GetString(EmailIndex),
                                
                            };
                        }
                    }

                    // Istället för att returnera null kan du välja att kasta ett undatag om du 
                    // inte får "träff" på en kund. I denna applikation väljer jag att *inte* betrakta 
                    // det som ett fel i detta lager om det inte går att hitta en kund. Vad du väljer 
                    // är en smaksak...
                    return null;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }


        public IEnumerable<Contact> GetContacts()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                    var contacts = new List<Contact>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                        // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                        // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var FirstNameIndex = reader.GetOrdinal("FirstName");
                        var LastNameIndex = reader.GetOrdinal("LastName");
                        var EmailIndex = reader.GetOrdinal("EmailAddress");
                      

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(FirstNameIndex),
                                LastName = reader.GetString(LastNameIndex),
                                EmailAdress = reader.GetString(EmailIndex),
                               
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    contacts.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting contacts from the database.");
                }
            }
        }
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
                using (var conn = CreateConnection())
                {
                    try
                    {
                        // Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                        var contacts = new List<Contact>(100);

                        // Skapar och initierar ett SqlCommand-objekt som används till att 
                        // exekveras specifierad lagrad procedur.
                        var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        var uprows = startRowIndex / maximumRows + 1;
                        cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = uprows;
                        cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                        // Öppnar anslutningen till databasen.
                        conn.Open();

                        // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                        // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                        // SqlDataReader-objekt och returnerar en referens till objektet.
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                            // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                            // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                            var contactIdIndex = reader.GetOrdinal("ContactId");
                            var FirstNameIndex = reader.GetOrdinal("FirstName");
                            var LastNameIndex = reader.GetOrdinal("LastName");
                            var EmailIndex = reader.GetOrdinal("EmailAddress");


                            // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                            // poster returnerar Read false.
                            while (reader.Read())
                            {
                                // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                                // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                                contacts.Add(new Contact
                                {
                                    ContactId = reader.GetInt32(contactIdIndex),
                                    FirstName = reader.GetString(FirstNameIndex),
                                    LastName = reader.GetString(LastNameIndex),
                                    EmailAdress = reader.GetString(EmailIndex),

                                });
                            }
                        }
                        totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                        // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                        return contacts;
                        
                        
                    }
                    catch
                    {
                        throw new ApplicationException("An error occured while getting contacts from the database.");
                    }
                }
            }
        

        public void InsertContact(Contact contact)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAdress;
                    

                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    contact.ContactId = (int)cmd.Parameters["@ContactId"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void UpdateContact(Contact contact)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAdress;


                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Value = contact.ContactId;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    contact.ContactId = (int)cmd.Parameters["@ContactId"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

    }
}