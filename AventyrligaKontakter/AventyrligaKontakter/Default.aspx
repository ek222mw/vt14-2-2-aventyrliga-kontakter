<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AventyrligaKontakter.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Äventyrliga Kontakter</title>
    <link href="Content/Style.css" rel="stylesheet" />
</head>
<body>
            <form id="theForm" runat="server">
            <h1>
                Kontakter
            </h1>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
                CssClass="validation-summary-errors" ValidationGroup="Update" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Insert"/>
            <%-- 
                    Visar alla kontakter. Innehåller även kommandoknappar för att lägga till, uppdatera och ta bort kontakter.
                    Hämtar alla kunduppgifter som finns i tabellen Contact i databasen via affärslogikklassen Service och 
                    metoden GetContacts, som i sin tur använder klassen ContactDAL och metoden GetContacts, som skapar en
                    lista med referenser till Contact-objekt; ett Contact-objekt för varje post i tabellen. 
            --%>
            <asp:ListView ID="ContactListView" runat="server"
                ItemType="AventyrligaKontakter.Model.Contact"
                SelectMethod="ContactListView_GetData"
                InsertMethod="ContactListView_InsertItem"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactId"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table class="grid">
                        <tr>
                            <th>
                                Firstname
                            </th>
                            <th>
                                Lastname
                            </th>
                            <th>
                                Email
                            </th>
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="5">
                                <Fields>
                                    <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowPreviousPageButton="False" ShowNextPageButton="False" ShowLastPageButton="False" FirstPageText="First" LastPageText="Last"/>
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ShowLastPageButton="True"  ShowPreviousPageButton="False" ShowNextPageButton="False" ShowFirstPageButton="False" LastPageText="Last" FirstPageText="First" />  
                                </Fields>
                            </asp:DataPager>
                            
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text=" <%#: Item.FirstName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="<%#: Item.LastName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="<%#: Item.EmailAdress %>"></asp:Label>  
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kontaktuppgifter. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då kontaktuppgifter saknas i databasen. --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Kontaktuppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kontaktuppgifter. Visas bara om InsertItemPosition 
                     har värdet FirstItemPosition eller LasItemPosition.--%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# BindItem.FirstName %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fältet tomt,mata in ett förnamn" ControlToValidate="FirstNameTextBox" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            
                            <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# BindItem.LastName %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="fältet tomt,mata in ett efternamn" ControlToValidate="LastNameTextBox" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# BindItem.EmailAdress %>' class="Email" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Fältet tomt,mata in en e-post" ControlToValidate="EmailTextBox" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fel inmatning, mata in en e-post adress" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Insert"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att lägga till en ny kontaktuppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kontaktuppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName1TextBox" runat="server" Text='<%# BindItem.FirstName %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Fältet tomt,mata in ett förnamn" ControlToValidate="FirstName1TextBox" ValidationGroup="Update"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="LastName1TextBox" runat="server" Text='<%# BindItem.LastName %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="fältet tomt,mata in ett efternamn" ControlToValidate="LastName1TextBox" ValidationGroup="Update"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Email1TextBox" runat="server" Text='<%# BindItem.EmailAdress %>' class="Email" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Fältet tomt,mata in en e-post" ControlToValidate="Email1TextBox" ValidationGroup="Update"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fel inmatning, mata in en e-post adress" ControlToValidate="Email1TextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Update"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kontaktuppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
            </form>
</body>
</html>