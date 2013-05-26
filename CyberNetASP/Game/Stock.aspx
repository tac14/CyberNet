<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Stock.aspx.cs" Inherits="CyberNet.Game.StockLayout" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <asp:ObjectDataSource ID="StockState" runat="server"  
           SelectMethod="GetStock" TypeName="CyberNet.Stock">  
        </asp:ObjectDataSource>  

        Ваши запасы:

	    <div id="stocktable">
                <asp:DataList ID="Repeater1" runat="server"  DataSourceID="StockState" style="margin-left: auto; margin-right: auto;">   
                    <ItemTemplate>
                    <%# Eval("ProductName")%> - <%# Eval("Count")%> - <%# Eval("Quality")%> 
                    </ItemTemplate>
                </asp:DataList>  
	    </div>	

</asp:Content>