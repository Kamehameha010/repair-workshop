document.getElementById("cedula").onkeyup = (() => ValidarCedula(document.getElementById("cedula").value))
document.getElementById("usuario").onkeyup = (() => ValidarUsuario(document.getElementById("usuario").value))
function ValidarUsuario(valor) {
    let url = "https://" + location.host + "/Usuario/ValidarUsuario/?cedula=" + valor
    fetch(url, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then((response) => {
        if (response.ok)
            return response.text()
        else
            throw new Error
    }).then((data) => {
        if (data != "1") {
            document.getElementById("usuario-existe").hidden = false;
            document.getElementById("usuario-existe").innerHTML = "Elemento Existe";
        } else {
            document.getElementById("usuario-existe").hidden = true;
        }
    }).catch(Error => { alert(Error) })
}




function ValidarCedula(valor) {
    let url = "https://" + location.host + "/Usuario/ValidarCedula/?cedula=" + valor
    fetch(url, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then((response) => {
        if (response.ok)
            return response.text()
        else
            throw new Error
    }).then((data) => {
        if (data != "1") {
            document.getElementById("cedula-existe").hidden = false;
            document.getElementById("cedula-existe").innerHTML = "Elemento Existe";
        } else {
            document.getElementById("cedula-existe").hidden = true;
        }
    }).catch(Error => { alert(Error) })
}

