// ****************************************************
// Error modal
// ****************************************************

xhrError = function (xhr) {
    showError(xhr.responseText);
};


function showError(text) {

    var modal = $('#dataConfirmModal');

    console.log('open ' + modal.hasClass('in'));

    modal.addClass("modal-error");

    modal.find('#dataConfirmText').text(text);
    
    modal.find('#dataConfirmNo').text('Volver');

    modal.find('#dataConfirmTitle').text('Error');

    modal.find('#dataConfirmIcon').attr("class", "fa fa-exclamation");

    modal.modal('show');
    document.querySelectorAll('.modal-backdrop').forEach(function (a) {
        a.remove();
    });

}

$(document).ready(function () {
    // ****************************************************
    // Los onclick de los links con confirm los borro y los
    // guardo en un atributo aparte.
    // ****************************************************

    $('a[data-confirmbodytext], a[data-conf]').each(function () {
        var onclick = $(this).attr('onclick');

        $(this).attr('data-onclick', onclick);

        this.removeAttribute('onclick');
    });


    // ****************************************************
    // Listener para cuando se aprieta un link con confirm.
    // ****************************************************

    $('body').on('click', 'a[data-confirmbodytext], a[data-conf]', function (e) {

        e.stopPropagation();
        e.preventDefault();
        e.stopImmediatePropagation();

        var href = $(this).attr('href');
        var onclick = $(this).attr('data-onclick');
        var dataAjax = $(this).attr('data-ajax');

        var confirmModal = $('#dataConfirmModal');

        confirmModal.removeClass('modal-error');

        confirmModal.find('#dataConfirmText').text($(this).attr('data-confirmbodytext'));

        if ($(this).attr('data-conf')) {
            confirmModal.find('#dataConfirmText').text($(this).attr('data-conf'));
        }

        confirmModal.find('#dataConfirmYes').attr('href', href);

        if (onclick || dataAjax) {
            confirmModal.find('#dataConfirmYes').attr('data-dismiss', 'modal');
        }

        if (onclick) {
            confirmModal.find('#dataConfirmYes').attr('onclick', onclick);
        }

        if (dataAjax) {
            confirmModal.find('#dataConfirmYes').attr('data-ajax', dataAjax);
            var dataAjaxComplete = $(this).attr('data-ajax-complete');
            var dataAjaxSuccess = $(this).attr('data-ajax-success');
            var dataAjaxFailure = $(this).attr('data-ajax-failure');
            var dataAjaxMode = $(this).attr('data-ajax-mode');
            var dataAjaxUpdate = $(this).attr('data-ajax-update');

            confirmModal.find('#dataConfirmYes').attr('data-ajax-complete', dataAjaxComplete);
            confirmModal.find('#dataConfirmYes').attr('data-ajax-success', dataAjaxSuccess);
            confirmModal.find('#dataConfirmYes').attr('data-ajax-failure', dataAjaxFailure);
            confirmModal.find('#dataConfirmYes').attr('data-ajax-mode', dataAjaxMode);
            confirmModal.find('#dataConfirmYes').attr('data-ajax-update', dataAjaxUpdate);
        }

        var dataConfirmYesText = $(this).attr('data-confirmyestext');

        if (dataConfirmYesText) {
            confirmModal.find('#dataConfirmYes').text(dataConfirmYesText);
        } else {
            confirmModal.find('#dataConfirmYes').text('Confirmar');
        }

        confirmModal.find('#dataConfirmYes').text('Confirmar');

        var dataConfirmNoText = $(this).attr('data-confirmnotext');

        if (dataConfirmNoText) {
            confirmModal.find('#dataConfirmNo').text(dataConfirmNoText);
        } else {
            confirmModal.find('#dataConfirmNo').text('Cancelar');
        }

        var dataConfirmTitleText = $(this).attr('data-confirmtitletext');

        if (dataConfirmTitleText) {
            confirmModal.find('#dataConfirmTitle').text(dataConfirmTitleText);
        } else {
            confirmModal.find('#dataConfirmTitle').text('¿Está seguro?');
        }

        var dataConfirmIcon = $(this).attr('data-confirmicon');

        switch (dataConfirmIcon) {
            case 'Warning':
                confirmModal.find('#dataConfirmIcon').attr("class", "fa fa-exclamation");
                break;
            case 'Success':
                confirmModal.find('#dataConfirmIcon').attr("class", "fa fa-check");
                break;
            case 'Info':
                confirmModal.find('#dataConfirmIcon').attr("class", "fa fa-exclamation");
                break;
            case 'Danger':
                confirmModal.find('#dataConfirmIcon').attr("class", "fa fa-remove");
                break;
            default:
                confirmModal.find('#dataConfirmIcon').attr("class", "fa fa-exclamation");
        }

        confirmModal.modal('show');
        return false;
    });



});