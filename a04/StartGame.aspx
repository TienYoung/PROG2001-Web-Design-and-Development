<%@ Page Title="Start Game" Language="C#" MasterPageFile="~/Hi-Lo.Master" AutoEventWireup="true" CodeBehind="StartGame.aspx.cs" Inherits="a04.Hi_Lo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%: Title %>.</h2>
        <p>
            <asp:Label for="txtUserName" runat="server">Please enter your name</asp:Label>
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
    </main>
</asp:Content>
