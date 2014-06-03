$(function () {
    if (window.addEventListener) {
        window.addEventListener("message", mpps.processMessage, false);
    }
    else {
        if (window.attachEvent) {
            window.attachEvent("onmessage", mpps.processMessage, false);
        }
    }
});

mpps = {};
mpps.rootUrl = 'http://localhost:54690';
//mpps.rootUrl = 'https://mpps.azurewebsites.net/';

mpps.sucess = {};
mpps.error = {};
mpps.processMessage = function (event) {
    if (typeof (mpps.success) == "function") {
        mpps.success(event.data);
    }
};

//mpps.processCheckout = function (form) {
//    var data = form.serialize();
//    data = data.replace(/card_[a-zA-Z0-9_]*=[a-zA-Z0-9-]*&?/g, "");

//    $.ajax({
//        type: "POST",
//        url: mpps.rootUrl +'/api/Signing/',
//        data: data,
//        success: function (e) {
//            form.append('<input type="hidden" name="signature" value="' + e.signature + '">');
//            form.submit();
//            //if (typeof (mpps.success) == "function") {
//            //    mpps.success();
//            //}
//        },
//        error: function (xhr, s, ee) {
//            if (typeof (mpps.error) == "function") {
//                mpps.error(s, ee);
//            }
//        }
//    });
//}
mpps.buildPaymentForm = function (selector) {
  //  var form = $("<form id='mpps-payment-form'>");
    $(selector).append('<div class="mpps-input-group"><label class="mpps-label mpps-label-card-type">Type:</label><input type="text" class="mpps-input mpps-input-card-type" data-mpps="card_type"></div>');
    $(selector).append('<div class="mpps-input-group"><label class="mpps-label mpps-label-card-number">Number:</label><input type="text" class="mpps-input mpps-input-card-number" data-mpps="card_number" /></div>');
    $(selector).append('<div class="mpps-input-group"><label class="mpps-label mpps-label-card-expiry-date">Expiration date:</label><input type="text" class="mpps-input mpps-input-card-expiry-date" data-mpps="card_expiry_date" /></div>');
    $(selector).append('<div class="mpps-input-group"><label class="mpps-label mpps-label-card-cvn">CVN:</label><input type="text" class="mpps-input mpps-input-card-cvn" data-mpps="card_cvn" /></div>');
   // $(selector).append(form);

    $("input[data-mpps='card_type']").val("001");
    $("input[data-mpps='card_number']").val("4242424242424242");
    $("input[data-mpps='card_expiry_date']").val("11-2020");
    $("input[data-mpps='card_cvn']").val("120");

   
}
mpps.ISODateString = function (d) {
    function pad(n) { return n < 10 ? '0' + n : n }
    return d.getUTCFullYear() + '-'
         + pad(d.getUTCMonth() + 1) + '-'
         + pad(d.getUTCDate()) + 'T'
         + pad(d.getUTCHours()) + ':'
         + pad(d.getUTCMinutes()) + ':'
         + pad(d.getUTCSeconds()) + 'Z'
}

