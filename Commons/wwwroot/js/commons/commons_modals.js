
/*
 *  Shows modal loader.
 */
function ShowLoader(modal, text) {
    var $modal = $(modal);
    var $loader = $modal.find('.holder')[0];
    var $body = $modal.find('.modal-body');

    $body[0].style.visibility = 'hidden';

    if (text) {
        var $text = $modal.find('.preloader-text')[0];
        $text.innerHTML = text;
    }

    $loader.style.display = '';
}


/*
 *  Hides modal loader.
 */
function HideLoader(modal) {
    var $modal = $(modal);
    var $loader = $modal.find('.holder')[0];
    var $body = $modal.find('.modal-body');
    
    $loader.style.display = 'none';
    $body[0].style.visibility = '';

    var $text = $modal.find('.preloader-text')[0];
    $text.innerHTML = '';
}


/*
 *  Main function.
 */
function LoadModal(modal, url, values) {
    var $modal = $(modal);
    var text = $modal[0].getAttribute('data-loader-text');

    ShowLoader(modal, text);
    $modal.modal('show');

    $.ajax({
        url: url + values,
        success: function (data) {
            updateModalContent(modal, data);
        }
    });
}


/*
 *
 */
function updateModalContent(modal, content) {
    var $modal = $(modal);
    var $body = $modal.find('.modal-body');

    $body.html(content);

    var $elems = $(modal).find('img, iframe, embed');

    if (window.navigator.userAgent.indexOf('MSIE ') > 0) {
        $elems = $(modal).find('img, iframe');
    }

    var elemsCount = $elems.length;

    if (elemsCount == 0) {
        HideLoader($modal);
    }

    var loadedCount = 0;
    
    $elems.on('load',
        function () {
            loadedCount++;
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


/*
 *  Overrides modal form and anchors to make them ajax calls.
 */
function MakeRecursive(modal) {
    var $modal = $(modal);
    $modal.find('form').each(function () {
        $(this).on('submit',
            function (e) {
                var form = $(this);
                var url = form.attr('action');
                var ajax = form.attr('data-ajax');
                var file = form.find('input[type="file"]');

                if (url && !ajax && !file[0]) {
                    e.preventDefault();
                    $.ajax({
                        type: "POST",
                        url: url,
                        data: form.serialize(), // serializes the form's elements.
                        success: function (data) {
                            if (data.indexOf("<!DOCTYPE html>") > -1) {
                                // Response is full view.
                                if (!(typeof history.pushState === 'undefined')) {
                                    history.pushState(
                                        { url: url, title: document.title },
                                        document.title, // Can also use json.title to set previous page title on server
                                        url
                                    );
                                }
                                // Output the HTML
                                document.open();
                                document.write(data);
                                document.close();
                            } else {
                                // Response is partial view.
                                updateModalContent($modal, data);
                            }
                        }
                    });
                }

            });
    });

    $modal.find('a').each(function () {
        $(this).on('click',
            function (e) {
                var form = $(this);
                var url = form.attr('href');
                var ajax = form.attr('data-ajax');

                if (url && url !== '#' && !ajax) {
                    e.preventDefault();
                    e.stopPropagation();
                    $.ajax({
                        type: "POST",
                        url: url,
                        success: function (data) {
                            if (data.indexOf("<!DOCTYPE html>") > -1) {
                                // Response is full view.
                                if (!(typeof history.pushState === 'undefined')) {
                                    history.pushState(
                                        { url: url, title: document.title },
                                        document.title, // Can also use json.title to set previous page title on server
                                        url
                                    );
                                }
                                // Output the HTML
                                document.open();
                                document.write(data);
                                document.close();
                            } else {
                                // Response is partial view.
                                updateModalContent($modal, data);
                            }
                        }
                    });
                }

            });
    });

}





