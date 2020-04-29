import { xhr } from "../funciones/XHR.js"
import { crearTabla, Parametros, rowEvent, agregarFila, datosFila } from "../funciones/table.js"
import { llenarForm } from "../funciones/actionScripts.js"

sessionStorage.clear()

crearTabla("#tablaBuscar", "/Proveedor/ListaProveedores", "",
    new Parametros("Nombre", true),
    new Parametros("Correo", true),
    new Parametros("Telefono", true),
    new Parametros("Direccion", true)
)
    .then(x => {
        x[3].push({
            "defaultContent": "<a type='button' class='select'><span class='fa fa-edit'></span></a>"
        })
        let table = $(x[0]).DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": x[1],
                "type": "POST",
                "datatype": "json"
            },
            "cache": true,
            "pageLength": 10,
            "filter": true,
            "responsivePriority": 1,
            "data": null,
            "columns": x[3]
        });

        //select 
        rowEvent(x[0] + " tbody", table, ".select", s => {
            llenarForm(document.forms[0], s.data())
            $("#ProveedorModal").modal("toggle")
        })
    })


crearTabla("#TablaRepuesto", "", "",
    new Parametros("Codigo", true),
    new Parametros("Descripcion", true),
    new Parametros("Precio", true),
    new Parametros("Cantidad", true)
)
    .then(x => {
        x[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>"
        });
        let table = $(x[0]).DataTable({
            "lengthChange": false,
            "searching": false,
            "data": x[1] == "" ? null : x[1],
            "columns": x[3]
        });

        //delete
        rowEvent(x[0] + " tbody", table, ".delete", s => {
            dataEdit(obj(s.data()));
            s.remove().draw();
        });

        //edit
        rowEvent(x[0] + " tbody", table, ".edit", s => {
            $("#RepuestoModal").modal();
            llenarForm(document.forms[1], s.data())
            document.getElementById("btnAdd").addEventListener("click", () => {
                dataEdit(s.data())
                s.remove().draw();
            }, false)
        });

        document.getElementById("RepuestoModal").addEventListener("click", () => {
            document.getElementById("btnAdd").removeEventListener("click", () => { dataEdit(null) }, false)
        })

        document.forms[1].addEventListener("submit", e => {
            e.preventDefault();
            agregarFila(new FormData(document.forms[1]))
                .then(d => {
                   
                    datosFila(d);
                    table.row.add(d).draw()
                    document.forms[1].reset();
                    $("#RepuestoModal").modal("toggle")
                })
                .catch(error => {
                    e.preventDefault();
                    alert(error)
                })
        })
        document.forms[0].addEventListener("submit", e => {
            e.preventDefault();
            let y = JSON.parse(sessionStorage.j)
            y.forEach(x => x.IdProveedor = document.getElementById("IdProveedor").value)
            
           xhr("/Repuesto/Crear", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(y)
            }).then(res => {

                if (res == 1) {
                    document.forms[0].reset();
                    table.clear().draw();
                    sessionStorage.clear()
                }
            }).catch(error => { alert(error) })

        })

    })
    .catch(error => alert(error))
