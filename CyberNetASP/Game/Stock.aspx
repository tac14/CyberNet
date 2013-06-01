<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Stock.aspx.cs" Inherits="CyberNet.Game.StockLayout" %>
    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

        Ваши запасы:

	    <div>
	        <asp:ListView ID="StockList" runat="server" >
                <emptydatatemplate>
                    У вас еще ничего нет  
                </emptydatatemplate>
                <LayoutTemplate>
                <table id="stocktable">
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Качество</th>
                            <th>Количество</th>
                        </tr>
                    </thead>
                    <tbody id="itemPlaceholder" runat="server"></tbody>
                </table>
            </LayoutTemplate>
                <ItemTemplate>
                    <tr <%# Container.DataItemIndex % 2 == 0 ? "" : " class=\"alt\"" %>>
                        <td>
                            <asp:Label runat="server" Text='<% #Eval("ProductName")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<% #Eval("Quality")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<% #Eval("Count")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
	    </div>
</asp:Content>