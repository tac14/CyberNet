<%@ Page Title="Киберсеть-Активные игроки" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActiveUsers.aspx.cs" Inherits="CyberNet.Game.ActiveUsersLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        

    <p  style="font-size:large; color: black;">
        Активные игроки <span style="color:blue;">*</span> :
    </p>        
    <span style="font-size:smaller;">
        <span style="color:blue;">*</span> - посление действие осуществленно не больше, чем 15 игровых дней назад.
    </span>

	    <div>
	        <asp:ListView ID="ActiveUsersList" runat="server" >
                <LayoutTemplate>
                <table id="stocktable" style="border: double 6px #A55129; margin-left: auto; margin-right: auto;" cellspacing="0" cellpadding="5" rules="all">
                    <thead>
                        <tr>
                            <th>Ник</th>
                            <th>Дата последнего действия</th>
                            <th>Город</th>
                            <th>Страна</th>
                        </tr>
                    </thead>
                    <tbody id="itemPlaceholder" runat="server"></tbody>
                </table>
            </LayoutTemplate>
                <ItemTemplate>
                    <tr <%# Container.DataItemIndex % 2 == 0 ? "" : " class=\"alt\"" %>>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<% #Eval("AgentName")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<% #Eval("LastActionDate")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<% #Eval("CityName")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text='<% #Eval("CountryName")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
	    </div>


</asp:Content>
