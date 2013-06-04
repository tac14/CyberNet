<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventions.aspx.cs" Inherits="CyberNet.Game.Inventions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p  style="text-align:left; font-size:large; color: black;">
        Ввести новое действие:
    </p>        

    <table>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 10 очков физ. силы и 5 очков интеллекта</td>
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

    <table>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 25 очков физ. силы и 5 очков интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Название ресурса:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="RawName"  runat="server" Text="" Width="150"  />
            </td>
            <td style="padding: 5px;text-align:left;">Вид ресурса:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="CategoryType"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoryTypeChange"> </asp:DropDownList>
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
    <table>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит 25 очков интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Название продукта:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductName"  runat="server" Text="" Width="150"  />
            </td>
            <td style="padding: 5px;text-align:left;">Единица измерения (шт., кг., куб.м. и т.п.):</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductMeasurement"  runat="server" Text="" Width="100"  />
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Можно использовать в пищу:</td>
            <td style="padding: 5px;">
                <asp:CheckBox id="IsFood" runat="server"/>
            </td>
            <td style="padding: 5px;">
                <asp:Button id="AddProduct1" Text="Придумать" OnClick="AddProduct" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        

    <p  style="text-align:left; font-size:large; color: black;">
        Изобрести вариант изготовления продукта:
    </p>        
    <table>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; font-style:italic;">Стоит ? очков интеллекта</td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Выберите продукт:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ProductList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductListChange"> </asp:DropDownList>
            </td>
        </tr>
    </table>    
    <p></p>        

    <p  style="text-align:left; color: black;">
        Добавление простого действия:
    </p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое сырье нужно:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="DropDownList3"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="RawListChange1"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Сколько единиц:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="TextBox2"  runat="server" Text="1.00" Width="80" style="text-align:right;"  />
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое действие надо выполнить над сырьем:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ActionList2"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionListChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button1" Text="Добавить" OnClick="AddFastAction" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        
    <p  style="color: blue;">
        Список простых действий, которые нужны для изготовления продукта:
    </p>        
    <p></p>        
    <p  style="text-align:left; color: black;">
        Добавление сложного действия:
    </p>        
    <p></p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое действие надо выполнить над сырьем:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ActionList3"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionListChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button2" Text="Добавить" OnClick="AddIFAction" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        
    <p  style="text-align:left; color: black;">
        Добавление условия выполнения действия:
    </p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Выберите № действия:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionIFListChange"> </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое сырье нужно:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="DropDownList2"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="RawListChange2"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Сколько единиц:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="TextBox5"  runat="server" Text="1.00" Width="80" style="text-align:right;" />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button3" Text="Добавить" OnClick="AddIFAction2" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        
    <p  style="color: blue;">
        Список сложных действий с условиями, которые необходимы для выполнения этого действия:
    </p>        
    <p></p>        

</asp:Content>
