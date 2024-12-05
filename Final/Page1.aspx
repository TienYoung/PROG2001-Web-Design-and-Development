<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="Final.Page1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label for="txtUserName" runat="server">User Name:</asp:Label>
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        <asp:Button
            ID="btnConfirm"
            runat="server"
            Text="Confirm"
            OnClick="btnConfirm_Click"></asp:Button>
    </p>
    <p>
        <asp:RequiredFieldValidator
            ID="rfvUserName"
            runat="server"
            ControlToValidate="txtUserName"
            ErrorMessage="Your name <b>cannot</b> be BLANK or contain only SPACE."
            ForeColor="Red"
            Display="Dynamic">
        </asp:RequiredFieldValidator>
    </p>
</asp:Content>
