<%@ Page Title="Киберсеть-Изобретения" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventions.aspx.cs" Inherits="CyberNet.Game.InventionsLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <p  style="text-align:left; font-size:large; color: black;">
        Ввести новое действие:
    </p>        

    <table runat="server" id="NewAction">
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 5 очков физ. силы и 2 очка интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red;" > 
                <asp:Label ID="Error1" runat="server" visible="false" Text="У Вас нехватает требуемых очков силы/интеллекта"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Название действия:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ActionName"  runat="server" Text="" Width="150"  />
            </td>
            <td style="padding: 5px;text-align:left;">Действие с более низким технологическим уровнем <span style="color:blue;">*</span>:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ActionList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionListChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">
                <asp:Button id="AddAction1" Text="Ввести" OnClick="AddAction" runat="server" />
            </td>
        </tr>
    </table>    
    <span style="font-size:smaller;">
        <span style="color:blue;">*</span> - нужно указывать, если новое действие является усовершенствованием 
            другого действия, например, "рубить" более технологическое действие, чем "повалить".
    </span>
    <p></p>        

    <p  style="text-align:left; font-size:large; color: black;">
        Открыть новый ресурс:
    </p>        

    <table runat="server" id="NewRaw">
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 10 очков физ. силы и 3 очка интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red;" > 
                <asp:Label ID="Error2" runat="server" visible="false" Text="У Вас нехватает требуемых очков силы/интеллекта"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Название ресурса:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="RawName"  runat="server" Text="" Width="150"  />
            </td>
            <td style="padding: 5px;text-align:left;">Единица измерения (шт., кг., куб.м. и т.п.):</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="RawMeasure"  runat="server" Text="" Width="100"  />
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Вид ресурса:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="CategoryType"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoryTypeChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Норма добычи за 6 часов:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="RawNorm"  runat="server" Text="1.0000" Width="80" style="text-align:right;"  />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="AddRaw1" Text="Открыть" OnClick="AddRaw" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        


    <p  style="text-align:left; font-size:large; color: black;">
        Придумать идею о новом продукте:
    </p>        
    <table  runat="server" id="NewProduct">
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 10 очков интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red;" > 
                <asp:Label ID="Error3" runat="server" visible="false" Text="У Вас нехватает требуемых очков интеллекта"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Название продукта:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductName"  runat="server" Text="" Width="150"  />
            </td>
            <td style="padding: 5px;text-align:left;">Единица измерения (шт., кг., куб.м. и т.п.):</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductMeasure"  runat="server" Text="" Width="100"  />
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Можно использовать в пищу:</td>
            <td style="padding: 5px;">
                <asp:CheckBox id="IsFood" runat="server"/>
            </td>
            <td style="padding: 5px;text-align:left;">Норма производства за 6 часов:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductNorm"  runat="server" Text="1.0000" Width="80" style="text-align:right;"  />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="AddProduct1" Text="Придумать" OnClick="AddProduct" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        


</asp:Content>
