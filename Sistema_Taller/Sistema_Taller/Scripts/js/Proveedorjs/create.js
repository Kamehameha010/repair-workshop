import { xhr } from "../funciones/XHR.js"


document.forms[0].addEventListener("submit", e => {
    e.preventDefault();

    xhr("/Proveedor/Crear", {
        method: "POST",
        headers: {
            "Content-Type":"application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }).then(d => {
        if (d == "1") {
            //message
        }
    }).catch(error => alert(error))

})