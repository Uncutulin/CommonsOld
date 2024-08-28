/*
 *  Main function.
 */
function LoadModalDebug(modal, url, values) {
    var $modal = $(modal);
    var text = $modal[0].getAttribute('data-loader-text');

    ShowLoader(modal, text);
    $modal.modal('show');

    $.ajax({
        url: url + values,
        success: function (data) {
            updateModalContentDebug(modal, data);
        }
    });
}


/*
 *
 */
function updateModalContentDebug(modal, content) {
    var $modal = $(modal);
    var $body = $modal.find('.modal-body');

    $body.html(content);

    var $elems = $(modal).find('img, iframe, embed, object');

    if (window.navigator.userAgent.indexOf('MSIE ') > 0) {
        console.log('Found MSIE');
        $elems = $(modal).find('img, iframe');
    }

    var elemsCount = $elems.length;

    console.log(`${elemsCount} to load.`);

    if (elemsCount == 0) {
        HideLoader($modal);
    }

    var loadedCount = 0;
    
    $elems.on('load',
        function (e) {
            loadedCount++;
            console.log(`Loaded: ${e}`);
            if (loadedCount == elemsCount) {
                HideLoader(modal);
            }
        });
    
    $modal.find('form').filter(':not([href="\\#"], [href*="javascript"], [data-ajax="true"])').each(function () {
        $(this).on('submit',
            function (e) {
                var text = $(e.currentTarget)[0].getAttribute('data-loader-text');
                console.log(text);
                ShowLoader($modal, text);
            });
    });

    $modal.find('a[href]').filter(':not([href="\\#"], [href*="javascript"], [data-ajax="true"])').each(function () {
        $(this).on('click',
            function (e) {
                var text = $(e.currentTarget)[0].getAttribute('data-loader-text');
                ShowLoader($modal, text);
            });
    });

    if ($modal.hasClass("recursive-modal")) {

        MakeRecursive(modal);

    }
}

