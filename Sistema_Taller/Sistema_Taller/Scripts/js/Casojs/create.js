import { xhr } from "../funciones/XHR.js"
import { dataEdit, agregarFila, datosFila } from "../funciones/table.js"
import { llenarForm, limpiar } from "../funciones/actionScripts.js"


onload = obtenerFecha();
onload = () => { sessionStorage.j }
onload = setTimeout(NumeroCaso, 5000);
var table = $("#tablaCaso").DataTable({
    "lengthChange": false,
    "searching": false,
    "data": null,
    "columns": [
        { "data": "Articulo" },
        { "data": "Modelo" },
        { "data": "Serie" },
        { "data": "Observacion" },
        { "data": "Estado" },
        {
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>"
        }
    ]
});

$('#tablaCaso tbody').on('click', '.delete', function () {

    let data = table.row($(this).parents("tr")).data();
    dataEdit(data)
    table.row($(this).parents('tr')).remove().draw();
});

$('#tablaCaso tbody').on("click", ".edit", function () {
    let data = table.row($(this).parents("tr")).data();
    $("#ArticuloModal").modal()
    llenarForm(document.forms[1], data)
    document.getElementById("btnAdd").addEventListener("click", dataEdit(data), false)
    table.row($(this).parents('tr')).remove().draw();
});


document.getElementById("ArticuloModal").addEventListener("click", () => {
    document.getElementById("btnAdd").removeEventListener("click", dataEdit, false)
})

document.forms[1].addEventListener("submit", e => {
    e.preventDefault();
    agregarFila(new FormData(document.forms[1]))
        .then(x => {
            let obj = {
                Detalle:x.Observacion,
                IdEstado: x.Estado,
                Articulo: {
                    Nombre: x.Articulo,
                    Codigo: x.Codigo,
                    Modelo: x.Modelo,
                    IdMarca: x.Marca,
                    IdCategoria: x.Categoria,
                    Serie: x.Serie
                
                }
            }
            datosFila(obj);
            table.row.add({
                Articulo: x.Articulo,
                Modelo: x.Modelo,
                Serie: x.Serie,
                Observacion: x.Observacion,
                Estado: x.Estado
            }).draw()
            limpiar(document.forms[1])

        })
        .catch(error => {
            e.preventDefault();
            alert(error)
        })

})
document.forms[0].addEventListener("submit", e => {
    e.preventDefault();
})

function obtenerFecha() {
    fetch("/Caso/Fecha")
        .then(response => response.text())
        .then(data => { document.getElementById("FechaIngreso").value = data })
        .catch(error => console.log(error));
}
function NumeroCaso() {
    xhr("/Caso/NumeroCaso")
        .then(x => {
            document.getElementById("NumeroCaso").value = x;
        })
        .catch(error => alert(error))
}