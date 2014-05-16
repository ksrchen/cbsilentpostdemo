$(document).ready(function () {

    setDefaults();

    $('#panelBar .accordion-header .k-icon').each(function () {
        var accordionHeader = $(this).parent().parent();
        $(this).click(function () {
            toggleAccordion(accordionHeader);
        });
    });
    $(".terms").click(function () {
        computeAmount();
    });
    $("#continue").click(function () {
        hideAccordion($("#selectProduct"));
        showAccordion($("#purchase"));
    });

  
    $("#completePurchase").click(function () {
        var form = $('#payment_form');

        // Disable the submit button to prevent repeated clicks
        $("#completePurchase").prop('disabled', true);

        var data = form.serialize();
        data = data.replace(/card_[a-zA-Z0-9_]*=[a-zA-Z0-9]*&?/g, "");
        
        $.ajax({
            type: "POST",
            url: "/api/Signing/",
            data : data,
            success: function (e) {
                form.append('<input type="hidden" name="signature" value="' + e.signature + '">');

                form.submit();
            },
            error: function (e) {
            }
        });

        // Prevent the form from submitting with the default action
        return false;
    });
   

    showAccordion($("#selectProduct"));
    computeAmount();
});


function showAccordion(accordion) {
    if (isAccordionHidden(accordion)) {
        toggleAccordion(accordion);
    }
};

function isAccordionHidden(accordion) {
    return $(accordion).children('.accordion-content').is(":hidden");
};

function toggleAccordion(accordion) {
    if ($(accordion).children('.accordion-content').is(":visible")) {
        $(accordion).children('span').removeClass('k-minus');
        $(accordion).children('span').addClass('k-plus');
    }
    else {
        $(accordion).children('span').removeClass('k-plus');
        $(accordion).children('span').addClass('k-minus');
    }
    $(accordion).children('.accordion-content').slideToggle('fast');
};

function showAccordion(accordion) {
    $(accordion).children('.accordion-content').slideDown('fast');
};
function hideAccordion(accordion) {
    $(accordion).children('.accordion-content').slideUp('fast');
};

function computeAmount() {
    var amount = 0;
    $(".terms").each(function (idx, el) {
        if (el.checked) {
            amount = new Number(el.value);
        }
    });

    var tax = Math.round((amount * 7 / 100) * 100) / 100;
    var total = amount + tax;

    $("#productPrice").val(amount);
}
function setDefaults() {
    $("input[name='transaction_type']").val("authorization");
    $("input[name='reference_number']").val(new Date().getTime());
    $("input[name='amount']").val("100.00");
    $("input[name='currency']").val("USD");
    $("input[name='payment_method']").val("card");
    $("input[name='bill_to_forename']").val("John");
    $("input[name='bill_to_surname']").val("Doe");
    $("input[name='bill_to_email']").val("null@cybersource.com");
    $("input[name='bill_to_phone']").val("02890888888");
    $("input[name='bill_to_address_line1']").val("1 Card Lane");
    $("input[name='bill_to_address_city']").val("My City");
    $("input[name='bill_to_address_state']").val("CA");
    $("input[name='bill_to_address_country']").val("US");
    $("input[name='bill_to_address_postal_code']").val("94043");
    $("input[name='card_type']").val("001");
    $("input[name='card_number']").val("4242424242424242");
    $("input[name='card_expiry_date']").val("11-2020");
    $("input[name='card_cvn']").val("120");
}