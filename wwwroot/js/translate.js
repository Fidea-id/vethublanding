if ($(".translate").length) {
    const lang = localStorage.getItem("language") ?? "id";

    // initialize
    changeLanguage(lang);
    dropdownSelected(lang);

    $(".translate .dropdown-item").on("click", function (e) {
        e.preventDefault();
        const lang = $(this).data("value");

        localStorage.setItem("language", lang);
        changeLanguage(lang);
        dropdownSelected(lang);
    });

    function dropdownSelected(lang) {
        const item = $(".translate").find("[data-value=" + lang + "]");
        const icon = $(item).find("i").clone();
        const label = $(item).text();

        $(".dropdown-toggle").text(label);
        $(".dropdown-toggle").prepend(icon);

        $(".dropdown-item").removeClass("active");
        $(item).addClass("active");
    }

    async function changeLanguage(lang) {
        let data = await fetch("../lang/" + lang + ".json").then((rest) => rest.json());
        data = { [lang]: data };

        $(document).each(function (index, element) {
            $(this)
                .find("[key]")
                .each(function (_, element) {
                    $(this).html(data[lang][$(this).attr("key")]);
                });
        });
    }
}
