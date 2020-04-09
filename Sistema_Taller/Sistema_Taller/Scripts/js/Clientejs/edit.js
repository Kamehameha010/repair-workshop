
import { xhr } from "../funciones/XHR.js"
import { obtenerId, llenarForm } from "../funciones/actionScripts.js"
//import { crearTabla, Parametros, rowEvent } from "../funciones/table.js"


var table;
xhr("/Cliente/BuscarC/?id=" + obtenerId(location.href))
    .then(d => {
        llenarForm(document.forms[0], d)
    })
    .catch(error => alert(error))


document.forms[0].addEventListener("submit", (e) => {
    e.preventDefault()

    xhr("/Cliente/Edit", {
        method: "POST",
        headers: {
            "Content-Type":"application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    })
        .then(res => {
            if (res == "1") {
                //enviar mensaje al div
            }
        })
        .catch(error => alert(error))
})