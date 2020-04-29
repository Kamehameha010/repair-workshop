import { xhr } from "../funciones/XHR.js"
import { llenarForm } from "../funciones/actionScripts.js"
import { crearTabla, Parametros, rowEvent } from "../funciones/table.js"


crearTabla("#tablaProveedor", "/Proveedor/ListaProveedores", "/Proveedor/",
    new Parametros("Nombre", true),
    new Parametros("Correo", true),
    new Parametros("Telefono", true),
    new Parametros("Direccion", true)
).then(d => {
    d[3].push({
        "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",

    })

    let table = $(d[0]).DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": d[1],
            "type": "POST",
            "datatype": "json"
        },
        "cache": true,
        "pageLength": 10,
        "filter": true,
        "responsivePriority": 1,
        "data": null,
        "columns": d[3]
    });

    //view
    rowEvent(d[0] + " tbody", table, ".show", x => {
        $("#ProveedorModal").modal()
        for (let i in x.data()) {
            $("#" + i).html(x.data()[i])
        }
    })

    //edit
    rowEvent(d[0] + " tbody", table, ".edit", x => {
        location.href = d[2] + "Editar/?id=" + Object.values(x.data())[0];
    })

    //delete
    rowEvent(d[0] + " tbody", table, ".delete", x => {
        $("#Eliminar").modal()
        llenarForm(document.forms[0], x.data())

        document.forms[0].addEventListener("submit", e => {
            e.preventDefault();

            xhr(d[2] + "Eliminar", {
                method: "POST",
                headers: {
                    "Content-Type":"application/json"
                },
                body: JSON.stringify({
                    id: document.getElementById("IdProveedor").value
                })
            }).then(s => {
                if (s == "1") {
                    //message
                    
                    x.remove().draw()
                    $("#Eliminar").modal("toggle")
                }
            }).catch(error => alert(error))
        })

    })
    
})
    .catch(error => alert(error))