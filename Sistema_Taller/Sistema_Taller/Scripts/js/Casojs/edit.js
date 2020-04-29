import { xhr } from "../funciones/XHR.js"
import { obtenerId, llenarForm } from "../funciones/actionScripts.js"
import { agregarFila, crearTabla, Parametros, datosFila, rowEvent, dataEdit } from "../funciones/table.js"
import { CasoDetalle, Caso } from "../Class/Class.js"

sessionStorage.clear()
xhr("/Caso/BuscarCaso/?id=" + obtenerId(location.href))
    .then(data => {
        
        for (let i of data.CasoDetalle) {
            datosFila(obj(i))
        }
        llenarForm(document.forms[0], data);
        return crearTabla("#tablaCaso", JSON.stringify(data.CasoDetalle), "",
            new Parametros("Articulo", true),
            new Parametros("Modelo", true),
            new Parametros("Serie", true),
            new Parametros("Observacion", true),
            new Parametros("Diagnostico", true)
        )
    })
    .then(d => {
        d[3].push({
            "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>"
        });

        let table = $(d[0]).DataTable({
            "lengthChange": false,
            "searching": false,
            "data": JSON.parse(d[1]),
            "columns": d[3]

        });
        
        //edit
        rowEvent(d[0] + " tbody", table, ".edit", x => {
            llenarForm(document.forms[1], x.data())
            $("#ArticuloModal").modal()
       
            $("#save").click(() => {
                dataEdit(obj(x.data()));
                x.remove().draw();
            });
            
        })


        document.forms[1].addEventListener("submit", e => {
            e.preventDefault();

            agregarFila(new FormData(document.forms[1]))
                .then(x => {
                    datosFila(obj(x))
                    table.row.add(x).draw()
                    document.forms[1].reset();
                    $("#ArticuloModal").modal("toggle")
                })
                .catch(error => {
                    e.preventDefault();
                    alert(error)
                })
        })
    }).catch(error => alert(error))

document.forms[0].addEventListener("submit", e => {
    e.preventDefault();

    let caso = new Caso();
    caso.IdCa = getValue(0,"IdCaso")
    caso.IdEstadoCaso = $("#IdEstadoCaso").val()
    caso.CasoDetalle = JSON.parse(sessionStorage.j)

    xhr(location.pathname, {
        method: "POST",
        headers: {
            "Content-Type":"application/json"
        },
        body:
            JSON.stringify(caso)
    }).then(d => {
        if (d == "1") {
            //mesagge
        }

    })

})
function createObject(obj) {
    if (typeof obj === "object") {
        let objR = new Object();

        for (let i in obj) {
            objR[i] = obj[i]
        }
        return objR;
    }

}
function obj(x) {
    
    let cd = new CasoDetalle();
    cd.IdCd = x.IdCasoDetalle;
    cd.IdCa = x.IdCaso;
    cd.Id_Articulo = x.IdArticulo;
    cd.Observacion = x.Observacion;
    cd.Solucion = x.Diagnostico;
    return cd;
}



function getValue(Form, name) {
    return document.forms[Form].elements[name].value;
}