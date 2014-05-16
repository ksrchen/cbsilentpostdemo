mpps = {};
mpps.processCheckout = function (form) {
    var data = form.serialize();
    data = data.replace(/card_[a-zA-Z0-9_]*=[a-zA-Z0-9-]*&?/g, "");

    $.ajax({
        type: "POST",
        url: "https://mpps.azurewebsites.net/api/Signing/",
        data: data,
        success: function (e) {
            form.append('<input type="hidden" name="signature" value="' + e.signature + '">');

            form.submit();
        },
        error: function (xhr, s, ee) {
        }
    });
}