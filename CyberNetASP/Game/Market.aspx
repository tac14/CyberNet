<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="CyberNet.Game.MarketLayout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


<style type="text/css">

    .st1
    {
        
    }
    .st2
    {
        padding:5px;
   }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ObjectDataSource ID="MarketState" runat="server"  
           SelectMethod="GetMarket" TypeName="CyberNet.Market">  
        </asp:ObjectDataSource>  

    <p  style="text-align:left; font-size:large; color: black;">
        Выставить новое предложение (лот):
    </p>        

    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Выберите продукт с вашего склада:</td>
            <td style="padding: 5px;">
                   <asp:DropDownList ID="ProductListFromStock"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductFromStockChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Введите кол-во, которое хотите обменять:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="TextBox1"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />

            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button1" Text="Добавить" OnClick="AddLot" runat="server" />
            </td>
        </tr>
    </table>    

    <p></p>

    <p  style="text-align:left; font-size:large; color: black;">
        Добавить вариант обмена:
    </p>        

        <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Выберите номер лота:</td>
            <td style="padding: 5px;">
                   <asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="LotChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">Выберите продукт:</td>
            <td style="padding: 5px;">
                   <asp:DropDownList ID="DropDownList2"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductChange"> </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Введите минимальное кол-во:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="TextBox2"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />
            </td>
            <td style="padding: 5px;text-align:left;">Введите минимальное качество:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="TextBox3"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button2" Text="Добавить" OnClick="AddLotVariant" runat="server" />
            </td>
        </tr>
    </table>    


    <p></p>

    <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
        Вами выставленные предложения:
    </span>
        <asp:ListView ID="ListView2" runat="server" DataSourceID="MarketState">
           <LayoutTemplate>
              <table style="border: double 6px #A55129; margin-left: auto; margin-right: auto;" cellspacing="0" cellpadding="5" rules="all">
                 <tr style="background-color: #A55129; color: Black;">
                    <th style="padding:5px;"></th>
                    <th style="padding:5px;">Название продукта</th>
                    <th style="padding:5px;">Количество</th>
                    <th style="padding:5px;">Качество</th>
                    <th style="padding:5px;">Лот №</th>
                 </tr>
                 <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                 </tbody>
              </table>
           </LayoutTemplate>
           <ItemTemplate>
              <tr runat="server" visible=<%# Eval("Column1")%>>
                 <td style="padding-left: 5px; padding-right: 50px; text-align: left; color: Blue; font-size:smaller;"><%# Eval("Column3")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; font-size: medium;"><%# Eval("Column4")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column7")%></td>
              </tr>
              <tr id="Tr1" runat="server" visible=<%# Eval("Column2")%>>
                 <td style="padding-left: 50px; padding-right: 5px; text-align: right; color: Blue;  font-size:smaller;"><%# Eval("Column3")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column4")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"></td>
              </tr>
           </ItemTemplate>
        </asp:ListView>
    

</asp:Content>
