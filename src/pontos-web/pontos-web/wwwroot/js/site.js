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

$("#search").on("keyup", function () {
    var txtenter = $(this).val();
    $("table tr").each(function (results) {
        if (results !== 0) {
            var id = $(this).find("td:nth-child(2)").text();

            if (id.indexOf(txtenter) !== 0 && id.toLowerCase().indexOf(txtenter.toLowerCase()) < 0) {
                $(this).hide();
            }
            else {
                $(this).show();
            }
        }
    });
});