<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="Final.Page2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:CheckBoxList ID="cblTopping" runat="Server" AutoPostBack="true" CssClass="custom-checkbox-list">
        <asp:ListItem Text="<img src='images/pepperoni.jpg' alt='Pepperoni'/>" Value="Pepperoni" />
        <asp:ListItem Text="<img src='images/mushrooms.jpg' alt='Mushrooms'/>" Value="Mushrooms"/>
        <asp:ListItem Text="<img src='images/green_olives.jpg' alt='Green Olives'/>" Value="Green Olives"/>
        <asp:ListItem Text="<img src='images/green_peppers.jpg' alt='Green Peppers'/>" Value="Green Peppers"/>
        <asp:ListItem Text="<img src='images/double_cheese.jpg' alt='Double Cheese'/>" Value="Double Cheese"/>
    </asp:CheckBoxList>
</asp:Content>
