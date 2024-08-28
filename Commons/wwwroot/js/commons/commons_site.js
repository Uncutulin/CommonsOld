function ReInitialize() {

    // Tooltips
    $('body').tooltip({ selector: '[data-toggle="tooltip"]' });
    
    // Minimize
    $(document).on('click', '.btn-box-tool[data-widget="collapse"]', function (e) {
        console.log('collapse');
        e.preventDefault();
        $($(this).closest('.box')[0]).boxWidget('toggle');
    });
}

$.extend(true, $.fn.dataTable.defaults, {
    stateSave: true,
    responsive: true,

    language: {
        processing: '<i class="fa fa-refresh fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ',
        lengthMenu: "Mostrar _MENU_",
        zeroRecords: "No se encontraron resultados",
        info: "Página _PAGE_ de _PAGES_",
        infoEmpty: "No hay registros disponibles",
        infoFiltered: "(filtrado de _MAX_ registros)",
        search: "Buscar:",
        paginate: {
            "first": "<i class='fa fa-angle-double-left'></i>",
            "last": "<i class='fa fa-angle-double-right'></i>",
            "next": "<i class='fa fa-angle-right'></i>",
            "previous": "<i class='fa fa-angle-left'></i>"
        },
    },
    columnDefs: [
        {
            targets: '_all',
            render: $.fn.dataTable.render.text()
        }
    ]
});


$.extend($.fn.dataTable.defaults, {
    responsive: true
});