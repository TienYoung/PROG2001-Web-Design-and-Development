<%@ Page Title="Win" Language="C#" MasterPageFile="~/Hi-Lo.Master" AutoEventWireup="true" CodeBehind="Win.aspx.cs" Inherits="a04.Win" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%# "Congratulations " + UserName + ", You Win!! You guessed the number!!"%>.</h2>
        <p>
            <asp:Button
                ID="btnConfirm"
                runat="server"
                Text="Play Again" 
                OnClick="btnConfirm_Click"></asp:Button>
        </p>
    </main>
</asp:Content>
