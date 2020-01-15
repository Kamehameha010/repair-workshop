document.getElementById("borrar").onclick = () => { jsEliminar()}

function jsEliminar(id) {
    let url = "https://" + location.host + "/Usuario/Eliminar"
    fetch(url, {
        method: "POST",
        body: JSON.stringify({
            id = id
        }), headers: {
            'Accept': 'application/json',
            'Content-Type' : 'application/json'
        }
    }).then(response => {
        if (response.ok) {
            return response.text
        } else {
            throw new Error
        }
    }).then(data => {
        if (data != '1') {
            alert("No se ha podido eliminar dato")
        } else {
            alert("dato eliminado")
            document.location.href = "https://" + location.host+"/Usuario/"
        }
    })
}