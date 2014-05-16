<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="cbsilentpostdemo._default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="/Content/payment.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script src="/Scripts/default.js" type="text/javascript"></script>
    <script src="https://mpps.azurewebsites.net/scripts/mpps.min.js"></script>
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
    <form id="payment_form" action="https://testsecureacceptance.cybersource.com/silent/pay" method="post">
        <div class="div-table">
        <div class="div-row">
        <div class="div-cell">
         <h1 class="section-title">Billing address</h1>
            <input type="hidden" name="access_key" value="2133ed425ed43e63aa8b4fdd6e8ad28a"  />
            <input type="hidden" name="profile_id" value="LNECOM1"  />
            <input type="hidden" name="override_custom_receipt_page" value="<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/receipt.aspx") %>"  />
            <input type="hidden" name="transaction_uuid" value="<% Response.Write(getUUID()); %>"  />
                <%--<input type="hidden" name="transaction_uuid" value="123567" />--%>
            <input type="hidden" name="signed_field_names" value="access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,payment_method,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code,override_custom_receipt_page" />
            <input type="hidden" name="unsigned_field_names" value="card_type,card_number,card_expiry_date,card_cvn" />
            <input type="hidden" name="signed_date_time" value="<% Response.Write(getUTCDateTime()); %>"  />
                <%--<input type="hidden" name="signed_date_time" value="2014-05-15T22:05:17Z">--%>

            <input type="hidden" name="locale" value="en" />
            <input type="hidden" name="transaction_type"  />
            <input type="hidden" name="reference_number" />
            <input type="hidden" name="currency" />
            <input type="hidden" name="payment_method" />
            <label class="fieldName">First Name:</label><input type="text" name="bill_to_forename" /><br/>
            <label class="fieldName">Last Name:</label><input type="text" name="bill_to_surname" /><br/>
            <label class="fieldName">Email:</label><input type="text" name="bill_to_email" /><br/>
            <label class="fieldName">Phone:</label><input type="text" name="bill_to_phone" /><br/>
            <label class="fieldName">Address Line1:</label><input type="text" name="bill_to_address_line1" /><br/>
            <label class="fieldName">City:</label><input type="text" name="bill_to_address_city" /><br/>
            <label class="fieldName">State:</label><input type="text" name="bill_to_address_state" /><br/>
            <label class="fieldName">Country:</label><input type="text" name="bill_to_address_country" /><br/>
            <label class="fieldName">Postal Code:</label><input type="text" name="bill_to_address_postal_code" /><br/>
    
        </div>
        <div class="div-cell">
        <h1 class="section-title">Credit Card Information</h1>
         <label class="fieldName">Type:</label><input type="text" name="card_type" /><br/>
         <label class="fieldName">Number:</label><input type="text" name="card_number" /><br/>
         <label class="fieldName">Expiration date:</label><input type="text" name="card_expiry_date" /><br/>
         <label class="fieldName">CVN:</label><input type="text" name="card_cvn" /><br/>
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

