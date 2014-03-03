<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="AventyrligaKontakter.Pages.ContactPages.Listing" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>hej</title>
</head>
<body>
    <form id="theForm" runat="server">
    <div id="page">
        <div id="main">
            <h1>
                Kontakter
            </h1>
            <asp:Panel runat="server" ID="MessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="MessageLiteral" />
            </asp:Panel>
            <div class="editor-field">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%$ RouteUrl:routename=ContactCreate %>' Text="Lägg till ny kund" />
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
                CssClass="validation-summary-errors" />
            <%-- 
                    Visar alla kontakter. Innehåller även kommandoknappar för att uppdatera och ta bort kontakter.
                    Hämtar alla kontaktuppgifter som finns i tabellen Contact i databasen via affärslogikklassen Service och 
                    metoden GetContact, som i sin tur använder klassen ContactDAL och metoden GetContact, som skapar en
                    lista med referenser till Contact-objekt; ett Contact-objekt för varje post i tabellen. 
            --%>
            <asp:ListView ID="ContactListView" runat="server"
                ItemType="AventyrligaKontakter.Model.Contact"
                SelectMethod="ContactListView_GetData"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactId">
                <LayoutTemplate>
                    <table class="grid">
                        <tr>
                            <th>
                                Förnamn
                            </th>
                            <th>
                                Efternamn
                            </th>
                            <th>
                               Email
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <%#: Item.FirstName %>
                        </td>
                        <td>
                            <%#: Item.LastName %>
                        </td>
                        <td>
                            <%#: Item.EmailAdress %>
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kunduppgifter. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då kunduppgifter saknas i databasen. --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Kontaktuppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="Name" runat="server" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Address" runat="server" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="PostalCode" runat="server" Text='<%# BindItem.EmailAdress %>' class="Email" />
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
        </div>
    </div>
    </form>
</body>
</html>