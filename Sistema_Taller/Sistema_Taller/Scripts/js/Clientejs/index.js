import { xhr } from "../funciones/XHR.js"
import { crearTabla, rowEvent, Parametros } from "../funciones/table.js"
document.forms[0].addEventListener("submit", e => {
    e.preventDefault()
    xhr("/Cliente/Eliminar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }).then(data => {
        if (data == "1") {
            location.replace("/Cliente/");
        }
    }).catch(error => {
        e.preventDefault();
        alert(error)
    })

})



onload =
    crearTabla("#tablaCliente", "/Cliente/ListaClientes", "/Clientes/",
    new Parametros("Nombre", true),
    new Parametros("Apellidos", true),
    new Parametros("Cedula" ,true),
    new Parametros("Correo", true),
    new Parametros("Empresa", true))
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
        //view
        rowEvent(data[0] + " tbody", table, ".show", (s) => {
           
            $("#visualizar").modal()
            for (let i in s) {
                $("#" + i).val(s[i])
            }
        })
        //edit
        rowEvent(data[0] + " tbody", table, ".edit", (s) => {
            location.href = "/Cliente/Editar/?id=" + Object.values(s)[0];
        })
        //delete
        rowEvent(data[0] + " tbody", table, ".delete", (s) => {
            $("#Eliminar").modal();
            document.forms[0].elements[0].value = Object.values(s)[0];

        })
    }).catch(error => alert(error));

//test
document.getElementById("lstCliente").addEventListener("click", (e) => {
    a()
})

function a() {
    tabla("#lista", "tablas", "Nombre", "Apellidos", "Cedula", "Teléfono", "Correo", "")

    crearTabla("#tablas", "/Cliente/LClientes", "/Cliente/",
        new Parametros("Nombre", true),
        new Parametros("Apellidos", true),
        new Parametros("Cedula", true),
        new Parametros("Telefono", true),
        new Parametros("Correo", true))
        .then(data => {
            data[3].push({
                "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",
            });
            console.log(document.querySelector("#tablas"))
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
                for (let i in s) {
                    $("#" + i).val(s[i])
                }
            })
            //edit
            rowEvent(data[0] + " tbody", table, ".edit", (s) => {
                location.href = "/Cliente/Editar/?id=" + Object.values(s)[0];
            })
            //delete
            rowEvent(data[0] + " tbody", table, ".delete", (s) => {
                console.log(s)
                $("#Eliminar").modal();
                document.forms[1].elements[0].value = Object.values(s)[0];

            })
        })
}
document.getElementById("lstEmpresa").addEventListener("click", (e) => {
    tabla("#lista", "tablas", "Nombre", "Apellidos", "Cedula", "Correo", "Empresa","")
    crearTabla("#tablas", "/Empresa/ListaEmpresa", "/Clientes/",
        new Parametros("Nombre", true),
        new Parametros("Apellidos", true),
        new Parametros("Cedula", true),
        new Parametros("Correo", true),
        new Parametros("Empresa", true))
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
            //view
            rowEvent(data[0] + " tbody", table, ".show", (s) => {

                $("#visualizar").modal()
                for (let i in s) {
                    $("#" + i).val(s[i])
                }
            })
            //edit
            rowEvent(data[0] + " tbody", table, ".edit", (s) => {
                location.href = "/Empresa/Editar/?id=" + Object.values(s)[0];
            })
            //delete
            rowEvent(data[0] + " tbody", table, ".delete", (s) => {
                console.log(s)
                $("#Eliminar").modal();
                document.forms[1].elements[0].value = Object.values(s)[0];

            })
        })
})

function tabla(div1, id, ...columns) {
    let div = document.querySelector(div1);
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