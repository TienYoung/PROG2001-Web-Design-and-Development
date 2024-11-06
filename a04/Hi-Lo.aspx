<%@ Page Title="Hi-Lo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hi-Lo.aspx.cs" Inherits="a04.Hi_Lo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%: Title %>.</h2>
        <div>
            <label for="userName">Please enter your name</label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <button>confirm</button>
        </div>
        <div>
            <asp:RequiredFieldValidator
                ID="rfvUserName"
                runat="server"
                ControlToValidate="txtUserName"
                ErrorMessage="Your name <b>cannot</b> be BLANK or contain only SPACE."
                ForeColor="Red"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>
    </main>
</asp:Content>
