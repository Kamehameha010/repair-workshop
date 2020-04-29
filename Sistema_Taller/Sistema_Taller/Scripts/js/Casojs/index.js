import { xhr } from "../funciones/XHR.js"
import { crearTabla, Parametros, rowEvent } from "../funciones/table.js"
import { convertirFecha, llenarForm } from "../funciones/actionScripts.js"


crearTabla("#tablaCaso", "/Caso/Casos", "/Caso/",
    new Parametros("FechaIngreso", true, x => convertirFecha(x)),
    new Parametros("Numerocaso", true),
    new Parametros("Usuario", true),
    new Parametros("Cliente", true),
    new Parametros("Estado", true))
    .then(d => {

        d[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",
        });
        var table = $(d[0]).DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": d[1],
                "type": "POST",
                "datatype": "jsonp"
            },
            "pageLength": 10,
            "filter": true,
            "responsivePriority": 1,
            "data": null,
            "columns": d[3]
        });

        //edit
        rowEvent(d[0] + " tbody", table, ".edit", data => {

            location.href = "/Caso/Editar/?id=" + Object.values(data.data())[0]
        })
        //delete
        rowEvent(d[0] + " tbody", table, ".delete", data => {
            $("#Eliminar").modal("toggle")
            console.log(Object.values(data.data())[0])
            document.getElementById("Id").value = Object.values(data.data())[0];
        })

    }).catch(error => alert(error))


document.forms[1].addEventListener("submit", (e) => {
    e.preventDefault();
    xhr("/Caso/Cerrar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[1])))
    }).then(d => {
        if (d == "1") {
            location.href = "/Caso/"
        }
    }).catch(error => alert(error));
})