mpps.checkout = function (options, success, error) {
    mpps.success = success;
    mpps.error = error;
    var data = {
        "access_key" : "2133ed425ed43e63aa8b4fdd6e8ad28a",
        "profile_id" : "LNECOM1",
        "override_custom_receipt_page": mpps.rootUrl + "/receipt.aspx",
        //override_custom_receipt_page :"https://cbsilentpostdemo.azurewebsites.net/receipt.aspx",
        "transaction_uuid" : new Date().getTime(),
        "signed_field_names": "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,payment_method,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code,override_custom_receipt_page",
        "unsigned_field_names": "card_type,card_number,card_expiry_date,card_cvn",
        "signed_date_time": mpps.ISODateString (new Date()),
        //"signed_date_time": "2014-05-27T19:03:48Z",
        "locale": "en",
        "transaction_type": "sale,create_payment_token",
        "reference_number": "1401217422318",
        "currency": "USD",
        "payment_method": "card",
        "bill_to_forename": options.contactInfo.firstName,
        "bill_to_surname": options.contactInfo.lastName,
        "bill_to_address_city": options.contactInfo.city,
        "bill_to_address_country": options.contactInfo.country,
        "bill_to_address_line1": options.contactInfo.addressLine1,
        //"bill_to_address_line2": options.contactInfo.addressLine2,
        "bill_to_address_state": options.contactInfo.state,
        "bill_to_address_postal_code": options.contactInfo.postalCode,
        "bill_to_email": options.contactInfo.email,
        "bill_to_phone": options.contactInfo.phone,
        "amount": options.amount
    };

    $.ajax({
        type: "POST",
        url: mpps.rootUrl + '/api/Signing/',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (e) {
            data.signature = e.signature;
            mpps.buildCheckoutForm(data);
            //if (typeof (mpps.success) == "function") {
            //    mpps.success();
            //}
        },
        error: function (xhr, s, ee) {
            if (typeof (mpps.error) == "function") {
                mpps.error(s, ee);
            }
        }
    });

    //mpps.buildCheckoutForm(options);

   // mpps.processCheckout($('#mpps-payment_frame form'));

}
mpps.buildCheckoutForm = function (data) {
    var cbSilentPostUrl = 'https://testsecureacceptance.cybersource.com/silent/pay';

    var iframe = $('#mpps-payment_frame');
    if (iframe.length) {
        iframe.remove();
    }
    $('body').append('<iframe id="mpps-payment_frame" name="payment_frame" frameborder="0" style="height: 980px; width: 900px" />');
    

    var frame = $("#mpps-payment_frame")[0].contentWindow;
    frame.document.open();
    frame.document.write('<html><head></head><body>');
    frame.document.write('<form id="mpps-checkout-form" method="post" action="' + cbSilentPostUrl + '">');
    frame.document.write('<input type="hidden" name="access_key" value="' + data.access_key + '"  />');
    frame.document.write('<input type="hidden" name="profile_id" value="' + data.profile_id + '"  />');
    frame.document.write('<input type="hidden" name="override_custom_receipt_page" value="' + data.override_custom_receipt_page+ '" />');
    frame.document.write('<input type="hidden" name="transaction_uuid" value="' + data.transaction_uuid + '"  />');
    frame.document.write('<input type="hidden" name="signed_field_names" value="'+  data.signed_field_names + '" />');
    frame.document.write('<input type="hidden" name="unsigned_field_names" value="'+data.unsigned_field_names+ '" />');
    frame.document.write('<input type="hidden" name="signed_date_time" value="' + data.signed_date_time +'"  />');
    frame.document.write('<input type="hidden" name="locale" value="'+data.locale + '" />');
    frame.document.write('<input type="hidden" name="transaction_type" value="'+data.transaction_type+'" />');
    frame.document.write('<input type="hidden" name="reference_number" value="' + data.reference_number + '"/>');
    frame.document.write('<input type="hidden" name="currency" value="'+data.currency+'" />');
    frame.document.write('<input type="hidden" name="payment_method" value="'+data.payment_method + '" />');
    frame.document.write('<input type="hidden" name="bill_to_forename" value="' + data.bill_to_forename + '"/>');
    frame.document.write('<input type="hidden" name="bill_to_surname" value="' + data.bill_to_surname + '" />');
    frame.document.write('<input type="hidden" name="bill_to_email" value="' + data.bill_to_email + '" />');
    frame.document.write('<input type="hidden" name="bill_to_phone" value="' + data.bill_to_phone + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_line1" value="' + data.bill_to_address_line1 + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_city" value="' + data.bill_to_address_city + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_state" value="' + data.bill_to_address_state + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_country" value="' + data.bill_to_address_country + '" />');
    //frame.document.write('<input type="hidden" name="bill_to_address_line2" value="' + data.bill_to_address_line2 + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_postal_code"" value="' + data.bill_to_address_postal_code + '" />');
    frame.document.write('<input type="hidden" name="card_type" value="' + $("input[data-mpps='card_type']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_number" value="' + $("input[data-mpps='card_number']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_expiry_date" value="' + $("input[data-mpps='card_expiry_date']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_cvn" value="' + $("input[data-mpps='card_cvn']").val() + '" />');
    frame.document.write('<input type="hidden" name="amount" value="' + data.amount + '" />');
    frame.document.write('<input type="hidden" name="signature" value="' + data.signature + '">');
    frame.document.write('</form>');
    frame.document.write('</body></html>');
    frame.document.body.onload = function () {
        frame.document.getElementById("mpps-checkout-form").submit();
    };

    frame.document.close();

};

mpps.createPaymentToken = function (options, success, error) {
    mpps.success = success;
    mpps.error = error;
    var data = {
        "override_custom_receipt_page": mpps.rootUrl + "/receipt.aspx",
        "transaction_uuid": new Date().getTime(),
        "signed_field_names": "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,currency,payment_method,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code,override_custom_receipt_page",
        "unsigned_field_names": "card_type,card_number,card_expiry_date,card_cvn",
        "signed_date_time": mpps.ISODateString(new Date()),
        "locale": "en",
        "transaction_type": "create_payment_token",
        "reference_number": options.referenceNumber,
        "currency": "USD",
        "payment_method": "card",
        "bill_to_forename": options.contactInfo.firstName,
        "bill_to_surname": options.contactInfo.lastName,
        "bill_to_address_city": options.contactInfo.city,
        "bill_to_address_country": options.contactInfo.country,
        "bill_to_address_line1": options.contactInfo.addressLine1,
        "bill_to_address_state": options.contactInfo.state,
        "bill_to_address_postal_code": options.contactInfo.postalCode,
        "bill_to_email": options.contactInfo.email,
        "bill_to_phone": options.contactInfo.phone,
    };

    $.ajax({
        type: "POST",
        url: mpps.rootUrl + '/api/signing/',
        contentType: 'application/json',
        headers: {
            'Authorization': options.key + ':' + mpps.sign({}),
            'x-timestamp': mpps.ISODateString(new Date())
        },
        data: JSON.stringify(data),
        success: function (e) {
            mpps.buildTokenCreateForm(e);
        },
        error: function (xhr, s, ee) {
            if (typeof (mpps.error) == "function") {
                mpps.error(ee);
            }
        }
    });
}
mpps.sign = function (options) {
    var hash = CryptoJS.HmacSHA256("Message", "Secret Passphrase");
    return hash.toString(CryptoJS.enc.Base64);
}
mpps.buildTokenCreateForm = function (data) {
    var iframe = $('#mpps-payment_frame');
    if (iframe.length) {
        iframe.remove();
    }
    $('body').append('<iframe id="mpps-payment_frame" name="payment_frame" frameborder="0" style="height:0px; width:0px" />');

    var frame = $("#mpps-payment_frame")[0].contentWindow;
    frame.document.open();
    frame.document.write('<html><head></head><body>');
    frame.document.write('<form id="mpps-checkout-form" method="post" action="' + data.url + '">');
    frame.document.write('<input type="hidden" name="access_key" value="' + data.access_key + '"  />');
    frame.document.write('<input type="hidden" name="profile_id" value="' + data.profile_id + '"  />');
    frame.document.write('<input type="hidden" name="override_custom_receipt_page" value="' + data.override_custom_receipt_page + '" />');
    frame.document.write('<input type="hidden" name="transaction_uuid" value="' + data.transaction_uuid + '"  />');
    frame.document.write('<input type="hidden" name="signed_field_names" value="' + data.signed_field_names + '" />');
    frame.document.write('<input type="hidden" name="unsigned_field_names" value="' + data.unsigned_field_names + '" />');
    frame.document.write('<input type="hidden" name="signed_date_time" value="' + data.signed_date_time + '"  />');
    frame.document.write('<input type="hidden" name="locale" value="' + data.locale + '" />');
    frame.document.write('<input type="hidden" name="transaction_type" value="' + data.transaction_type + '" />');
    frame.document.write('<input type="hidden" name="reference_number" value="' + data.reference_number + '"/>');
    frame.document.write('<input type="hidden" name="currency" value="' + data.currency + '" />');
    frame.document.write('<input type="hidden" name="payment_method" value="' + data.payment_method + '" />');
    frame.document.write('<input type="hidden" name="bill_to_forename" value="' + data.bill_to_forename + '"/>');
    frame.document.write('<input type="hidden" name="bill_to_surname" value="' + data.bill_to_surname + '" />');
    frame.document.write('<input type="hidden" name="bill_to_email" value="' + data.bill_to_email + '" />');
    frame.document.write('<input type="hidden" name="bill_to_phone" value="' + data.bill_to_phone + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_line1" value="' + data.bill_to_address_line1 + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_city" value="' + data.bill_to_address_city + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_state" value="' + data.bill_to_address_state + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_country" value="' + data.bill_to_address_country + '" />');
    //frame.document.write('<input type="hidden" name="bill_to_address_line2" value="' + data.bill_to_address_line2 + '" />');
    frame.document.write('<input type="hidden" name="bill_to_address_postal_code"" value="' + data.bill_to_address_postal_code + '" />');
    frame.document.write('<input type="hidden" name="card_type" value="' + $("input[data-mpps='card_type']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_number" value="' + $("input[data-mpps='card_number']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_expiry_date" value="' + $("input[data-mpps='card_expiry_date']").val() + '" />');
    frame.document.write('<input type="hidden" name="card_cvn" value="' + $("input[data-mpps='card_cvn']").val() + '" />');
    frame.document.write('<input type="hidden" name="signature" value="' + data.signature + '">');
    frame.document.write('</form>');
    frame.document.write('</body></html>');
    frame.document.body.onload = function () {
        frame.document.getElementById("mpps-checkout-form").submit();
    };

    frame.document.close();

};