import { xhr } from "../funciones/XHR.js"
import { llenarForm, obtenerId } from "../funciones/actionScripts.js"

xhr("/Proveedor/BuscarProveedor/?id=" + obtenerId(location.href))
    .then(d => {
        llenarForm(document.forms[0], d)
    })
    .catch(error => alert(error))

document.forms[0].addEventListener("submit", e => {
    e.preventDefault();

    xhr("/Proveedor/Editar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }).then(d => {
        if (d == "1") {
            //message
        }
    }).catch(error => alert(error))

})