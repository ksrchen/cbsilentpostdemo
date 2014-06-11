<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="cbsilentpostdemo._default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="/Content/payment.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script src="/Scripts/default.js" type="text/javascript"></script>
<%--    <script src="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/hmac-sha256.js"></script>
    <script src="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/enc-base64-min.js"></script>--%>
    <%--<script src="https://mpps.azurewebsites.net/scripts/mpps.min.js"></script>--%>
    <script src="http://localhost:54690/api/v1"></script>    
    <title></title>
</head>
<body>
    <div class="content" >
    <div>
        <a class="logo"></a>
        <a style="color:#929292;">#1 in Commercial Real Estate Online</a>
    </div>
    <ul id="panelBar" class="t-accordion">
    <li id="selectProduct">
        <div class="accordion-header">
        <span class="k-icon k-plus"><span class="circle">1</span><span>Select your product and subscription terms</span></span>
        </div>
        <div class="accordion-content" style="display:none">
        <div class="sectionContent">
         <%--<h1 class="section-title">Product</h1>--%>
            <div><input type="radio" name="Product" id="premiumSearcher" checked="checked" value="99"/>Premium Searcher - starting at $64.95/month</div>
            <div><input type="radio" name="Product" id="platinumSearcher" value="99"/>Platinum Searcher - starting at $179.95/month</div>
         <h1 class="section-title">Subscription terms</h1>
             <div><input type="radio" name="Terms" class="terms" checked="checked" value="64.95"/>Annual Subscription - $64.95/month</div>
            <div><input type="radio" name="Terms" class="terms"" value="79.95"/>Quarterly Subscription - $79.95/month</div>
            <div><input type="radio" name="Terms" class="terms" value="89.95"/>Monthly Subscription - $89.95/month</div>
        
       <input type="button" value="Continue" id="continue" />
        </div>
        </div>
    </li>        
        <li id="purchase">
        <div class="accordion-header">
        <span class="k-icon k-plus"><span class="circle">2</span><span>Enter your contact information</span></span>
        </div>
    <div class="accordion-content" style="display:none">
    <div class="sectionContent">
        <div id="message" class="message" ></div>
    <form id="contact_form" action="#" method="post">
        <div class="div-table">
        <div class="div-row">
        <div class="div-cell">
         <h1 class="section-title">Billing address</h1>
            <label class="fieldName">First Name:</label><input type="text" name="firstName" /><br/>
            <label class="fieldName">Last Name:</label><input type="text" name="lastName" /><br/>
            <label class="fieldName">Email:</label><input type="text" name="email" /><br/>
            <label class="fieldName">Phone:</label><input type="text" name="phone" /><br/>
            <label class="fieldName">Address Line1:</label><input type="text" name="addressLine1" /><br/>
            <label class="fieldName">Address Line2:</label><input type="text" name="addressLine2" /><br/>
            <label class="fieldName">City:</label><input type="text" name="city" /><br/>
            <label class="fieldName">State:</label><input type="text" name="state" /><br/>
            <label class="fieldName">Country:</label><input type="text" name="country" /><br/>
            <label class="fieldName">Postal Code:</label><input type="text" name="postalCode" /><br/>    
        </div>
        <div class="div-cell">
        <h1 class="section-title">Credit Card Information</h1>
         <div id="mpps-payment"></div>
        </div>
        </div>
        </div>
            <h1 class="section-title">Purchase summary</h1>
        <div >
            <label class="fieldName" id="Label1">Premium Searcher</label><input type="text" style="float:right" name="amount" id="productPrice" />
        </div>
           <input type="submit" id="completePurchase" value="Complete Purchase" />
        </form>
       </div>
       </div>
    </li>    
    </ul>
</div>
</body>
</html>

