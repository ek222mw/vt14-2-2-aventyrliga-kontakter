<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="AventyrligaKontakter.Pages.ContactPages.Create" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skapa Kontakt</title>
    <link href="../../Content/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="theForm" runat="server">
    <div id="page">
        <div id="main">
            <h1>
                Ny kund
            </h1>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors" />
            <asp:FormView ID="CustomerFormView" runat="server"
                ItemType="AventyrligaKontakter.Model.Contact"
                DefaultMode="Insert"
                RenderOuterTable="false"
                InsertMethod="ContactFormView_InsertItem"
                OnItemCommand="ContactFormView_ItemCommand">
                <InsertItemTemplate>
                    <div class="editor-label">
                        <label for="FirstName">Förnamn</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# BindItem.FirstName %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Fältet tomt,mata in ett förnamn." ControlToValidate="FirstNameTextBox"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Fel inmatning, mata in ett förnamn" ControlToValidate="FirstNameTextBox"></asp:CompareValidator>
                    </div>
                    <div class="editor-label">
                        <label for="LastName">Efternamn</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="EmailAdress" runat="server" Text='<%# BindItem.EmailAdress %>' />
                    </div>
                    <div>
                        <asp:Button ID="Button1" runat="server" Text="Spara" CommandName="Insert" />
                        <asp:Button ID="Button2" runat="server" Text="Avbryt" CommandName="Cancel" />
                    </div>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </div>
    </form>
</body>
</html>