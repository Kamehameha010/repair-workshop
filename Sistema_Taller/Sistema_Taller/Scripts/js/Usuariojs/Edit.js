import { xhr } from "../funciones/XHR.js"
import { crearUsername, obtenerId, llenarForm, limpiar } from "../funciones/actionScripts.js"


onload = xhr("/Usuario/BuscarUsuario/?id=" + obtenerId(location.href))
    .then(data => {
        sessionStorage.obj = JSON.stringify(data)
        llenarForm(document.forms[0], data)
    });
document.getElementById("username").addEventListener("change", () => {

    xhr("/Usuario/ValidarUsuario", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ usuario: document.getElementById("username").value })
    }).then(x => {
        console.log(x)
        if (x == "0") {
            $("#usuario-existe").fadeIn("slow", 0)
        } else {
            $("#usuario-existe").fadeOut("slow", 0)
        }
    }).catch(error => console.log(error));
});
document.getElementById("username").addEventListener("focus", () => {
    if (document.getElementById("username") == "") {
        let n = crearUsername($("#nombre").val(), $("#apellidos").val(), $("#cedula").val())
        document.getElementById("username").value = n;
    }
});
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
            location.replace("/Usuario/");
        }
    });
    
})
