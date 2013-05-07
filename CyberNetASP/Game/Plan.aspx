<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Plan.aspx.cs" Inherits="WebTest.Game.Plan" %>
<%@ Register TagPrefix="asp2" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
<script runat="server">
 
</script>
    
    <style type="text/css">
    /*Reorder List*/
.dragHandle {
      width:10px;
      height:15px;
      background-color:Red;
      background-image:url(images/bg-menu-main.png);
      cursor:move;
      border:outset thin white;
}
 
.callbackStyle {
      border:thin blue inset;       
}
 
.callbackStyle table {
      background-color:#5377A9;     
      color:Black;
}
 
 
.reorderListDemo li {
      list-style:none;
      margin:2px;
      background-image:url(images/bg_nav.gif);
      background-repeat:repeat-x;
      color:#FFF;
}
 
.dragVisualContainer li {
    list-style:none;
      background-image:url(images/bg_nav.gif);
      background-repeat:repeat-x;
      color:#FFF;
}
 
.reorderListDemo li a {color:#FFF !important; font-weight:bold;}
 
.reorderCue {
    background-color:green;
    border:thin dotted black;
    height:25px;                       
}
 
.itemArea {
      margin-left:15px;
      font-family:Arial, Verdana, sans-serif;
      font-size:1em;
      text-align:left;
}
 
 
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ObjectDataSource ID="Product1" runat="server"  
       SelectMethod="GetProductVariant" UpdateMethod="SaveList" TypeName="CyberNet.Product">  

        <UpdateParameters> 
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Position" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>

    <p align="left">
        Выбирите продукт, который хотите сделать: 
        <asp:DropDownList ID="ProductList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductChange">
        </asp:DropDownList>
    </p>
    <p align="left">
        Выбирите способ, которым хотите сделать: 
        <asp:DropDownList ID="OptionsList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="OptionsChange" >
        </asp:DropDownList>

<%--        <asp:DataList ID="Repeater1" runat="server"  DataSourceID="Product1">   
            <ItemTemplate>
			    <%# Eval("OperationID")%> - <%# Eval("Name")%> - <%# Eval("ActionName")%>
            </ItemTemplate>
        </asp:DataList>  --%>
    </p>

       <asp2:ToolkitScriptManager ID="ToolkitScriptManager1"   runat="server">
        </asp2:ToolkitScriptManager>

<%--- <%# Eval("OperationID")%> - <%# Eval("Name")%> --%>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="reorderListDemo">
                   <asp2:ReorderList ID="ReorderList1" runat="server" 
                        CallbackCssStyle="callbackStyle"
                        PostBackOnReorder="false"
                        DataSourceID="Product1"
                        DragHandleAlignment="Left" 
                        ItemInsertLocation="Beginning"
                        DataKeyField="ID" 
                        AllowReorder="true"
                        SortOrderField="Position"
                        >
                          <ItemTemplate>
                            <div class="itemArea">
                                <strong> <%# Eval("ID")%> - <%# Eval("Position")%> - <%# Eval("Name")%></strong>
                            </div>
                          </ItemTemplate>
                            <ReorderTemplate>
                                <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                            </ReorderTemplate>
                            <DragHandleTemplate>
                                <div class="dragHandle"></div>
                            </DragHandleTemplate>
                    </asp2:ReorderList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>



