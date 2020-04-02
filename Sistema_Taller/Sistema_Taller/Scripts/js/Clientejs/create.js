import { xhr, objRequest } from "../funciones/XHR.js"
import { agregarFila, datosFila, rowSelect } from "../funciones/table.js"
import { limpiar, llenarForm } from "../funciones/actionScripts.js"
window.onload = () => {
    sessionStorage.clear()
}
var table = $("#tablaNegocio").DataTable({
    "lengthChange": false,
    "searching": false,
    "data": null,
    "columns": [
        { "data": "NombreEmpresa" },
        { "data": "CedJuridica" },
        { "data": "Direccion" },
        { "data": "TelEmpresa" },
        {
            "defaultContent": "<a type='button' class='show'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>"
        }
    ]
});

$('#tablaNegocio tbody').on('click', '.delete', function () {

    let data = table.row($(this).parents("tr")).data();
    dataEdit(data)
    table.row($(this).parents('tr')).remove().draw();
}); 

$('#tablaNegocio tbody').on("click", ".edit", function () {
    let data = table.row($(this).parents("tr")).data();
    $("#negocioModal").modal()
    llenarForm(document.forms[1], data)
    document.getElementById("btnAdd").addEventListener("click", dataEdit(data), false)
    table.row($(this).parents('tr')).remove().draw();
});

document.getElementById("modal").addEventListener("click", () => {
    document.getElementById("btnAdd").removeEventListener("click", dataEdit, false)

})

document.forms[1].addEventListener("submit", function (e) {
    e.preventDefault();
    agregarFila(new FormData(document.forms[1]))
        .then(data => {
            
            let obj = {
                NombreEmpresa: data["Empresa.NombreEmpresa"],
                CedJuridica: data["Empresa.CedJuridica"],
                Direccion: data["Empresa.Direccion"],
                TelEmpresa: data["Empresa.TelEmpresa"]
            }
            datosFila(obj)
            
            table.row.add(obj).draw()
            limpiar(document.forms[1])
        })
        .catch(error => {
            e.preventDefault();
            alert(error)
        })
})

document.forms[0].addEventListener("submit", (e) => {
    e.preventDefault()

    let newObj = objRequest(Object.fromEntries(new FormData(document.forms[0])),
        sessionStorage.j)
    xhr(location.pathname, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newObj)
    }, data => {
            if (data == "1") {
                limpiar(document.forms[0])
                sessionStorage.clear()
            }
    })
})

//validacion de cedula y cedula jurica

//pasar a modulo
function dataEdit(data = null) { 
    let s = JSON.parse(sessionStorage.j)
    let nd = s.filter(x => JSON.stringify(x) !== JSON.stringify(data))
    sessionStorage.j = JSON.stringify(nd)
}