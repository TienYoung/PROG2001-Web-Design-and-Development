<%@ Page Title="Game Setting" Language="C#" MasterPageFile="~/Hi-Lo.Master" AutoEventWireup="true" CodeBehind="SetMax.aspx.cs" Inherits="a04.SetGame" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%: Title %>.</h2>
        <p>
            <asp:Label for="txtMaxNumber" runat="server">Set a max number(1~999)</asp:Label>
            <asp:TextBox ID="txtMaxNumber" runat="server"></asp:TextBox>
            <asp:Button
                ID="btnConfirm"
                runat="server"
                Text="Confirm" 
                OnClick="btnConfirm_Click"></asp:Button>
        </p>
        <p>
            <asp:RangeValidator
                ID="rvMaxNumber"
                runat="server"
                ControlToValidate="txtMaxNumber"
                MinimumValue="1"
                MaximumValue="999"
                Type="Integer"
                ErrorMessage="The number <b>must</b> between 1 and 999."
                ForeColor="Red"
                Display="Dynamic">
            </asp:RangeValidator>
            <asp:RequiredFieldValidator
                ID="rfvMaxNumber"
                runat="server"
                ControlToValidate="txtMaxNumber"
                ErrorMessage="The number <b>cannot</b> be EMPTY."
                ForeColor="Red"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </p>
    </main>
</asp:Content>
