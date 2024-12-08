<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page3.aspx.cs" Inherits="Final.Page3" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="item-list">
        <asp:Repeater ID="rptItemList" runat="server">
            <ItemTemplate>
                <div class="item-row">
                    <span><%# Eval("Name") %></span>
                    <span>$<%# Eval("Price") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblTotalPrice" runat="server" CssClass="total-price" />
    </div>
    <div class="buttons">
        <asp:Button ID="btnConfirm" runat="server" Text="CONFIRM" OnClick="btnConfirm_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" OnClick="btnCancel_Click" />
    </div>
</asp:Content>
