mpps = {};
mpps.processCheckout = function (form, success, error) {
    var data = form.serialize();
    data = data.replace(/card_[a-zA-Z0-9_]*=[a-zA-Z0-9-]*&?/g, "");

    $.ajax({
        type: "POST",
        url: "https://mpps.azurewebsites.net/api/Signing/",
        data: data,
        success: function (e) {
            form.append('<input type="hidden" name="signature" value="' + e.signature + '">');
            form.submit();
            if (typeof (success) == "function") {
                success();
            }
        },
        error: function (xhr, s, ee) {
            if (typeof (error) == "function") {
                error(s, ee);
            }
        }
    });
}