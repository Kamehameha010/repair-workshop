import { xhr } from "../funciones/XHR.js"
import { llenarForm, obtenerId } from "../funciones/actionScripts.js"

xhr("/Empresa/BuscarEmpresa/?id=" + obtenerId(location.href))
    .then(d => {

        llenarForm([document.forms[0], document.forms[1]], d)
        
    })
    .catch(error => alert(error))

document.forms[1].addEventListener("submit", (e) => {
    e.preventDefault();
    let obj = {
        IdEmpresa : $("#IdEmpresa").val(),
        Nombre : $("#Empresa").val(),
        CedJuridica : $("#CedJuridica").val(),
        Direccion : $("#Direccion").val(),
        TelEmpresa : $("#TelEmpresa").val()
    }
    xhr("/Empresa/Editar", {
        method: "POST",
        headers: {
            "Content-Type":"application/json"
        },
        body: JSON.stringify(obj)
    })
        .then(resp => {
            if (resp == "1") {
                //msg
            }
        })
        .catch(error => alert(error))

})