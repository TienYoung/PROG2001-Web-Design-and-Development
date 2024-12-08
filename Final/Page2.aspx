<%@ Page Title="" Language="C#" MasterPageFile="~/PizzaShop.Master" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="Final.Page2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pizza">
        <asp:Image ImageUrl="images/pizza.jpg" AlternateText="Pizza" runat="server" />
        <p>
            <span>
                This delicious pizza comes with a perfectly baked crust,
                topped with our classic tomato sauce and melted cheese as the base.
                Create the perfect pizza tailored to your taste!
            </span>
            <span>
                Price: $ 10.00 (with sauce and cheese)
            </span>
        </p>
    </div>

    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:CheckBoxList ID="cblTopping" runat="Server"
                AutoPostBack="true" OnSelectedIndexChanged="cblTopping_SelectedIndexChanged"
                CssClass="checkbox-list-topping" RepeatDirection="Horizontal">
                <asp:ListItem Value="Pepperoni">
                    <img src='images/pepperoni.jpg' alt='Pepperoni'/>
                    <p>$ 1.50</p>
                </asp:ListItem>
                <asp:ListItem Value="Mushrooms">
                    <img src='images/mushrooms.jpg' alt='Mushrooms'/>
                    <p>$ 1.00</p>
                </asp:ListItem>
                <asp:ListItem Value="Green Olives">
                    <img src='images/green_olives.jpg' alt='Green Olives'/>
                    <p>$ 1.00</p>
                </asp:ListItem>
                <asp:ListItem Value="Green Peppers">
                    <img src='images/green_peppers.jpg' alt='Green Peppers'/>
                    <p>$ 1.00</p>
                </asp:ListItem>
                <asp:ListItem Value="Double Cheese">
                    <img src='images/double_cheese.jpg' alt='Double Cheese'/>
                    <p>$ 2.25</p>
                </asp:ListItem>
            </asp:CheckBoxList>
            <div class="make-it">
                <asp:Label for="btnMakeIt" ID="lblPrice" runat="server" />
                <asp:Button ID="btnMakeIt" runat="server" Text="Make It!" OnClick="btnMakeIt_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
