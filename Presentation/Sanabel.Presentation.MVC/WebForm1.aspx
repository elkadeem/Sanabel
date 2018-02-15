<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Sanabel.Presentation.MVC.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <% foreach (System.Security.Claims.Claim claim in (HttpContext.Current.User.Identity)
                {%>
                <div><%= claim %></div>
            <%} %>
        </div>
    </form>
</body>
</html>
