export const crearUsername = (nombre, apellidos, nid) => {
    return (nombre.substr(nombre, 1) + apellidos.substr(apellidos, 3) + nid.substring(nid.length, nid.length - 2));
}
export const obtenerId = (url) => {
    return parseFloat(/\d+$/.exec(url))
}
export const llenarForm = (form, data) => {
    if (typeof form === "object" && typeof data === "object") {

        if (Array.isArray(form)) {
            form.forEach(x => {
                for (let j of x) {
                    for (let k in data) {
                        if (k == j.id) {
                            j.value = data[k]
                        }
                    }
                }
            })
        }
        else {
            for (let i of form) {
                if (i.type !== "button" && i.type !== "password" && i.type !== "submit") {
                    for (let j in data) {
                        if (j == i.id) {
                            if (i.type == "date") {
                                i.value = convertirFecha(data[j])
                            }
                            else {
                                i.value = data[j]
                            }
                        }
                    }
                }
            }
        }
    }
}
export const limpiar = (form) => {
    for (let i of form) {
        if (i.type !== "button" && i.type !== "submit") {
            i.value = "";
        }
    }
}
export function convertirFecha(valor) {

    let ms = parseFloat(/\d+/.exec(valor));
    let format  = new Date(ms);
    return format.getFullYear() + "-" + (format.getMonth() < 9 ? `0${format.getMonth() + 1}` : format.getMonth() + 1) + "-" +
        (format.getDate() < 10 ? `0${format.getDate()}` : format.getDate());


}