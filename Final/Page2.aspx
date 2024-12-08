<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="Final.Page2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Image ImageUrl="images/pizza.jpg" AlternateText="Pizza" runat="server" CssClass="image-pizza" />
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:CheckBoxList ID="cblTopping" runat="Server"
                AutoPostBack="true" OnSelectedIndexChanged="cblTopping_SelectedIndexChanged"
                CssClass="checkbox-list-topping" RepeatDirection="Horizontal">
                <asp:ListItem Value="Pepperoni">
                    <img src='images/pepperoni.jpg' alt='Pepperoni'/>
                </asp:ListItem>
                <asp:ListItem Value="Mushrooms">
                    <img src='images/mushrooms.jpg' alt='Mushrooms'/>
                </asp:ListItem>
                <asp:ListItem Value="Green Olives">
                    <img src='images/green_olives.jpg' alt='Green Olives'/>
                </asp:ListItem>
                <asp:ListItem Value="Green Peppers">
                    <img src='images/green_peppers.jpg' alt='Green Peppers'/>
                </asp:ListItem>
                <asp:ListItem Value="Double Cheese">
                    <img src='images/double_cheese.jpg' alt='Double Cheese'/>
                </asp:ListItem>
            </asp:CheckBoxList>
            <asp:Label ID="lblPrice" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnMakeIt" runat="server" Text="Make It!" OnClick="btnMakeIt_Click" />
</asp:Content>
