<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="GeekCustomer.Pages.Shared.Error" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Serverfel</title>

</head>
<body>
    <form id="theForm" runat="server">
        <div role="main">
            <p>
                Vi är beklagar att ett fel inträffade och vi inte kunde hantera din förfrågan.
            </p>
            <p>
                <a id="A1" href='<%$ RouteUrl:routename=Contacts %>' runat="server">Tillbaka till listan med kontakter</a>
            </p>
        </div>
    </form>
</body>
</html>