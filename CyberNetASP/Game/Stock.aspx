<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="CyberNet.Game.Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="/Scripts/underscore.js"></script>
    <script type="text/javascript" src="/Scripts/handlebars.js"></script>
    <script type="text/javascript" src="/Scripts/backbone.js"></script>
    <script type="text/javascript" src="/Scripts/stock.js"></script>
</head>
<body>
    <script type="text/template" id="list">
			<td> {{ name }}</td> <td>{{ count }}</td> <td>{{ quality }}</td>
    </script>

    <!-- Блок ошибки -->
    <script type="text/template" id="error">
			<p>Извините произошла ошибка при запросе данных. Попробуйте обновить страницу</p>
    </script>

    <div id="block" class="block"></div>

</body>
</html>
