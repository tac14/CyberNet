<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventions2.aspx.cs" Inherits="CyberNet.Game.Inventions2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ObjectDataSource ID="FastActionList" runat="server"  
        SelectMethod="GetFastAction" TypeName="CyberNet.Inventions">  
    </asp:ObjectDataSource>  
    <asp:ObjectDataSource ID="IfActionList" runat="server"  
        SelectMethod="GetIfAction" TypeName="CyberNet.Inventions">  
    </asp:ObjectDataSource>  

    <asp:HiddenField ID="ConfirmResult" runat="server" />

    <p  style="text-align:left; font-size:large; color: black;">
        Изобрести вариант изготовления продукта:
    </p>        
    <table>
        <tr>
            <td style="padding: 2px; text-align:left; font-size:smaller; font-style:italic; color: brown;">
                Чтобы патентное бюро приняло на рассмотрение ваше изобретение Вы потратите 
                <asp:Label style="color: red; font-size:medium; font-style:oblique; font-weight:bold;" ID="Cost" runat="server" Text="0"></asp:Label>
                интеллектуальных очков.
            </td>
        </tr>
        <tr>
            <td style="text-align:left; font-size:smaller; font-style:italic;">
                Если изобретение будет признанно оригинальным и полезным Вы сможете получить лицензию.
            </td>
        </tr>
        <tr>
            <td style="text-align:left; font-size:smaller; font-style:italic;">
                Патентное бюро оставляет за собой право в односторонем порядке корректировать ваше предложение.
            </td>
        </tr>
        <tr>
            <td style="text-align:left; font-size:smaller; font-style:italic;">
                Выдача лицензии не гарантируется и назад интеллектуальные очки не возвращаются.
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red;" > 
                <asp:Label ID="Error1" runat="server" visible="false" Text="У Вас нехватает требуемых очков интеллекта"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red;" > 
                <asp:Label ID="Error2" runat="server" visible="false" Text="Вы уже запрашивали лицензию на этот продукт"></asp:Label>
            </td>
        </tr>
    </table>    
    <p></p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left; color: black; font-size:medium;">Выберите продукт:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ProductList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductListChange"> </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left; color: black; font-size:medium;">Выберите тип лицензирования:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="LicenseTypeList"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="LicenseTypeChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;">
                <asp:Button id="RequestLicense1" style="padding: 5px; background-color:blue; color:white; font-weight:bold;" 
                    Text="Запросить лицензию" OnClick="RequestLicense" runat="server"
                    OnClientClick="return confirm('Вы уверены, что закончили работу над Вашим изобретением?')" />
            </td>
            <td style="padding: 5px; text-align:left; font-size:smaller; color: red; font-weight:bold;" > 
                <asp:Label ID="LicenseStatus" runat="server" visible="false" Text=""></asp:Label>
            </td>
        </tr>
    </table>    
    <p></p>        

    <p  style="text-align:left; color: black;">
        Добавление простого действия:
    </p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое действие надо выполнить над сырьем:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ActionList2"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionListChange"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Это сложное действие (есть условия выполнения):</td>
            <td style="padding: 5px;">
                <asp:CheckBox id="IsIfAction" runat="server" AutoPostBack="True" OnCheckedChanged="OnCheckFastAction"/>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое сырье нужно:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ProductList2"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="RawListChange1"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Сколько единиц:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="CountIndex1"  runat="server" Text="1.00" Width="80" style="text-align:right;"  />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button1" style="padding: 5px;" Text="Добавить" OnClick="AddFastAction" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        
    <p  style="color: blue;">
        Список простых действий, которые нужны для изготовления продукта:
    </p>        
        <asp:ListView ID="FastActionList1" runat="server" DataSourceID="FastActionList">
           <LayoutTemplate>
              <table style="border: double 6px #A55129; margin-left: auto; margin-right: auto;" cellspacing="0" cellpadding="5" rules="all">
                 <tr style="background-color: #A55129; color: Black;">
                    <th style="padding:5px;"></th>
                    <th style="padding:5px;">Получаемый продукт</th>
                    <th style="padding:5px;">Нужно осуществить действие</th>
                    <th style="padding:5px;">Необходимо сырье</th>
                    <th style="padding:5px;">Норма кол-ва</th>
                    <th style="padding:5px;">№ действия</th>
                 </tr>
                 <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                 </tbody>
              </table>
           </LayoutTemplate>
           <ItemTemplate>
              <tr>
                 <td style="padding-left: 5px; padding-right: 50px; text-align: left; color: Blue; font-size:smaller;">
                     <asp:CheckBox id="CheckFastAction" runat="server"/>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; font-size: medium;"><%# Eval("Column1")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; "><%# Eval("Column2")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; "><%# Eval("Column3")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column4")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right; font-size: smaller;">
                     <asp:Label ID="N1" runat="server" Text=<%# Eval("Column5")%>></asp:Label>
                 </td>
              </tr>
           </ItemTemplate>
        </asp:ListView>
        <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
            <asp:Button id="Button4" style="padding: 5px;" Text="Удалить выбранные действия" OnClick="DeleteFastActions" runat="server"/>
        </span>

    <p></p>        
    <p  style="text-align:left; color: black;">
        Добавление условия выполнения действия:
    </p>        
    <table>
        <tr>
            <td style="padding: 5px;text-align:left;">Выберите № действия:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="IfActionsNumberList1"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActionIFListChange"> </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;text-align:left;">Какое сырье нужно:</td>
            <td style="padding: 5px;">
                <asp:DropDownList ID="ProductList3"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="RawListChange2"> </asp:DropDownList>
            </td>
            <td style="padding: 5px;text-align:left;">Сколько единиц:</td>
            <td style="padding: 5px;">
                <asp:TextBox ID="CountIndex2"  runat="server" Text="1.00" Width="80" style="text-align:right;" />
            </td>
            <td style="padding: 5px;">
                <asp:Button id="Button3" style="padding: 5px;" Text="Добавить" OnClick="AddIFAction" runat="server" />
            </td>
        </tr>
    </table>    
    <p></p>        
    <p  style="color: blue;">
        Список сложных действий с условиями, которые необходимы для выполнения этого действия:
    </p>        
        <asp:ListView ID="IfActionList1" runat="server" DataSourceID="IfActionList">
           <LayoutTemplate>
              <table style="border: double 6px #A55129; margin-left: auto; margin-right: auto;" cellspacing="0" cellpadding="5" rules="all">
                 <tr style="background-color: #A55129; color: Black;">
                    <th style="padding:5px;"></th>
                    <th style="padding:5px;">Для действия №</th>
                    <th style="padding:5px;">Необходимо сырье</th>
                    <th style="padding:5px;">Норма кол-ва</th>
                 </tr>
                 <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                 </tbody>
              </table>
           </LayoutTemplate>
           <ItemTemplate>
              <tr>
                 <td style="padding-left: 5px; padding-right: 50px; text-align: left; color: Blue; font-size:smaller;">
                     <asp:CheckBox id="CheckIfAction" runat="server"/>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; ">
                     <asp:Label ID="N1" runat="server" Text=<%# Eval("Column1")%>></asp:Label>
                 </td>
                 <td style="padding-left: 5px; padding-right: 5px; "><%# Eval("Column2")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; text-align: right;"><%# Eval("Column3")%></td>
                 <td style="padding-left: 5px; padding-right: 5px; " runat="server" visible="false">
                     <asp:Label ID="N2" runat="server" Text=<%# Eval("Column4")%>></asp:Label>
                 </td>
                 <td id="Td1" style="padding-left: 5px; padding-right: 5px; " runat="server" visible="false">
                     <asp:Label ID="N3" runat="server" Text=<%# Eval("Column5")%>></asp:Label>
                 </td>
              </tr>
           </ItemTemplate>
        </asp:ListView>
        <span  style="margin-left: auto; margin-right: auto; font-size:large; color: black;">
            <asp:Button id="Button2" style="padding: 5px;" Text="Удалить выбранные условия" OnClick="DeleteIfActions" runat="server"/>
        </span>
    <p></p>        

</asp:Content>
