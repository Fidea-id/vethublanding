// FORMAT NUMBER
window.formatNumber = (number, thousand = ",") => {
    return number
        .toString()
        .replace(/[.,\s]/g, "")
        .replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1${thousand}`);
};

if ($(".nav").length) {
    const nav = $(".nav");
    const btnMenu = $(nav).find("button");

    $(btnMenu).on("click", function () {
        const menu = $(nav).find(".menu");

        $(menu).toggleClass("hidden");

        $(menu).css({
            "border-color": "rgba(0,0,0, 0.2)",
            "border-top-width": "1px",
            "border-bottom-width": "1px",
        });
    });
}

// Navbar fixed
if ($(".navbar-fixed").length) {
    const navbarTopPosition = parseInt($(".navbar-fixed").offset().top);

    $(window).on("scroll", function () {
        const _element = $(".navbar-fixed");
        const windowTopPosition = $(window).scrollTop();

        if (windowTopPosition > navbarTopPosition) {
            $(_element).addClass("active");
        } else {
            $(_element).removeClass("active");
        }
    });
}

// Animation scroll
// $(window).on('scroll resize', function() {
$(window).on("scroll", function () {
    const windowHeight = $(window).height();
    const windowTopPosition = $(window).scrollTop();
    const windowBottomPosition = windowTopPosition + windowHeight;

    // get all class selector
    $(".fade-to-left, .fade-to-right, .fade-to-top, .fade-to-bottom").each(function () {
        const element = $(this);

        $.each(element, function () {
            const _element = $(this);
            const elementHeight = _element.outerHeight();
            const elementTopPosition = _element.offset().top;
            const elementBottomPosition = elementTopPosition + elementHeight;

            if (elementBottomPosition >= windowTopPosition && elementTopPosition <= windowBottomPosition) {
                _element.addClass("active");
            } else {
                _element.removeClass("active");
            }
        });
    });
});

// initial trigger scroll
$(window).trigger("scroll");

// Tabs
if ($(".tab").length) {
    const btnDefault = $(".tab").first();
    $(btnDefault).addClass("active");

    $(".tab").on("click", function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        // remove all active class
        $(".tab").removeClass("active");

        if (!$(this).hasClass("active")) {
            $(this).addClass("active");

            $(".tab-item").hide();
            $(`[data-item=${id}]`).fadeIn();
        }
    });
}

// Requeired field flag
if ($(".form-group").length) {
    $("span.required").remove();
    $(".form-group").each(function () {
        if ($(this).find(".form-control").attr("required")) {
            $(this).find("label").append('<span class="required"> *</span>');
        }
    });

    if ($("label.required").length) {
        $("label.required").append('<span class="required"> *</span>');
    }
}

if ($(".dropdown-toggle").length) {
    $(".dropdown-toggle").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();
        $(".dropdown-menu").toggle();
    });

    $("body")
        .not(".dropdown-toggle, .dropdown-menu")
        .on("click", function () {
            $(".dropdown-menu").hide();
        });
}
