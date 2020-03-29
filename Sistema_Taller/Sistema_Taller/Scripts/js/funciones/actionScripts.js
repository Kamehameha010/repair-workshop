 export const crearUsername = (nombre, apellidos, nid) => {
    return (nombre.substr(nombre, 1) + apellidos.substr(apellidos, 3) + nid.substring(nid.length, nid.length - 2));
}

export const obtenerId = (url) => {
    return parseFloat(/\d$/.exec(url))
}

export const llenarForm = (form, data,...distinc) => {
    if (typeof form === "object" && typeof data === "object") {
        for (let i of form) {
            if (i.type !== "button" && i.type !== "password") {
                for (let j in data) {
                    if (j == i.id) {
                        i.value = data[j]
                    }
                }
            }
        }
    }
}

export const limpiar = (form) => {
    for (let i in form) {
        if (form[i].type !== "button") {
            form[i].value = "";
        }
    }
}
