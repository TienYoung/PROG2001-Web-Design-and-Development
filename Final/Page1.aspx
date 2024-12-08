<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="Final.Page1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome! Please enter your name.</h2>
    <div>
        <p class="input-container">
            <asp:Label for="txtUserName" runat="server">Name:</asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:Button
                ID="btnConfirm"
                runat="server"
                Text="Confirm"
                OnClick="btnConfirm_Click"></asp:Button>
        </p>
        <p class="input-feedback">
            <asp:RequiredFieldValidator
                ID="rfvUserName"
                runat="server"
                ControlToValidate="txtUserName"
                ErrorMessage="Your name <b>cannot</b> be BLANK or contain only SPACE."
                ForeColor="Red"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="revUserName"
                runat="server"
                ControlToValidate="txtUserName"
                ErrorMessage="Your name <b>cannot</b> be BLANK and is comprised of alphabetic letters."
                ForeColor="Red"
                ValidationExpression="^(?!\s+$)[a-zA-Z ]+$">
            </asp:RegularExpressionValidator>
        </p>
    </div>
</asp:Content>
