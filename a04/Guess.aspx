<%@ Page Title="Guess" Language="C#" MasterPageFile="~/Hi-Lo.Master" AutoEventWireup="true" CodeBehind="Guess.aspx.cs" Inherits="a04.Guess" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%: Title %>.</h2>
        <p>
            <asp:Label for="txtGuessNumber" runat="server">Guess a number(<%# Min + "~" + Max%>).</asp:Label>
            <asp:TextBox ID="txtGuessNumber" runat="server"></asp:TextBox>
            <asp:Button
                ID="btnConfirm"
                runat="server"
                Text="Make a guess"
                OnClick="btnConfirm_Click"></asp:Button>
        </p>
        <p>
            <asp:RangeValidator
                ID="rvGuessNumber"
                runat="server"
                ControlToValidate="txtGuessNumber"
                MinimumValue='<%# Min %>'
                MaximumValue='<%# Max %>'
                Type="Integer"
                ErrorMessage='<%# "The number <b>must</b> between " + Min + " and " + Max %>'
                ForeColor="Red"
                Display="Dynamic">
            </asp:RangeValidator>
            <asp:RequiredFieldValidator
                ID="rfvGuessNumber"
                runat="server"
                ControlToValidate="txtGuessNumber"
                ErrorMessage="The number <b>cannot</b> be EMPTY."
                ForeColor="Red"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </p>
    </main>
</asp:Content>
