import { xhr } from "../funciones/XHR.js"
import { dataEdit, agregarFila, datosFila, crearTabla, Parametros, rowEvent } from "../funciones/table.js"
import { llenarForm } from "../funciones/actionScripts.js"
import { CasoDetalle, Articulo, Caso } from "../Class/Class.js";

window.onload = () => {
    sessionStorage.clear()

    obtenerFecha();

    setTimeout(NumeroCaso, 5000);
}

crearTabla("#tablaCaso", "", "",
    new Parametros("Articulo", true),
    new Parametros("Codigo", true),
    new Parametros("Modelo", true),
    new Parametros("Serie", true),
    new Parametros("Observacion", true)
    
)
    .then(x => {
        x[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>"
        });
        var table = $(x[0]).DataTable({
            "lengthChange": false,
            "searching": false,
            "data": x[1] == "" ? null : d[1],
            "columns": x[3]
        });

        //delete
        rowEvent(x[0] + " tbody", table, ".delete", x => {
            dataEdit(obj(x.data()));
            x.remove().draw();
        });

        //edit
        rowEvent(x[0] + " tbody", table, ".edit", x => {
            $("#ArticuloModal").modal();
            llenarForm(document.forms[1], x.data())
            document.getElementById("btnAdd").addEventListener("click", () => {
                dataEdit(obj(x.data()))
                x.remove().draw();
            }, false)
        });

        document.getElementById("ArticuloModal").addEventListener("click", () => {
            document.getElementById("btnAdd").removeEventListener("click", () => { dataEdit(obj(null)) }, false)
        })

        document.forms[1].addEventListener("submit", e => {
            e.preventDefault();
            agregarFila(new FormData(document.forms[1]))
                .then(x => {
                    datosFila(obj(x));
                    table.row.add(x).draw()
                    document.forms[1].reset();
                    $("#ArticuloModal").modal("toggle")
                })
                .catch(error => {
                    e.preventDefault();
                    alert(error)
                })
        })
        document.forms[0].addEventListener("submit", e => {
            e.preventDefault();
            let b = Object.fromEntries(new FormData(document.forms[0]))
            let n = new Caso(0, b.IdUsuario, b.IdCliente, b.NumeroCaso, b.IdEstadoCaso, JSON.parse(sessionStorage.j))
            xhr("/Caso/Crear", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(n)
            }).then(res => {

                if (res == 1) {
                    document.forms[0].reset();
                    obtenerFecha();
                    NumeroCaso();
                    table.clear().draw();
                    sessionStorage.clear()
                }
            }).catch(error => { alert(error) })
            
        })

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
function obj(x) {
    let cd = new CasoDetalle();
    cd.Observacion = x.Observacion.trim();
    let at = new Articulo();
    at.Nombre_ = x.Articulo.trim();
    at.Codigo_ = x.Codigo.trim();
    at.Modelo_ = x.Modelo.trim();
    at.Marca = x.Marca.trim();
    at.Categoria = x.Categoria.trim();
    at.Serie_ = x.Serie.trim();
    cd.ObjArticulo = at
    return cd;
}

$(document).ready(function () {
    $("#NCliente").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Cliente/Buscar",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))

                }
            })
        },
        select: function (event, ui) {
            //set tagids to save
            $("#IdCliente").val(ui.item.id);
        }
    });
})


