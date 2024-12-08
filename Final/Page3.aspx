<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page3.aspx.cs" Inherits="Final.Page3" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rptItemList" runat="server">
        <ItemTemplate>
            <div class="item-row">
                <span><%# Eval("Name") %></span>
                <span>$<%# Eval("Price") %></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="total-price">
        TOTAL:<asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
    </div>
    <asp:Button ID="btnConfirm" runat="server" Text="CONFIRM" OnClick="btnConfirm_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="CANCEL" OnClick="btnCancel_Click" />
</asp:Content>
