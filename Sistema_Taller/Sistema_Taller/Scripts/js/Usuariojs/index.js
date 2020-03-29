import {crearTabla,Parametros,rowSelect,rowSelectEdit, rowSelectDelete} from "../funciones/table.js"
import { xhr } from "../funciones/XHR.js";


document.forms[0].addEventListener("submit",e=>{
    e.preventDefault()
    xhr("/Usuario/Eliminar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }, data => {
        if (data == "1") {
            location.replace("/Usuario/");
        }
    });

})


crearTabla("#tablaUsuario", "/Usuario/ListaUsuarios", "/Usuario/",
    new Parametros("nombre", "nombre", true),
    new Parametros("apellidos", "apellidos", true),
    new Parametros("cedula", "cedula", true),
    new Parametros("correo", "correo", true),
    new Parametros("Usuario", "Usuario", true),
    new Parametros("Rol", "Rol", true),
    new Parametros("Estado", "Estado", true))
    .then(data => {
        data[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",
        });
        let table = $(data[0]).DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": data[1],
                "type": "POST",
                "datatype": "json"
            },
            "cache": true,
            "pageLength": 10,
            "filter": true,
            "responsivePriority": 1,
            "data": null,
            "columns": data[3]
        });
        rowSelect(data[0] + " tbody", table, "#visualizar");
        rowSelectEdit(data[0] + " tbody", table, data[2]);
        rowSelectDelete(data[0] + " tbody", table, data[2], "#Eliminar", document.forms[0])

    });

