<%@ Page Title="Киберсеть-Рынок" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllMarket.aspx.cs" Inherits="CyberNet.Game.AllMarket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ObjectDataSource ID="MarketState" runat="server"  
        SelectMethod="GetAllMarket" TypeName="CyberNet.Market">  
    </asp:ObjectDataSource>  

    <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
        Текущие предложения на рынке:
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
                    <th style="padding:5px;">Выставил</th>
                 </tr>
                 <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                 </tbody>
              </table>
           </LayoutTemplate>
           <ItemTemplate>
              <tr id="Tr1" runat="server" visible=<%# Eval("Column1")%>>
                 <td style="padding-left: 5px; padding-right: 50px; text-align: left; color: Blue; font-size:smaller;">
                     <%# Eval("Column3")%>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; font-size: medium;"><%# Eval("Column4")%></td>

                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;" runat="server" visible= <%# Eval("Column9")%> ><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center; color: red;" runat="server" visible= <%# Eval("Column10")%> ><%# Eval("Column5")%></td>

                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column7")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: center;"><%# Eval("Column8")%></td>
              </tr>
              <tr id="Tr2" runat="server" visible=<%# Eval("Column2")%>>
                 <td style="padding-left: 50px; padding-right: 5px; text-align: right; color: Blue;  font-size:smaller;">
                     <%# Eval("Column3")%>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column4")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column5")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller;"><%# Eval("Column6")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller; color: gray;"></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size:smaller; color: gray;"></td>
              </tr>
           </ItemTemplate>
        </asp:ListView>

</asp:Content>
