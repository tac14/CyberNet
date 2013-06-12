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
            <selectparameters>
                <asp:querystringparameter name="argAgentName" querystringfield="AgentName" defaultvalue="" />
            </selectparameters>
        </asp:ObjectDataSource>  
        <asp:ObjectDataSource ID="ExchangeVariant" runat="server"  
               SelectMethod="GetExchangeVariant" TypeName="CyberNet.Market">  
            <selectparameters>
                <asp:querystringparameter name="argAgentName" querystringfield="AgentName" defaultvalue="" />
            </selectparameters>
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
                <asp:TextBox ID="ProductCount"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />

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
                   <asp:DropDownList ID="LotList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="LotChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">Выберите продукт:</td>
            <td style="padding: 5px;">
                   <asp:DropDownList ID="ProductList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductChange"> </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Введите минимальное кол-во:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductMinCount"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />
            </td>
            <td style="padding: 5px;text-align:left;">Введите минимальное качество:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="ProductMinQuality"  runat="server" Text="0.0000" Width="60" Style="text-align: right" />
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
        <asp:ListView ID="MarketList" runat="server" DataSourceID="MarketState">
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
              <tr runat="server" visible=<%# Eval("Column1")%> >
                 <td style="padding-left: 5px; padding-right: 50px; text-align: left; color: Blue; font-size:smaller;">
                     <asp:CheckBox id="CheckLot" runat="server"/> <%# Eval("Column3")%>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; font-size: medium;"><%# Eval("Column4")%></td>

                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;" runat="server" visible= <%# Eval("Column9")%> ><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center; color: red;" runat="server" visible= <%# Eval("Column10")%> ><%# Eval("Column5")%></td>

                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;">
                     <asp:Label ID="N1" runat="server" visible=<%# Eval("Column1")%> Text=<%# Eval("Column7")%>></asp:Label>
                 </td>
              </tr>
              <tr id="Tr1" runat="server" visible=<%# Eval("Column2")%>>
                 <td style="padding-left: 50px; padding-right: 5px; text-align: right; color: Blue;  font-size:smaller;">
                     <asp:CheckBox id="CheckLotVariant" runat="server"/> <%# Eval("Column3")%>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column4")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller; color: gray;">
                     <asp:Label ID="N2" runat="server" visible=<%# Eval("Column2")%> Text=<%# Eval("Column7")%>></asp:Label>
                 </td>
              </tr>
           </ItemTemplate>
        </asp:ListView>
        <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
            <asp:Button id="Button3" Text="Удалить выбранные рыночные предложения" OnClick="Delete" runat="server"/>
        </span>
        <p></p>

     <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
        Доступные варианты обмена:
    </span>
    <table runat="server" visible="false" id="DealLimit">
        <tr>
            <td style="padding: 2px; text-align:left; color: red;">
                При осуществлении одной или более сделок Вами или вашим партнером превышен лимит числа сделок. За 6 игровых часов можно осуществить не более 10 сделок.
            </td>
        </tr>
    </table>
        <asp:ListView ID="ExchangeList" runat="server" DataSourceID="ExchangeVariant">
           <LayoutTemplate>
              <table style="border: double 6px #A55129; margin-left: auto; margin-right: auto;" cellspacing="0" cellpadding="5" rules="all">
                 <tr style="background-color: #A55129; color: Black;">
                    <th style="padding:5px;">Лот №</th>
                    <th style="padding:5px;">Владелец</th>
                    <th style="padding:5px;">Название продукта</th>
                    <th style="padding:5px;">Количество</th>
                    <th style="padding:5px;">Качество</th>
                    <th style="padding:5px;">&#8660;</th>
                    <th style="padding:5px;">Лот №</th>
                    <th style="padding:5px;">Владелец</th>
                    <th style="padding:5px;">Название продукта</th>
                    <th style="padding:5px;">Количество</th>
                    <th style="padding:5px;">Качество</th>
                 </tr>
                 <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                 </tbody>
              </table>
           </LayoutTemplate>
           <ItemTemplate>
              <tr id="Tr2">
                 <td id="Td1" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                    <asp:Label ID="ID1" runat="server" Text=<%# Eval("Column1")%>></asp:Label>
                 </td>
                 <td id="Td3" style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column2")%></td>
                 <td id="Td4" style="padding-left: 5px; padding-right: 5px; text-align: left;"><%# Eval("Column3")%></td>
                 <td id="Td5" style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column4")%></td>
                 <td id="Td6" style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column5")%></td>

                 <td style="padding-left: 5px; padding-right: 5px; text-align: center; ">
                     <asp:CheckBox id="CheckExchange" runat="server"/> 
                 </td>
                 <td id="Td2" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                    <asp:Label ID="ID2" runat="server" Text=<%# Eval("Column6")%>></asp:Label>
                    <asp:Label ID="eID2" runat="server" Visible="false" Text=<%# Eval("eID2")%>></asp:Label>
                 </td>
                 <td id="Td7" style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column7")%></td>
                 <td id="Td8" style="padding-left: 5px; padding-right: 5px; text-align: left;"><%# Eval("Column8")%></td>
                 <td id="Td9" style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column9")%></td>
                 <td id="Td10" style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column10")%></td>

              </tr>
           </ItemTemplate>
        </asp:ListView>
        <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
            <asp:Button id="Button4" Text="Осуществить обмен" OnClick="Exchange" runat="server"/>
        </span>
       

</asp:Content>
