<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receipt.aspx.cs" Inherits="cbsilentpostdemo.receipt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <%--<script src="/Scripts/jquery-1.8.2.min.js"></script>--%>
</head>
<body>
</body>
    <script type='text/javascript'>       
       window.onload = function () {
            window.parent.window.postMessage({ type: 'response', response: <%= Newtonsoft.Json.JsonConvert.SerializeObject(Response) %> }, '*');
        };
</script>
</html>

