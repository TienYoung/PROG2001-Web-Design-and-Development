<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="a06.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A-06 : JQuery and JSON based text editor</title>
</head>
<body>
    <form id="editorForm" runat="server">
        <div>
            <asp:TextBox ID="txtEditor" runat="server" TextMode="MultiLine" Rows="10" Columns="50"></asp:TextBox>
        </div>
    </form>
</body>
</html>
