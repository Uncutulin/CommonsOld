
$(document).ready(function (e) {
    ReInitialize();
    loadPartials();
});

$(document).ajaxComplete(function() {
    ReInitialize();
    loadPartials();
});

function loadPartials() {
    $(".partialContents").each(async function (index, item) {
        var url = $(item).data("url");

        $(item).removeClass('partialContents');
        $(item).removeAttr('data-url');

        if (url && url.length > 0) {
            var loader = ($(item).closest('.box')[0] ? $(item).closest('.box')[0].querySelector('.overlay') : null);

            if (loader) { loader.style.display = ''; }

            await $(item).load(url,
                function () {
                    $(item).addClass('loaded');
                    if (loader !== null) { loader.style.display = 'none'; }
                }
            );
        }
    });
}