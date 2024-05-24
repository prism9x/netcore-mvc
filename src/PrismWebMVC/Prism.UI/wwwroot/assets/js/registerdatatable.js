function registerDatatable(elementName, columns, urlApi) {

    new DataTable(elementName, {
        // Cài đặt layout
        layout: {
            bottom1End: function () {
                let addBtn = document.createElement('div');
                addBtn.innerHTML = `<a class="btn btn-light px-5 my-2" href='/admin/account/savedata' title="Thêm mới">Thêm mới</a>`;

                return addBtn;
            }
        },
        // Cài đặt ngôn ngữ hiên thị
        language: {
            url: '//cdn.datatables.net/plug-ins/2.0.7/i18n/vi.json',
        },
        scrollY: ($(window).height() - 400),
        scrollX: true,
        processing: true,
        serverSide: true,
        columns: columns,
        // Cài đặt gọi API data
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json'
        }
    });

}

document.getElementById('togglePassword').add