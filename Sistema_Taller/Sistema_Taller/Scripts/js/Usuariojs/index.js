import {crearTabla,Parametros, rowEvent} from "../funciones/table.js"
import { xhr } from "../funciones/XHR.js";



onload = crearTabla("#tablaUsuario", "/Usuario/ListaUsuarios", "/usuario/",
    new Parametros("nombre", true),
    new Parametros("apellidos", true),
    new Parametros("cedula", true),
    new Parametros("correo", true),
    new Parametros("Usuario", true),
    new Parametros("Rol", true),
    new Parametros("Estado", true))
    .then(data => {
        data[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",
        });
        console.log(data)
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
        //view
        rowEvent(data[0] + " tbody", table, ".show", (s) => {
            
            $("#visualizar").modal()
            for (let i in s.data()) {
                $("#" + i).val(s.data()[i])
            }
        })
        //edit
        rowEvent(data[0] + " tbody", table, ".edit", (s) => {
            location.href ="/Usuario/Editar/?id=" + Object.values(s.data())[0];
        })
        //delete
        rowEvent(data[0] + " tbody", table, ".delete", (s) => {
            $("#Eliminar").modal();
            document.forms[0].elements[0].value = Object.values(s.data())[0];

        })
    }).catch(error => alert(error));


document.forms[0].addEventListener("submit", e => {
    e.preventDefault()
    xhr("/Usuario/Eliminar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }).then(data => {
        if (data == "1") {
            location.replace("/Usuario/");
        }
    }).catch(error => {
        e.preventDefault();
        alert(error)
    })

})