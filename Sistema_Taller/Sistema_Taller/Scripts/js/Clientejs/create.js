import { xhr, objRequest } from "../funciones/XHR.js"

import { agregarFila, datosFila, dataEdit, crearTabla, Parametros, rowEvent } from "../funciones/table.js"

import { llenarForm } from "../funciones/actionScripts.js"

window.onload = () => {
    sessionStorage.clear()
}


crearTabla("#tablaNegocio", "", "",
    new Parametros("NombreEmpresa", true),
    new Parametros("CedJuridica", true),
    new Parametros("Direccion", true),
    new Parametros("TelEmpresa", true)
)
    .then(d=> {
        d[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>"
        });

        var table = $(d[0]).DataTable({
            "lengthChange": false,
            "searching": false,
            "data": d[1] === "" ? null:d[1],
            "columns": d[3]
        });

        //edit
        rowEvent(d[0] + " tbody", table, ".edit", x => {
            $("#negocioModal").modal()
            llenarForm(document.forms[1], x.data())
            document.getElementById("btnAdd").addEventListener("click", () =>
            {
                dataEdit(x.data())
                x.remove().draw();
            }, false)
           
        })

        //delete
        rowEvent(d[0] + " tbody", table, ".delete", x => {
            dataEdit(x.data())
            x.remove().draw()
        })


        document.getElementById("modal").addEventListener("click", () => {
            document.getElementById("btnAdd").removeEventListener("click", () => { dataEdit(null);
            }, false)
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
                    };
                    datosFila(obj);

                    table.row.add(obj).draw();
                    document.forms[1].reset();
                })
                .catch(error => {
                    alert(error)
                });
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
            document.forms[0].reset()
            sessionStorage.clear()
        }
    })
})


//validacion de cedula y cedula jurica

//pasar a modulo