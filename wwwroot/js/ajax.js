// Handle ajax request
if ($(".ajax-request").length) {
    $(".ajax-request").each(function () {
        const form = $(this).closest("form");
        const method = $(form).attr("method");
        const action = $(form).attr("action");
        const successRedirect = $(this).data("success-redirect");
        const button = $(form).find("button[type=submit]");

        $(button).on("click", function (e) {
            // currency format to number
            if ($(".currency-format").length) {
                $(".currency-format").each(function () {
                    $(this).val($(this).data("value"));
                });
            }

            const data = $(form).serialize();

            window.ajaxRequest({
                url: action,
                method: method,
                data: data,
                buttonElement: $(this),
                successCallback: function (response) {
                    if (typeof successRedirect != "undefined") {
                        window.location.href = successRedirect;
                    }
                },
            });
        });
    });
}

// ajax request
window.ajaxRequest = ({ url, method, data, buttonElement, successCallback, errorCallback }) => {
    const form = $(buttonElement).closest("form");
    const btnText = $(buttonElement).text();
    const btnProcess = $(buttonElement).data("label") || "Saving ...";

    $(buttonElement).attr("disabled", true);
    $(buttonElement).html('<i class="fa fa-refresh fa-spin"></i> ' + btnProcess);

    // reset error
    $(form).find(".error-text").remove();
    $(form).find(".has-error").removeClass("has-error");

    $.ajax({
        url: url,
        type: method,
        data: data,
        success: function (response) {
            if (response.data != null) {
                if (response.data.message != null && typeof response.data.redirect == "undefined") {
                    if (response.data.message.hasOwnProperty("text")) {
                        if (!response.data.message.hasOwnProperty("type")) {
                            response.data.message = {
                                ...response.data.message,
                                type: "success",
                            };
                        }

                        if (!response.data.message.hasOwnProperty("title")) {
                            response.data.message = {
                                ...response.data.message,
                                title: response.data.message.type.toUpperCase(),
                            };
                        }

                        toastr["success"](response.data.message, "SUCCESS");
                    }
                }

                if (response.data.redirect != null) {
                    window.location.href = response.data.redirect;
                }
            }

            if (successCallback != null) {
                successCallback(response);
                return;
            }
        },
        error: function (xhr, status, message) {
            const errors = xhr.responseJSON?.errors;

            if (errorCallback != null) {
                errorCallback(xhr, status, message);
                return;
            }

            if (typeof errors === typeof undefined) {
                const msg = xhr.responseJSON?.message;

                if (typeof msg !== typeof undefined) {
                    toastr["error"](msg, "ERROR");
                    return;
                }
            }

            $.each(errors, function (key, val) {
                try {
                    let inputkey = null;
                    $.each(key.split("."), function (index, val) {
                        if (index == 0) {
                            inputkey = val;
                        } else {
                            inputkey += `[${val}]`;
                        }
                    });

                    const input = $(form)
                        .find('[name^="' + inputkey + '"]')
                        .closest(".form-group");

                    if (input.length) {
                        $(input).append('<span class="help-block error-text">' + val + "</span>");
                        $(input).addClass("has-error");
                        return;
                    }

                    throw 401;
                } catch (error) {
                    toastr["error"](val, "ERROR");
                }
            });
        },
        complete: function () {
            $(buttonElement).attr("disabled", false);
            $(buttonElement).html('<i class="fa fa-refresh fa-save"></i> ' + btnText);
        },
    });
};