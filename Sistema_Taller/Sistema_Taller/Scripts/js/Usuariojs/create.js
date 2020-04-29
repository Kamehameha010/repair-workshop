import { xhr } from "../funciones/XHR.js"
import { crearUsername, limpiar } from "../funciones/actionScripts.js"


//btn crear
document.forms[0].addEventListener("submit", (e) => {
    e.preventDefault();
    xhr(location.pathname, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }, data => {
            if (data == "1") {
                document.forms[0].reset;
        }
    })
});

//Busca si la cedula esta duplicada
document.getElementById("cedula").addEventListener("keyup", () => {

    xhr("/Usuario/ValidarCedula", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ cedula: document.getElementById("cedula").value })
    }, (x) => {
        if (x == "0") {
            $("#cedula-existe").fadeIn("slow", 0)
        } else {
            $("#cedula-existe").fadeOut("slow", 0)
        }
    }).catch(error => console.log(error));
})

//Busca si la cedula esta duplicada
document.getElementById("username").addEventListener("change", () => {

    xhr("/Usuario/ValidarUsuario", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ usuario: document.getElementById("username").value })
    }, (x) => {
        if (x == "0") {
            $("#usuario-existe").fadeIn("slow", 0)
        } else {
            $("#usuario-existe").fadeOut("slow", 0)
        }
    }).catch(error => console.log(error));
});
//crear username
document.getElementById("username").addEventListener("focus", () => {
    if (document.getElementById("username").value == "") {
        let n = crearUsername($("#nombre").val(), $("#apellidos").val(), $("#cedula").val())
        document.getElementById("username").value = n;
    }
});