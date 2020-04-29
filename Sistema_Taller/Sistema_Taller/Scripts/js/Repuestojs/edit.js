import { xhr } from "../funciones/XHR.js"
import { obtenerId, llenarForm } from "../funciones/actionScripts.js"

xhr("/Repuesto/BuscarProveedorRepuesto/?id=" + obtenerId(location.href))
    .then(x => {
        console.log()
        llenarForm(document.forms[0], x);
        llenarForm(document.forms[1], x);
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
    }).then(x => {
        if (x == 1) {
            //message
        }
    }).catch(error => alert(error))
})

document.forms[1].addEventListener("submit", e => {
    e.preventDefault();

    xhr("/Repuesto/Editar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[1])))
    }).then(x => {
        if (x == 1) {
            //message
        }
    }).catch(error => alert(error))
})

/*
export function createObject(obj) {
    if (typeof obj === "object") {
        let objR = new Object();

        for (let j of obj) {
            if (j.type !== "submit") {
                objR[j.id] = j.value
            }
        }
        return objR;
    }
}*/