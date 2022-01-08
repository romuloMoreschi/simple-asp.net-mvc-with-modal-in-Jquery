abrirModal = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

pesquisar = () => {
    var url = window.location.origin + "/Produto";
    $.ajax({
        type: "POST",
        url: url + "/Pesquisar",
        data: { Nome: $('#search').val() },
        success: function (res) {
            $("tbody tr").remove();
            var tr;
            $.each(res, function (index, value) {
                tr = $('<tr/>');
                tr.append("<td>" + value.id + "</td>");
                tr.append("<td>" + value.nome + "</td>");
                tr.append("<td>" + value.pontos + "</td>");
                tr.append("<td>" + value.categoria.nome + "</td>");
                tr.append("<td>" + 
                            `<a onclick="abrirModal('${url + "/Atualizar/" + value.id}')">` +
                                "<img src='/icons/update.svg' alt='update' />" +
                            "</a>" +
                            "<a href='" + url + "/Excluir/" + value.id + "' >" +
                                "<img src='/icons/delete.svg' alt='delete' />" +
                            "<a>" +
                        "</td>");
                $('tbody').append(tr);
            });
        }
    })
}