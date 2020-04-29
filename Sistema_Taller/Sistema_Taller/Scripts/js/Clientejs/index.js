import { xhr } from "../funciones/XHR.js"
import { crearTabla, rowEvent, Parametros } from "../funciones/table.js"
import { llenarForm } from "../funciones/actionScripts.js";

function tabla(div1, id, ...columns) {
    let div = document.getElementById(div1);
    div.innerHTML = "";
    let table = document.createElement("table");
    table.id = id;
    table.className = "table table-hover table-bordered";
    let thead = document.createElement("thead")
    let tbody = document.createElement("tbody")
    let tr = document.createElement("tr")
    for (let i in columns) {
        let th = document.createElement("th")
        th.textContent = columns[i];
        tr.appendChild(th)
        thead.appendChild(tr)
    }
    table.appendChild(thead)
    table.appendChild(tbody)
    div.appendChild(table)
}
function a(div, idTable, url, redirect, ...params) {
    let p = []

    for (let i in params) {
        if (params[i] !== "")
            p.push(new Parametros(params[i], true));
    }
    return Promise.all([tabla(div, idTable, ...params),
        crearTabla('#' + idTable, url, redirect, ...p)
    ]);
}

function b(div, idTable, url, redirect, ...params) {
    a(div, idTable, url, redirect, ...params)
        .then(x => {
            x[1][3].push({
                "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",
            })
            let table = $(x[1][0]).DataTable({
                "processing": true,
                "serverSide": true,
                "ajax": {
                    "url": x[1][1],
                    "type": "POST",
                    "datatype": "json"
                },
                "cache": true,
                "pageLength": 10,
                "filter": true,
                "responsivePriority": 1,
                "data": null,
                "columns": x[1][3]
            });
            //edit
            rowEvent(x[1][0] + " tbody", table, ".edit", d => {
                (location.href = x[1][2] + "Editar/?id=" + Object.values(d.data())[0]);
            });
            //delete
            rowEvent(x[1][0] + " tbody", table, ".delete", d => {
                $("#Eliminar").modal();
                document.forms[1].elements[0].value = Object.values(d.data())[0];
            })

            document.forms[1].addEventListener("click", e => {
                e.preventDefault();

                xhr(x[1][2] + "Eliminar", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(Object.fromEntries(new FormData(document.forms[1])))
                }).then(f => {
                    if (f == "1") {
                        location.href = /Cliente/;
                    }
                }).catch(error => alert(error))
            });

        });

}

onload = () => { document.getElementById("lstCliente").click() }

document.getElementById("lstCliente").addEventListener("click", e => {
    e.target.className = "btn btn-primary";
    document.getElementById("lstEmpresa").className = "btn btn-outline-primary"
    b("lista", "tablaCliente", "/Cliente/LClientes", "/Cliente/", 'Nombre',
        'Apellidos', 'Cedula', 'Telefono', 'Correo', '');
})

document.getElementById("lstEmpresa").addEventListener("click", e => {
    e.target.className = "btn btn-primary";
    document.getElementById("lstCliente").className = "btn btn-outline-primary";
    b("lista", "tablaEmpresa", "/Empresa/ListaEmpresa", "/Empresa/", 'Nombre',
        'Apellidos','Empresa', 'Direccion', 'Correo', '');
})
/*
        rowEvent(data[0] + " tbody", table, ".show", (s) => {
           
            $("#visualizar").modal()
            for (let i in s.data()) {
                $("#" + i).val(s.data()[i])
            }
        })
        //edit

*/