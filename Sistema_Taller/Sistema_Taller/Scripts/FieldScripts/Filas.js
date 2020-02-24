
function SelectRowEdit(e) {
    let row = e.target.parentElement.parentElement.parentElement.getElementsByTagName("td");
    $("#negocioModal").modal(
        $("#idEmpresa").val(row[0].innerText),
        $("#NombreEmpresa").val(row[1].innerText),
        $("#CedJuridica").val(row[2].innerText),
        $("#Direccion").val(row[3].innerText),
        $("#TelefonoEmp").val(row[4].innerText),
        $("#idClienteE").val(row[5].innerText),

    );
}