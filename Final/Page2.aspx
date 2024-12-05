<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="Final.Page2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CheckBoxList ID="cblTopping" runat="Server" AutoPostBack="true" CssClass="custom-checkbox-list">
        <asp:ListItem Text="<img src='pepperoni.jpg' alt='Pepperoni'/>" Value="Pepperoni" />
        <asp:ListItem>Mushrooms</asp:ListItem>
        <asp:ListItem>Green Olives</asp:ListItem>
        <asp:ListItem>Green Peppers</asp:ListItem>
        <asp:ListItem>Double Cheese</asp:ListItem>
    </asp:CheckBoxList>
</asp:Content>
