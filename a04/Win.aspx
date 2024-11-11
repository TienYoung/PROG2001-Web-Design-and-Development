<%@ Page Language="C#" MasterPageFile="~/Hi-Lo.Master" AutoEventWireup="true" CodeBehind="Win.aspx.cs" Inherits="a04.Win" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2 id="title"><%# "Congratulations " + UserName + ", You Win!!"%>.</h2>
        <div>
            <p>
                <asp:Button
                    ID="btnConfirm"
                    runat="server"
                    Text="Play Again"
                    OnClick="btnConfirm_Click"></asp:Button>
            </p>
        </div>
    </main>
</asp:Content>
