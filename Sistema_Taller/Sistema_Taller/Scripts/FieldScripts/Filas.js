
function eventos() {
    let btnEdit = document.getElementsByClassName("edit");
    let btnDelete = document.getElementsByClassName("delete");
    for (let i = 0; i < btnEdit.length; i++) {
        btnEdit[i].addEventListener("click", SelectRowEdit);
        btnDelete[i].addEventListener("click", SelectRowDelete);
    }
}
function SelectRowEdit(e) {

    if (btn_edit.style.display === "none") {
        btn_edit.style.display = "inline";
    }
    btn_save.style.display = "none";
    let data = e.target.parentElement.parentElement.parentElement.getElementsByTagName("td");

    let row = this.parentElement.parentElement;
    $("#negocioModal").modal(
        $("#idEmpresa").val(data[0].innerText),
        $("#NombreEmpresa").val(data[1].innerText),
        $("#CedJuridica").val(data[2].innerText),
        $("#Direccion").val(data[3].innerText),
        $("#TelefonoEmp").val(data[4].innerText),
        $("#idClienteE").val(data[5].innerText),
    );

 
    $("#edit").click(function () {
        editRow(event, row);
    });
}

function editRow(evt, row) {
    if (document.getElementById("NombreEmpresa").value === "") {
        evt.preventDefault();
    } else if (document.getElementById("CedJuridica").value === "") {
        evt.preventDefault();
    } else if (document.getElementById("Direccion").value === "") {
        evt.preventDefault();
    } else if (document.getElementById("TelefonoEmp").value === "") {
        evt.preventDefault();
    } else {
        let elements = [$("#idEmpresa").val(), $("#NombreEmpresa").val(), $("#CedJuridica").val(), $("#Direccion").val(), $("#TelefonoEmp").val(), $("#idClienteE").val()];;
        for (let i in elements) {
            row.cells[i].innerHTML = elements[i];
        }
    }
}

function SelectRowDelete(e) {

    let row = e.target.parentElement.parentElement;
    console.log(e.target)
    console.log(row)
    row.remove();
}

